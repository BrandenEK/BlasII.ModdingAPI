using Il2CppI2.Loc;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using Il2CppTMPro;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.ModdingAPI.Localization
{
    /// <summary>
    /// Provides access to automatic localization based on selected language
    /// </summary>
    public class LocalizationHandler
    {
        private readonly BlasIIMod _mod;

        private readonly Dictionary<string, Dictionary<string, string>> _textByLanguage = new();
        private readonly List<ILocalizer> _localizers = new();

        private string _defaultLanguage = string.Empty;

        internal LocalizationHandler(BlasIIMod mod)
        {
            _mod = mod;
        }

        /// <summary>
        /// Whenever the language is changed, update the text of any existing localizers
        /// </summary>
        internal void OnLangaugeChanged()
        {
            for (int i = 0; i < _localizers.Count; i++)
            {
                if (!_localizers[i].Localize(this))
                {
                    _localizers.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Localizes the key into its term in the current language
        /// </summary>
        public string Localize(string key)
        {
            string currentLanguage = CoreCache.Localization.CurrentLanguageCode;

            // The language exists and contains the specified key
            if (_textByLanguage.ContainsKey(currentLanguage) && _textByLanguage[currentLanguage].ContainsKey(key))
            {
                return _textByLanguage[currentLanguage][key];
            }

            // The language doesn't exist - use default language
            if (_textByLanguage.ContainsKey(_defaultLanguage) && _textByLanguage[_defaultLanguage].ContainsKey(key))
            {
                return _textByLanguage[_defaultLanguage][key];
            }

            _mod.LogError($"Failed to localize '{key}' to any language.");
            return ERROR_TEXT;
        }

        /// <summary>
        /// Registers this text object to be localized whenever the current language changes
        /// </summary>
        public void AddTMProLocalizer(TMP_Text text, string key)
        {
            RemoveVanillaLocalizers(text.gameObject);

            var localizer = new LocalizeTMPro(text, key);
            localizer.Localize(this);
            _localizers.Add(localizer);
        }

        /// <summary>
        /// Registers this text object to be localized whenever the current language changes
        /// </summary>
        public void AddPixelTextLocalizer(UIPixelTextWithShadow text, string key)
        {
            RemoveVanillaLocalizers(text.gameObject);

            var localizer = new LocalizePixelText(text, key);
            localizer.Localize(this);
            _localizers.Add(localizer);
        }

        /// <summary>
        /// Finds any vanilla localizers on a gameobject and destroys them
        /// </summary>
        private void RemoveVanillaLocalizers(GameObject obj)
        {
            var localize = obj.GetComponent<Localize>();
            if (localize != null)
                Object.Destroy(localize);
        }

        /// <summary>
        /// Specifies which language to default to and loads the translations
        /// </summary>
        public void RegisterDefaultLanguage(string languageKey)
        {
            _defaultLanguage = languageKey;
            DeserializeLocalization(_mod.FileHandler.LoadLocalization());
        }

        /// <summary>
        /// Takes in the lines from the localization file and fills the text dictionary
        /// </summary>
        private void DeserializeLocalization(string[] localization)
        {
            string currLanguage = null;
            foreach (string line in localization)
            {
                // Skip lines without a colon
                int colon = line.IndexOf(':');
                if (colon < 0)
                    continue;

                // Get key and term for each pair
                string key = line[..colon].Trim();
                string term = line[(colon + 1)..].Trim();

                // Possibly set new language
                if (key == "lang")
                {
                    currLanguage = term;
                    _textByLanguage.Add(term, new Dictionary<string, string>());
                    continue;
                }

                // Make sure the current language has been set
                if (currLanguage == null)
                    continue;

                _textByLanguage[currLanguage].Add(key, term.Replace("\\n", "\n"));
            }
        }

        private const string ERROR_TEXT = "LOC_ERROR";
    }
}
