using Il2CppTGK.Game;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Localization
{
    public class LocalizationHandler
    {
        private readonly Dictionary<string, Dictionary<string, string>> _textByLanguage = new();

        public LocalizationHandler(string[] localization)
        {
            LoadLocalization(localization);
        }

        /// <summary>
        /// Takes in the lines from the localization file and fills the text dictionary
        /// </summary>
        private void LoadLocalization(string[] localization)
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

        /// <summary>
        /// Whenever the language is changed, update the text of any existing localizers
        /// </summary>
        internal void OnLangaugeChanged()
        {
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

            // The language doesn't exist - default to english
            if (_textByLanguage.ContainsKey("en") && _textByLanguage["en"].ContainsKey(key))
            {
                return _textByLanguage["en"][key];
            }

            return ERROR_TEXT;
        }

        private const string ERROR_TEXT = "LOC_ERROR";
    }
}
