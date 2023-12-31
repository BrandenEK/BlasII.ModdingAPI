﻿using Il2CppTGK.Game.Components.UI;
using Il2CppTMPro;
using UnityEngine;

namespace BlasII.ModdingAPI.UI
{
    /// <summary>
    /// Useful methods to modify TextMeshProUGUIs
    /// </summary>
    public static class TextExtensions
    {
        /// <summary>
        /// Turns the text object into a ShadowPixelText component
        /// </summary>
        public static UIPixelTextWithShadow AddShadow(this TextMeshProUGUI text)
        {
            // Create new overhead text
            var newText = UIModder.Create(new RectCreationOptions()
            {
                Name = "Text",
                Parent = text.transform,
                Position = new Vector2(0, 4),
                Size = text.rectTransform.sizeDelta
            }).AddText(text.CopyOptions());

            // Update old shadow text
            text.rectTransform.ChangePosition(0, -2);
            text.SetColor(new Color(0.004f, 0.008f, 0.008f));

            // Add pixel text component
            UIPixelTextWithShadow shadow = text.gameObject.AddComponent<UIPixelTextWithShadow>();
            shadow.normalText = newText;
            shadow.shadowText = text;
            return shadow;
        }

        internal static TextMeshProUGUI ApplyOptions(this TextMeshProUGUI text, TextCreationOptions options)
        {
            text.text = options.Contents;
            text.color = options.Color;
            text.fontSize = options.FontSize;
            text.alignment = options.Alignment;
            text.enableWordWrapping = options.WordWrap;
            text.font = options.Font;
            return text;
        }

        internal static TextCreationOptions CopyOptions(this TextMeshProUGUI text)
        {
            return new TextCreationOptions()
            {
                Contents = text.text,
                Color = text.color,
                FontSize = text.fontSize,
                Alignment = text.alignment,
                WordWrap = text.enableWordWrapping,
                Font = text.font
            };
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
    }
}
