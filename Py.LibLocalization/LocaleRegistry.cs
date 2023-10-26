using System.Collections.Generic;
using Fidelity.Localization;
using Fidelity.Singleton;

namespace Py.LibLocalization
{
    /// <summary>
    /// Registry for modded localizations.
    /// </summary>
    public class LocaleRegistry : Singleton<LocaleRegistry>
    {
        // <locale, <id, localization>>
        private readonly Dictionary<string, Dictionary<string, string>> _internalRegistry = new();

        /// <summary>
        /// Get all registered localizations matching a locale.
        /// </summary>
        /// <param name="locale">locale to get localizations for</param>
        /// <returns>custom localizations matching the provided locale</returns>
        public Dictionary<string, string> GetLocalizationsForLocale(string locale) =>
            !_internalRegistry.ContainsKey(locale.ToLower())
                ? new Dictionary<string, string>()
                : _internalRegistry[locale.ToLower()];

        /// <summary>
        /// Register a mod's localizations.
        /// </summary>
        /// <param name="locale">locale key (e.g. en-US)</param>
        /// <param name="localizations">A dictionary of stringId, localization for locale.</param>
        public void AddLocalizations(string locale, Dictionary<string, string> localizations)
        {
            foreach (var (id, localization) in localizations)
            {
                AddLocalization(locale, id, localization);
            }
        }
        
        /// <summary>
        /// Register a single localization.
        /// </summary>
        /// <param name="locale">locale key (e.g. en-US)</param>
        /// <param name="id">stringId for the localization</param>
        /// <param name="localization">localization matching the stringId and locale.</param>
        public void AddLocalization(string locale, string id, string localization)
        {
            if (!_internalRegistry.ContainsKey(locale.ToLower()))
            {
                _internalRegistry[locale.ToLower()] = new Dictionary<string, string>();
            }
            
            _internalRegistry[locale.ToLower()].Add(id, localization);
        }

        /// <summary>
        /// Refreshes Demeo's locale registry.
        /// Note: Py.Localizations will call this during DemeoMod.Load so you shouldn't have to.
        /// </summary>
        public void Refresh()
        {
            Singleton<Locale>.Instance.Refresh();
        }
    }
}