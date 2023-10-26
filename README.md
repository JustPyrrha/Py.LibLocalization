![Static Badge](https://img.shields.io/badge/Demeo-v1.32-blue?style=flat-square)
![Discord](https://img.shields.io/discord/841011788195823626?style=flat-square&logo=discord&logoColor=white&link=https%3A%2F%2Fdiscord.gg%2FZDjjbRwzs4)
![GitHub release (with filter)](https://img.shields.io/github/v/release/JustPyrrha/Py.LibLocalization?style=flat-square&link=https%3A%2F%2Fgithub.com%2Forendain%2FDemeoMods%2Freleases%2Flatest)
![GitHub all releases](https://img.shields.io/github/downloads/JustPyrrha/Py.LibLocalization/total?style=flat-square)
# Py.LibLocalization

A simple library for adding custom localizations to [Demeo](https://www.resolutiongames.com/demeo).

## Installation
Download the [latest stable release](https://github.com/JustPyrrha/Py.LibLocalization/releases/latest)'s `Py.LibLocalization-version.zip` file
and extract it into your Demeo game folder.

You should have a folder structure that looks like this:
```
Demeo (or Demeo - PC Edition)/
├─ DemeoMods/
│  ├─ Py.LibLocalization/
│  │  ├─ 0Harmony.dll
│  │  ├─ Py.LibLocalization.dll
```

## Developer Guide
### Setup
Add dependencies for `ResolutionGames.Singleton.dll` (which can be found in Demeo's `demeo_Data/Managed` folder)
and `Py.LibLocalization.dll`

### Add a localization
> [!IMPORTANT]
> You should add your localizations before the built-in mod loader (Boardgame.Modding.ModLoader) calls `DemeoMod.Load` since that's when LibLocalization refreshes Demeo's localization list.\
> It's recommended to load them during `DemeoMod.OnEarlyInit` if you're using the built-in mod loader.\
> You can still add localizations after `DemeoMod.Load` has been called you'll just need to call `registry.Refresh()` but this is resource intensive and should generally be avoided since it reloads all localizations, vanilla (from Unity Resources) and modded (from LocaleRegistry).

1. Start by getting the singleton instance of LocalRegistry.
    ```csharp
    var registry = Fidelity.Singleton.Singleton<Py.LibLocalization.LocaleRegistry>.Instance;
    ```
2. Add localization(s)
   > [!WARNING]
   > Be careful when adding localizations since you can override vanilla localizations if you use an id already defined by Demeo.
   
   ```csharp
   // Add many
   registry.AddLocalizations(
       "en-US",
       new Dictionary<string, string>
       {
           { "Card/mycard/title", "My Custom Card" },
           { "Card/mycard/description", "Look at this card I made!" }
       }
   );
   
   // Add single
   registry.AddLocalization("en-US", "Card/mycard/title", "My Custom Card");
   ```
    If you're not sure what to use for the localization id, have a look at [default-localization-keys.txt](default-localization-keys.txt). It contains all the localization ids included in Demeo by default and should give you a general idea on how to format your ids.


### Frequently Asked Questions
   - Q: Why does the download include `0Harmony.dll`?\
     A: Since we use the built-in mod loader we need to load Lib.Harmony ourselves. If it's already loaded by a third-party mod loader the dll will be ignored.