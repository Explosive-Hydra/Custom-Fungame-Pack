using System;
using Bark.Constant;
using Bark.Tool;
using BepInEx;
using BepInEx.Logging;
using CUCoreLib.Data;
using CUCoreLib.Registries;
using CustomFungamePack.Data;
using CustomFungamePack.Data.Feature.Player;
using CustomFungamePack.Data.Feature.World;
using CustomFungamePack.Lang;
using HarmonyLib;

namespace CustomFungamePack;

[BepInPlugin(Guid, Name, Version)]
[BepInDependency("org.explosivehydra.Bark")]
public class Plugin : BaseUnityPlugin
{
    public const string Guid = "org.explosivehydra.customfungamepack";
    public const string Name = "Custom Fungame Pack";
    public const string Version = "1.2.0";

    internal new static ManualLogSource Logger;

    // Config values stored directly as static fields
    public static bool MoreLogs;
    public static bool StartGameUseFungame;
    public static string FirstUseFungame;
    public static int ProgressUpdateInterval;

    public static readonly Fungame TemplateFungame = new()
    {
        Name = $"{Name} Template",
        Id = "template",
        Version = Version,
        Author = ["Black_Moss"],
        Description = "a map template",
        Levels =
        [
            new LevelData
            {
                X = -68,
                Y = 62,
                Spawn = [0, 0],
                // Waypoints =
                // [
                //     new()
                //     {
                //         Id = "default",
                //         X = 0,
                //         Y = 0
                //     }
                // ],
                MapData = new MapData
                {
                    Map =
                    [
                        "1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",
                        "1122222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222211",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                   1111111111111111111",
                        "11                                                                                                                   1111111111111111111",
                        "11                                                                                              11111111111111111    11               11",
                        "11                                                                                              11111111111111111                     11",
                        "11                                                                                              11             11                     11",
                        "11                                                                                              11             11                     11",
                        "11                                                                                              11                      p p p p p p p 11",
                        "11                                                                                              11 b b b b b         1111111111111111111",
                        "11                                                                                              11                   1111111111111111111",
                        "11                                                                                              11111111111111111    11t             t1",
                        "11                                                                                              11111111111111111    11               11",
                        "11                                                                                              11111111111111111    11t             t11",
                        "11                                                                                              11iiiiiiiiiiiii11                     11",
                        "11                                                                                              11iiiiiiiiiiiii11                     11",
                        "11                                                                                              11iiiiiiiiiiiii11                     11",
                        "11                                                                                              11iiiiiiiiiiiii11          1111111111111",
                        "11                                                                                              11iiiiiiiiiiiii11          1111111111111",
                        "11                                                                                              11iiiiiiiiiiiii11          11         11",
                        "11                                                                                              11iiiiiiiiiiiii11          11         11",
                        "11                                                                                              11iiiigiiiiiiii11          11         11",
                        "11                                                                                              11111111111111111    11111111         11",
                        "11                                                                                              11111111111111111    11111111         11",
                        "11                                                                                                                                    11",
                        "11                                                                                                                                    11",
                        "11 s                                                                                                                                  11",
                        "11                     l                                              7   8   9                                              jjjjjjjjj11",
                        "1122222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222211",
                        "1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111"
                    ],
                    Key =
                    {
                        { " ", Blocks.Air.Id },
                        { "1", Blocks.SteelTile.Id },
                        { "2", Blocks.HeatResistantAlloy.Id },
                        { "l", "landmine" },
                        { "j", "jumppad" },
                        { "t", "turret" },
                        { "s", "soundcannon" },
                        { "p", "spikestabber" },
                        { "g", "geyser" },
                        { "i", "lifepodpump" },
                        { "b", "beartrap" },
                        { "9", "trader1" },
                        { "8", "trader2" },
                        { "7", "trader3" }
                    }
                },
                Items =
                [
                    new ItemData
                    {
                        Id = Items.Rifle,
                        Slot = Slots.MainHand
                    },
                    new ItemData
                    {
                        Id = Items.MindWipe,
                        Slot = Slots.Mouth
                    },
                    new ItemData
                    {
                        Id = Items.GravBag,
                        Slot = Slots.UpperBck
                    },
                    new ItemData
                    {
                        Id = Items.Aed,
                        Slot = Slots.MiddleBack
                    },
                    new ItemData
                    {
                        Id = Items.Lrd,
                        Slot = Slots.LowerBack
                    }
                ]
            }
        ],
        MineData = new MineData
        {
            Undestroy = true,
            ExplosionParamsData = new ExplosionParamsData
            {
                Range = 3
            }
        },
        JumpPadData = new JumpPadData
        {
            Force = 0.5f,
            NoLight = true,
            Cooldown = 0f
        },
        TurretData = new TurretData
        {
            Cooldown = 0f,
            NoLight = true
        },
        SoundCannonData = new SoundCannonData
        {
            MaxDistance = 20,
            Cooldown = 3,
            Undestroy = true
        },
        SpikeStabberData = new SpikeStabberData
        {
            Undestroy = true,
            NoLight = true,
            Cooldown = 3
        },
        GeyserData = new GeyserData
        {
            Cooldown = 1
        },
        BearTrapData = new BearTrapData
        {
            Undestroy = true,
            Cooldown = 1
        },
        XpData = new XpData
        {
            StrXp = 999,
            ResXp = 999,
            IntXp = 999
        },
        WorldSettingsData = new WorldSettingsData
        {
            SkipBackground = false
        }
    };

    private readonly Harmony _harmony = new(Guid);

    public void Awake()
    {
        Logger = base.Logger;

        LocaleGenerator.SetLogger(Logger);
        LocaleGenerator.Register(new EnLangGenerator(), Logger);
        LocaleGenerator.Register(new ZhCnLangGenerator(), Logger);
        LocaleGenerator.Register(new ZhTwLangGenerator(), Logger);
        LocaleGenerator.GenerateAll();

        _harmony.PatchAll();
        FungameCheck.Initialize();

        // Register settings using CUCoreLib ModOptionsRegistry
        RegisterSettings();
    }

    private static void RegisterSettings()
    {
        // Register more_logs setting
        RegisterBoolSetting(
            "more_logs",
            Setting.SettingCategory.Game,
            false,
            value => MoreLogs = value
        );

        // Register start_game_use_fungame setting
        RegisterBoolSetting(
            "start_game_use_fungame",
            Setting.SettingCategory.Game,
            false,
            value => StartGameUseFungame = value
        );

        // Register first_use_fungame setting
        RegisterDropdownSetting(
            "first_use_fungame",
            Setting.SettingCategory.Game,
            0,
            [new ModDropdownChoice("template", TemplateFungame.Id)],
            _ => FirstUseFungame = TemplateFungame.Id
        );

        // Register progress_update_interval setting
        RegisterIntSetting(
            "progress_update_interval",
            Setting.SettingCategory.Game,
            333,
            10,
            1000,
            value => ProgressUpdateInterval = value
        );
    }

    private static string GetSettingId(string key, Setting.SettingCategory category)
    {
        return $"customfungamepack.{category.ToString().ToLowerInvariant()}.{key}";
    }

    private static void RegisterBoolSetting(
        string key,
        Setting.SettingCategory category,
        bool defaultValue,
        Action<bool> apply)
    {
        var id = GetSettingId(key, category);
        ModOptionsRegistry.Register(ModOptionDefinition.Bool(
            id,
            Locale("other", $"customfungamepack.{key}", key),
            Locale("other", $"customfungamepack.{key}dsc", $"{key} description"),
            category,
            defaultValue,
            apply
        ));
    }

    private static void RegisterIntSetting(
        string key,
        Setting.SettingCategory category,
        int defaultValue,
        int min,
        int max,
        Action<int> apply)
    {
        var id = GetSettingId(key, category);
        ModOptionsRegistry.Register(ModOptionDefinition.Int(
            id,
            Locale("other", $"customfungamepack.{key}", key),
            Locale("other", $"customfungamepack.{key}dsc", $"{key} description"),
            category,
            defaultValue,
            min,
            max,
            apply
        ));
    }

    private static void RegisterDropdownSetting(
        string key,
        Setting.SettingCategory category,
        int defaultValue,
        ModDropdownChoice[] choices,
        Action<int> apply)
    {
        var id = GetSettingId(key, category);
        ModOptionsRegistry.Register(ModOptionDefinition.Dropdown(
            id,
            Locale("other", $"customfungamepack.{key}", key),
            Locale("other", $"customfungamepack.{key}dsc", $"{key} description"),
            category,
            defaultValue,
            choices,
            apply
        ));
    }

    private static string Locale(string key, params object[] args)
    {
        var text = BetterLocale.Other("other", key, key);
        return args.Length > 0 ? string.Format(text, args) : text;
    }
}