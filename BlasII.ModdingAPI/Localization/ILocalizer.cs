using Il2CppI2.Loc;
using Il2CppTGK.Game.Components.UI;
using Il2CppTMPro;
using UnityEngine;

namespace BlasII.ModdingAPI.Localization
{
    internal interface ILocalizer
    {
        public bool Localize(LocalizationHandler handler);
    }

    internal class LocalizeTMPro : ILocalizer
    {
        private readonly TMP_Text _text;
        private readonly string _key;

        public LocalizeTMPro(TMP_Text text, string key)
        {
            _text = text;
            _key = key;

            RemoveVanillaLocalizers();
        }

        /// <summary>
        /// Updates the text of the tmpro to the localized term
        /// </summary>
        public bool Localize(LocalizationHandler handler)
        {
            if (_text == null)
                return false;

            _text.text = handler.Localize(_key);
            return true;
        }

        /// <summary>
        /// Finds any vanilla localizers on the gameobject and destroys them
        /// </summary>
        private void RemoveVanillaLocalizers()
        {
            var localize = _text.gameObject.GetComponent<Localize>();
            if (localize != null)
                Object.Destroy(localize);
        }
    }

    internal class LocalizePixelText : ILocalizer
    {
        private readonly UIPixelTextWithShadow _text;
        private readonly string _key;

        public LocalizePixelText(UIPixelTextWithShadow text, string key)
        {
            _text = text;
            _key = key;

            RemoveVanillaLocalizers();
        }

        /// <summary>
        /// Updates the text of the pixeltext to the localized term
        /// </summary>
        public bool Localize(LocalizationHandler handler)
        {
            if (_text == null)
                return false;

            _text.SetText(handler.Localize(_key));
            return true;
        }

        /// <summary>
        /// Finds any vanilla localizers on the gameobject and destroys them
        /// </summary>
        private void RemoveVanillaLocalizers()
        {
            var localize = _text.gameObject.GetComponent<Localize>();
            if (localize != null)
                Object.Destroy(localize);
        }
    }
}
