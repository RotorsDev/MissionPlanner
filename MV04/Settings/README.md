# MV04 settings manager<!-- omit from toc -->

1. [Fields](#fields)
2. [Functions](#functions)
3. [Steps to add a new setting](#steps-to-add-a-new-setting)

## Fields

| Name | Type | Details |
| --- | --- | --- |
| `Settings.Settings` | `enum` | Setting types |
| `Settings.SettingCollection` | `Dictionary<string, object>` | Setting key-value collection |

## Functions

| Name | Parameter(s) | Return value | Details |
| --- | --- | --- | --- |
| `SettingManager.SettingManager` | | SettingManager instance | Creates a new SettignManager instance |
| `SettingManager.Load` | | | Loads the repviously saved settings from a JSON file |
| `SettingManager.Save` | | | Saves the current settings to a JSON file |
| `SettingManager.OpenDialog` | | | Opens a dialog window where the settings can be set by hand |

The file save location is `user\Documents\Mission Planner\MV04_settings.json`

## Steps to add a new setting

1. Add a new item to the `Settings.Settings` enum
2. *Optionally, create a new setting type (class, structure) in the `SettingManager` class*
3. Add a new item to the `Settings.SettingCollection` dictionary in the `SettingManager.SettingManager` class constructor (with the default value)
4. Add new UI input elements to the `SettingForm` dialog window
