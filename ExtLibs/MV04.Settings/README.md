# MV04 settings manager<!-- omit from toc -->

1. [Public fields](#public-fields)
2. [Setting item fields](#setting-item-fields)
3. [Public functions](#public-functions)
4. [Steps to add a new setting](#steps-to-add-a-new-setting)

## Public fields

| Name | Type | Details |
| --- | --- | --- |
| `Settings.Setting` | `enum` | Setting types |
| `Settings.SettingItem` | `class` | Setting item type |

## Setting item fields

| Name | Type | Details |
| --- | --- | --- |
| `SettingItem.Setting` | `Setting` | Setting type |
| `SettingItem.Value` | `string` | Setting value |
| `SettingItem.Valid` | `Func<string, bool>` | Setting value validator function delegate |

## Public functions

| Name | Parameter(s) | Return value | Details |
| --- | --- | --- | --- |
| `SettingManager.Get` | `Setting` | `string` | Retrieves the setting value for a setting type |
| `SettingManager.Reset` | | | Resets the settings to their original values |
| `SettingManager.Save` | | | Saves the current settings to the default JSON file |
| `SettingManager.Load` | | | Loads the repviously saved settings from the default JSON file |
| `SettingManager.Import` | `string` | | Loads the repviously saved settings from the specified JSON file |
| `SettingManager.OpenDialog` | | | Opens a dialog window where the settings can be set by hand |

The default file save location is `user\Documents\Mission Planner\MV04_settings.json`

## Steps to add a new setting

1. Add a new item to the `Settings.Setting` enum
2. *If the setting's value cannot be represented by a `string`, create a new setting type (`class`, `struct`)*
3. Add a new item to the `SettingManager.SettingCollection` dictionary in the `SettingManager.SettingManager` property getter (with the default value and validator function)
4. Add new UI input elements to the `SettingForm` dialog window
