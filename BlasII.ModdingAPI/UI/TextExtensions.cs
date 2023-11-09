using Il2CppTMPro;
using System.Text;
using UnityEngine;

namespace BlasII.ModdingAPI.UI
{
    /// <summary>
    /// Useful methods to modify TextMeshProUGUIs
    /// </summary>
    public static class TextExtensions
    {
        /// <summary>
        /// Resets contents, color, size, alignment, wrapping, and font to default
        /// </summary>
        public static TextMeshProUGUI ResetToDefault(this TextMeshProUGUI text)
        {
            return text
                .SetContents(string.Empty)
                .SetColor(Color.white)
                .SetFontSize(16)
                .SetAlignment(TextAlignmentOptions.Center)
                .SetWrapping(false)
                .SetFont(UIModder.Fonts.Default);
        }

        /// <summary>
        /// Displays the properties of the UI
        /// </summary>
        public static string DisplayProperties(this TextMeshProUGUI text)
        {
            var sb = new StringBuilder("\n\n");
            sb.AppendLine("Contents: " + text.text);
            sb.AppendLine("Color: " + text.color);
            sb.AppendLine("Font size: " + text.fontSize);
            sb.AppendLine("Alignment: " + text.alignment);
            sb.AppendLine("Overflow: " + text.overflowMode);
            sb.AppendLine("Font: " + text.font?.name ?? "null");
            return sb.ToString();
        }

        /// <summary> Updates the contents </summary>
        public static TextMeshProUGUI SetContents(this TextMeshProUGUI text, string contents)
        {
            text.text = contents;
            return text;
        }

        /// <summary> Updates the color </summary>
        public static TextMeshProUGUI SetColor(this TextMeshProUGUI text, Color color)
        {
            text.color = color;
            return text;
        }

        /// <summary> Updates the font size </summary>
        public static TextMeshProUGUI SetFontSize(this TextMeshProUGUI text, float size)
        {
            text.fontSize = size;
            return text;
        }

        /// <summary> Updates the alignment mode </summary>
        public static TextMeshProUGUI SetAlignment(this TextMeshProUGUI text, TextAlignmentOptions alignment)
        {
            text.alignment = alignment;
            return text;
        }

        /// <summary> Failed to update the wrapping mode </summary>
        [System.Obsolete("This did the wrong thing.  Use SetWrapping instead")]
        public static TextMeshProUGUI SetOverflow(this TextMeshProUGUI text, TextOverflowModes overflow)
        {
            text.overflowMode = overflow;
            return text;
        }

        /// <summary> Updates the wrapping mode </summary>
        public static TextMeshProUGUI SetWrapping(this TextMeshProUGUI text, bool wordWrap)
        {
            text.enableWordWrapping = wordWrap;
            return text;
        }

        /// <summary> Updates the font asset </summary>
        public static TextMeshProUGUI SetFont(this TextMeshProUGUI text, TMP_FontAsset font)
        {
            text.font = font;
            return text;
        }
    }
}
