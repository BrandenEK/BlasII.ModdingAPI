using Il2CppTMPro;
using System.Text;
using UnityEngine;

namespace BlasII.ModdingAPI.UI
{
    public static class TextExtensions
    {
        public static TextMeshProUGUI ResetToDefault(this TextMeshProUGUI text)
        {
            return text
                .SetContents(string.Empty)
                .SetColor(Color.white)
                .SetFontSize(16)
                .SetAlignment(TextAlignmentOptions.Center)
                .SetOverflow(TextOverflowModes.Overflow)
                .SetFont(UIModder.DefaultFont);
        }

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

        public static TextMeshProUGUI SetContents(this TextMeshProUGUI text, string contents)
        {
            text.text = contents;
            return text;
        }

        public static TextMeshProUGUI SetColor(this TextMeshProUGUI text, Color color)
        {
            text.color = color;
            return text;
        }

        public static TextMeshProUGUI SetFontSize(this TextMeshProUGUI text, float size)
        {
            text.fontSize = size;
            return text;
        }

        public static TextMeshProUGUI SetAlignment(this TextMeshProUGUI text, TextAlignmentOptions alignment)
        {
            text.alignment = alignment;
            return text;
        }

        public static TextMeshProUGUI SetOverflow(this TextMeshProUGUI text, TextOverflowModes overflow)
        {
            text.overflowMode = overflow;
            return text;
        }

        public static TextMeshProUGUI SetFont(this TextMeshProUGUI text, TMP_FontAsset font)
        {
            text.font = font;
            return text;
        }
    }
}
