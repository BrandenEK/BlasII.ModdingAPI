using Il2CppTMPro;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    /// <summary>
    /// Useful methods to modify RectTransforms
    /// </summary>
    public static class RectExtensions
    {
        /// <summary>
        /// Adds an Image component to the UI
        /// </summary>
        public static Image AddImage(this RectTransform rect)
        {
            return rect.gameObject.AddComponent<Image>().ResetToDefault();
        }

        /// <summary>
        /// Adds a TextMeshProUGUI component to the UI
        /// </summary>
        public static TextMeshProUGUI AddText(this RectTransform rect)
        {
            return rect.gameObject.AddComponent<TextMeshProUGUI>().ResetToDefault();
        }

        /// <summary>
        /// Resets xrange, yrange, pivot, position, and size to default
        /// </summary>
        public static RectTransform ResetToDefault(this RectTransform rect)
        {
            return rect
                .SetXRange(0.5f, 0.5f)
                .SetYRange(0.5f, 0.5f)
                .SetPivot(0.5f, 0.5f)
                .SetPosition(0, 0)
                .SetSize(100, 100);
        }

        /// <summary>
        /// Displays the properties of the UI
        /// </summary>
        public static string DisplayProperties(this RectTransform rect)
        {
            var sb = new StringBuilder("\n\n");
            sb.AppendLine("X range: " + new Vector2(rect.anchorMin.x, rect.anchorMax.x));
            sb.AppendLine("Y range: " + new Vector2(rect.anchorMin.y, rect.anchorMax.y));
            sb.AppendLine("Pivot: " + rect.pivot);
            sb.AppendLine("Position: " + rect.anchoredPosition);
            sb.AppendLine("Size: " + rect.sizeDelta);
            return sb.ToString();
        }

        /// <summary> Updates the x anchors </summary>
        public static RectTransform SetXRange(this RectTransform rect, Vector2 range)
        {
            rect.anchorMin = new Vector2(range.x, rect.anchorMin.y);
            rect.anchorMax = new Vector2(range.y, rect.anchorMax.y);
            return rect;
        }

        /// <summary> Updates the x anchors </summary>
        public static RectTransform SetXRange(this RectTransform rect, float xMin, float xMax) => rect.SetXRange(new Vector2(xMin, xMax));

        /// <summary> Updates the y anchors </summary>
        public static RectTransform SetYRange(this RectTransform rect, Vector2 range)
        {
            rect.anchorMin = new Vector2(rect.anchorMin.x, range.x);
            rect.anchorMax = new Vector2(rect.anchorMax.x, range.y);
            return rect;
        }

        /// <summary> Updates the y anchors </summary>
        public static RectTransform SetYRange(this RectTransform rect, float yMin, float yMax) => rect.SetYRange(new Vector2(yMin, yMax));

        /// <summary> Updates the pivot </summary>
        public static RectTransform SetPivot(this RectTransform rect, Vector2 pivot)
        {
            rect.pivot = pivot;
            return rect;
        }

        /// <summary> Updates the pivot </summary>
        public static RectTransform SetPivot(this RectTransform rect, float xPivot, float yPivot) => rect.SetPivot(new Vector2(xPivot, yPivot));

        /// <summary> Updates the x and y position </summary>
        public static RectTransform SetPosition(this RectTransform rect, Vector2 position)
        {
            rect.anchoredPosition = position;
            return rect;
        }

        /// <summary> Updates the x and y position </summary>
        public static RectTransform SetPosition(this RectTransform rect, float xPosition, float yPosition) => rect.SetPosition(new Vector2(xPosition, yPosition));

        /// <summary> Updates the width and height </summary>
        public static RectTransform SetSize(this RectTransform rect, Vector2 size)
        {
            rect.sizeDelta = size;
            return rect;
        }

        /// <summary> Updates the width and height </summary>
        public static RectTransform SetSize(this RectTransform rect, float xSize, float ySize) => rect.SetSize(new Vector2(xSize, ySize));
    }
}
