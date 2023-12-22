using Il2CppTGK.Game.Components.UI;
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
        /// Copies the properties from the other TextMeshProUGUI
        /// </summary>
        public static TextMeshProUGUI CopyFrom(this TextMeshProUGUI text, TextMeshProUGUI other)
        {
            return text
                .SetContents(other.text)
                .SetColor(other.color)
                .SetFontSize(other.fontSize)
                .SetAlignment(other.alignment)
                .SetWrapping(other.enableWordWrapping)
                .SetFont(other.font);
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
            sb.AppendLine("Wrapping: " + text.enableWordWrapping);
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

        /// <summary>
        /// Turns the text object into a ShadowPixelText component
        /// </summary>
        public static UIPixelTextWithShadow AddShadow(this TextMeshProUGUI text)
        {
            // Create new overhead text
            var newText = UIModder.CreateRect("Text", text.transform)
                .CopyFrom(text.rectTransform).SetPosition(0, 4)
                .AddText().CopyFrom(text);

            // Update old shadow text
            text.rectTransform.ChangePosition(0, -2);
            text.SetColor(new Color(0.004f, 0.008f, 0.008f));

            // Add pixel text component
            UIPixelTextWithShadow shadow = text.gameObject.AddComponent<UIPixelTextWithShadow>();
            shadow.normalText = newText;
            shadow.shadowText = text;
            return shadow;
        }
    }
}
