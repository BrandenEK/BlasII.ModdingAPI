using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    /// <summary>
    /// Useful methods to modify Images
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Resets sprite and color to default
        /// </summary>
        public static Image ResetToDefault(this Image image)
        {
            return image
                .SetSprite(null)
                .SetColor(Color.white);
        }

        /// <summary>
        /// Copies the properties from the other Image
        /// </summary>
        public static Image CopyFrom(this Image image, Image other)
        {
            return image
                .SetSprite(other.sprite)
                .SetColor(other.color);
        }

        /// <summary>
        /// Displays the properties of the UI
        /// </summary>
        public static string DisplayProperties(this Image image)
        {
            var sb = new StringBuilder("\n\n");
            sb.AppendLine("Sprite: " + image.sprite?.name ?? "null");
            sb.AppendLine("Color: " + image.color);
            return sb.ToString();
        }

        /// <summary> Updates the sprite </summary>
        public static Image SetSprite(this Image image, Sprite sprite)
        {
            image.sprite = sprite;
            return image;
        }

        /// <summary> Updates the color </summary>
        public static Image SetColor(this Image image, Color color)
        {
            image.color = color;
            return image;
        }
    }
}
