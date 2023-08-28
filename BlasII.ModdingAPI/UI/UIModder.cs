using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    internal class UIModder
    {
        public static RectTransform CreateRect(string name)
        {
            var rect = new GameObject(name).AddComponent<RectTransform>().ResetToDefault();
            return rect;
        }

        public static Image CreateImage(string name)
        {
            var rect = CreateRect(name);
            return rect.AddImage();
        }

        public static TextMeshProUGUI CreateText(string name)
        {
            var rect = CreateRect(name);
            return rect.AddText();
        }
    }
}
