## 1.2.0

依赖库迁移

### Change

* 迁移到 [CUCoreLib](https://github.com/jimmyking9999999/CUCoreLib) 和 [Bark](https://github.com/CNCUMC/Bark) 作为依赖
* 移除 MossLib 依赖
* 配置系统迁移到 CUCoreLib ModOptionsRegistry，设置项现在显示在游戏原生选项菜单中
* 本地化系统迁移到 CUCoreLib LocaleRegistry + Bark.Tool.BetterLocale
* ModCommand 重构：使用 CUCoreLib ConsoleCommandRegistry 替代 Bark.Base.ModCommandBase
* 添加 Directory.Build.props.example 作为开发者配置模板
* 更新 .gitignore 添加 Directory.Build.props（本地配置不提交）

### Fix

* 修复力量经验太高导致打物块有超级后坐力的问题
* 修复经验值达到最大值时游戏卡死的问题
* 修复 XpData 子属性（str_xp, res_xp, int_xp 等）缺少翻译键的问题
