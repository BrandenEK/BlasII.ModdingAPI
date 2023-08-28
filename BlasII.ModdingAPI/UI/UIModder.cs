using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    internal class UIModder
    {
        public static RectTransform CreateRect(string name)
        {
            var obj = new GameObject(name);
            
            var rect = obj.AddComponent<RectTransform>();
            return rect;
        }

        public static Image CreateImage(string name)
        {
            var rect = CreateRect(name);

            var image = rect.gameObject.AddComponent<Image>();
            return image;
        }

        public static TextMeshProUGUI CreateText(string name)
        {
            var rect = CreateRect(name);

            var text = rect.gameObject.AddComponent<TextMeshProUGUI>();
            return text;
        }
    }
}
