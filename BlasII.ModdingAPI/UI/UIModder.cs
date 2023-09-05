using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    public static class UIModder
    {
        public static Fonts Fonts { get; private set; } = new();

        public static Parents Parents { get; private set; } = new();

        public static RectTransform CreateRect(string name, Transform parent)
        {
            var rect = new GameObject(name).AddComponent<RectTransform>();
            rect.SetParent(parent, false);
            return rect.ResetToDefault();
        }

        public static RectTransform CreateRect(string name) => CreateRect(name, Parents.Default);


        public static Image CreateImage(string name, Transform parent)
        {
            var rect = CreateRect(name, parent);
            return rect.AddImage();
        }

        public static Image CreateImage(string name) => CreateImage(name, Parents.Default);


        public static TextMeshProUGUI CreateText(string name, Transform parent)
        {
            var rect = CreateRect(name, parent);
            return rect.AddText();
        }

        public static TextMeshProUGUI CreateText(string name) => CreateText(name, Parents.Default);
    }

    public class Fonts
    {
        internal TMP_FontAsset Default => Blasphemous;

        public TMP_FontAsset Blasphemous { get; internal set; }

        public TMP_FontAsset Arial { get; internal set; }

        internal void Initialize()
        {
            Blasphemous = Object.FindObjectOfType<TextMeshProUGUI>()?.font;
            Arial = TMP_FontAsset.CreateFontAsset(Resources.GetBuiltinResource<Font>("Arial.ttf"));
        }
    }

    public class Parents
    {
        internal Transform Default => Canvas;

        public Transform Canvas { get; internal set; }

        public Transform MainMenu => Canvas?.Find("Interfaces/MainMenuWindow_prefab(Clone)");

        public Transform GameLogic => Canvas?.Find("InGame/InGameWindow_prefab(Clone)");

        internal void Initialize()
        {
            Canvas = Object.FindObjectOfType<CanvasScaler>()?.transform;
        }
    }
}
