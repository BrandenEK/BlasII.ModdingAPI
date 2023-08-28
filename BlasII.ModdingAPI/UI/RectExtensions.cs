using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    public static class RectExtensions
    {
        public static Image AddImage(this RectTransform rect)
        {
            return rect.gameObject.AddComponent<Image>();
        }

        public static TextMeshProUGUI AddText(this RectTransform rect)
        {
            return rect.gameObject.AddComponent<TextMeshProUGUI>();
        }

        public static RectTransform SetParent(this RectTransform rect, Transform parent)
        {
            rect.SetParent(parent, false);
            return rect;
        }

        public static RectTransform SetXRange(this RectTransform rect, Vector2 anchor)
        {
            rect.anchorMin = new Vector2(anchor.x, rect.anchorMin.y);
            rect.anchorMax = new Vector2(anchor.y, rect.anchorMax.y);
            return rect;
        }

        public static RectTransform SetYRange(this RectTransform rect, Vector2 anchor)
        {
            rect.anchorMin = new Vector2(rect.anchorMin.x, anchor.x);
            rect.anchorMax = new Vector2(rect.anchorMax.x, anchor.y);
            return rect;
        }

        public static RectTransform SetPosition(this RectTransform rect, Vector2 position)
        {
            rect.anchoredPosition = position;
            return rect;
        }
    }
}
