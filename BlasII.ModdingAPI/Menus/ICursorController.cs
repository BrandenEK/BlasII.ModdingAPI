﻿using BlasII.ModdingAPI.UI;
using System;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    internal interface ICursorController
    {
        public void UpdatePosition(Vector2 mousePosition);
    }

    internal class RealCursor : ICursorController
    {
        private readonly RectTransform _cursor;

        public RealCursor(Transform menu)
        {
            _cursor = UIModder.Create(new RectCreationOptions()
            {
                Name = "Cursor",
                Parent = menu,
                XRange = Vector2.zero,
                YRange = Vector2.zero,
                Pivot = new Vector2(0, 1),
                Size = new Vector2(40, 40)
            }).AddImage(new ImageCreationOptions()
            {
                Sprite = Main.ModdingAPI.CursorIcon
            })
            .rectTransform;
        }

        public void UpdatePosition(Vector2 mousePosition)
        {
            Vector2 cursorPosition = new(
                Math.Clamp(mousePosition.x, 0, Screen.width - _cursor.sizeDelta.x / 2) / Screen.width * 1920,
                Math.Clamp(mousePosition.y, 0 + _cursor.sizeDelta.y / 2, Screen.height) / Screen.height * 1080);

            _cursor.anchoredPosition = cursorPosition;
        }
    }

    internal class FakeCursor : ICursorController
    {
        public void UpdatePosition(Vector2 mousePosition) { }
    }
}
