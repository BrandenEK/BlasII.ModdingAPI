﻿using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    public static class ImageExtensions
    {
        public static Image ResetToDefault(this Image image)
        {
            return image
                .SetSprite(null)
                .SetColor(Color.white);
        }

        public static Image SetSprite(this Image image, Sprite sprite)
        {
            image.sprite = sprite;
            return image;
        }

        public static Image SetColor(this Image image, Color color)
        {
            image.color = color;
            return image;
        }
    }
}