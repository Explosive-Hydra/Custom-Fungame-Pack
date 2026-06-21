## 1.2.0

Dependency Migration

### Change

* Migrated to [CUCoreLib](https://github.com/jimmyking9999999/CUCoreLib) and [Bark](https://github.com/CNCUMC/Bark) as dependencies
* Removed MossLib dependency
* Configuration system migrated to CUCoreLib ModOptionsRegistry (settings now appear in game's native options menu)
* Localization system migrated to CUCoreLib LocaleRegistry + Bark.Tool.BetterLocale
* ModCommand refactored: Using CUCoreLib ConsoleCommandRegistry instead of Bark.Base.ModCommandBase
* ModCommand list output updated to use Bark.Tool.Log (PrintNumberedList, PrintGroupedList)
* Added Directory.Build.props.example as developer configuration template
* Updated .gitignore to add Directory.Build.props (local config not committed)

### Fix

* Fixed high strength XP causing excessive recoil when hitting blocks
* Fixed game freeze when XP reaches maximum value
* Fixed missing translation keys for XpData child properties (str_xp, res_xp, int_xp, etc.)
