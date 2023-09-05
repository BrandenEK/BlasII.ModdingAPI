using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    public static class UIModder
    {
        private static RectTransform _defaultParent;
        private static TMP_FontAsset _defaultFont;

        internal static RectTransform DefaultParent
        {
            get
            {
                if (_defaultParent == null)
                    Main.ModdingAPI.LogError("Default ui parent has not been stored yet!");
                return _defaultParent;
            }
            set
            {
                if (_defaultParent == null)
                    _defaultParent = value;
            }
        }

        internal static TMP_FontAsset DefaultFont
        {
            get
            {
                if (_defaultFont == null)
                    Main.ModdingAPI.LogError("Default ui font has not been stored yet!");
                return _defaultFont;
            }
            set
            {
                if (_defaultFont == null)
                    _defaultFont = value;
            }
        }

        public static RectTransform CanvasParent => DefaultParent;

        public static RectTransform MainMenuParent => DefaultParent?.Find("Interfaces/MainMenuWindow_prefab(Clone)")?.GetComponent<RectTransform>();

        public static RectTransform GameLogicParent => DefaultParent?.Find("InGame/InGameWindow_prefab(Clone)")?.GetComponent<RectTransform>();

        public static RectTransform CreateRect(string name, Transform parent)
        {
            var rect = new GameObject(name).AddComponent<RectTransform>();
            rect.SetParent(parent, false);
            // Maybe set ui layer ?
            return rect.ResetToDefault();
        }

        public static RectTransform CreateRect(string name) => CreateRect(name, DefaultParent);


        public static Image CreateImage(string name, Transform parent)
        {
            var rect = CreateRect(name, parent);
            return rect.AddImage();
        }

        public static Image CreateImage(string name) => CreateImage(name, DefaultParent);


        public static TextMeshProUGUI CreateText(string name, Transform parent)
        {
            var rect = CreateRect(name, parent);
            return rect.AddText();
        }

        public static TextMeshProUGUI CreateText(string name) => CreateText(name, DefaultParent);
    }
}
