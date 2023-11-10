using System;
using System.Collections.Generic;
using Fidelity.Localization;
using Fidelity.Singleton;
using HarmonyLib;

namespace Py.LibLocalization.Internal.Utils
{
    public static class ModPatches
    {
        public static void Patch()
        {
            var harmony = new Harmony("Py.LibLocalization");
            harmony.Patch(
                original: AccessTools.Method(typeof(Locale), "TryLoadStrings"),
                postfix: new HarmonyMethod(typeof(ModPatches), nameof(Locale_TryLoadStrings_Postfix))
            );
        }
        
        private static void Locale_TryLoadStrings_Postfix(
            // ReSharper disable InconsistentNaming
            string ___currentLocale,
            ref Dictionary<string, string> ___strings
            // ReSharper restore InconsistentNaming
        )
        {
            var modStrings = Singleton<LocaleRegistry>.Instance.GetLocalizationsForLocale(___currentLocale);
            foreach (var (id, localization) in modStrings)
            {
                ___strings[id] = localization;
            }
            ModLog.Log($"Loaded {modStrings.Count} custom localizations for {___currentLocale}.");
        }
    }
}