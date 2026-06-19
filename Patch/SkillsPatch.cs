using System;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;

namespace CustomFungamePack.Patch;

[HarmonyPatch(typeof(Skills))]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SkillsPatch
{
    /// <summary>
    /// 技能等级的安全上限。After level 30 the XP curve doubles each level,
    /// causing GetExperienceForLevel to overflow int.MaxValue for huge levels
    /// (e.g. 999), which breaks the entire XP system.
    /// </summary>
    private const int MaxSafeLevel = 30;

    /// <summary>
    /// CheckForLevelUp while 循环的最大迭代次数。
    /// 原始方法没有限制，当经验值极大时会导致游戏卡死。
    /// </summary>
    private const int MaxLevelUpsPerCheck = 20;

    [HarmonyPatch("Setup")]
    [HarmonyPrefix]
    private static bool SetupPrefix(Skills __instance, int chr)
    {
        var currentFungame = FungameCheck.CurrentFungame;
        if (currentFungame?.XpData == null)
            return true;

        var xpData = currentFungame.XpData;

        // 限制等级上限，防止 GetExperienceForLevel 溢出 int.MaxValue
        // 等级超过 30 后 XP 曲线每级翻倍，999 级会导致 float 溢出
        __instance.STR = Math.Min(xpData.StrXp, MaxSafeLevel);
        __instance.RES = Math.Min(xpData.ResXp, MaxSafeLevel);
        __instance.INT = Math.Min(xpData.IntXp, MaxSafeLevel);

        // 重算边界（确保 min/max/exp 与等级一致）
        __instance.UpdateExpBoundaries();

        // 钳制经验值到 [min, max)，防止加载时触发升级循环
        __instance.expSTR = ClampExp(xpData.ExpStr, __instance.minSTR, __instance.maxSTR);
        __instance.expRES = ClampExp(xpData.ExpRes, __instance.minRES, __instance.maxRES);
        __instance.expINT = ClampExp(xpData.ExpInt, __instance.minINT, __instance.maxINT);

        return false;
    }

    /// <summary>
    /// 替换 CheckForLevelUp，防止 while 循环无限迭代导致卡死。
    /// 与原始方法行为一致（循环内调 UpdateExpBoundaries），仅增加迭代上限。
    /// 达到上限后将对应属性的经验值钳制到 max-1，防止下一帧重复触发。
    /// </summary>
    [HarmonyPatch("CheckForLevelUp")]
    [HarmonyPrefix]
    private static bool CheckForLevelUpPrefix(
        Skills __instance,
        ref int level,
        float xp,
        ref int xpToLevel,
        ref bool __result)
    {
        int iterations = 0;
        while ((double)xp >= (double)xpToLevel)
        {
            ++level;
            __instance.UpdateExpBoundaries();
            if (++iterations >= MaxLevelUpsPerCheck)
            {
                // 达到上限，将当前属性的经验值钳制到 max-1
                // xp 是 this.expSTR/RES/INT 的副本，通过值匹配确定是哪个属性
                ConsumeExcessXp(__instance, xp);
                break;
            }
        }
        __result = iterations > 0;
        return false;
    }

    /// <summary>
    /// 根据传入的 xp 值（exp 字段的副本）匹配对应的属性，
    /// 将经验值设到 max-1，防止下一帧重复升级。
    /// </summary>
    private static void ConsumeExcessXp(Skills __instance, float xp)
    {
        // xp 是进入 CheckForLevelUp 时 this.expSTR/RES/INT 的副本
        // 循环中 exp 字段未被修改，所以 xp 仍匹配对应字段的当前值
        if (Math.Abs(xp - __instance.expSTR) < 0.5f)
            __instance.expSTR = __instance.maxSTR - 1;
        else if (Math.Abs(xp - __instance.expRES) < 0.5f)
            __instance.expRES = __instance.maxRES - 1;
        else if (Math.Abs(xp - __instance.expINT) < 0.5f)
            __instance.expINT = __instance.maxINT - 1;
    }

    private static float ClampExp(float exp, int min, int max)
    {
        if (max <= min) return min;
        return Math.Max(min, Math.Min(exp, max - 1));
    }
}
