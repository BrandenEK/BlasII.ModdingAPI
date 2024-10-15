using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI;

/// <summary>
/// Useful methods to modify Images
/// </summary>
public static class ImageExtensions
{
    internal static Image ApplyOptions(this Image image, ImageCreationOptions options)
    {
        image.sprite = options.Sprite;
        image.color = options.Color;
        return image;
    }

    internal static ImageCreationOptions CopyOptions(this Image image)
    {
        return new ImageCreationOptions()
        {
            Sprite = image.sprite,
            Color = image.color
        };
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
