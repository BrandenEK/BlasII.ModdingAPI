using BlasII.ModdingAPI.Input;
using MelonLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    [RegisterTypeInIl2Cpp]
    internal class MenuComponent : MonoBehaviour
    {
        private bool _closeNextFrame = false;
        private Clickable _clickedSetting = null;

        private readonly List<Clickable> _clickables = new();
        private ICursorController _cursorController;

        private void OnEnable()
        {
            _cursorController ??= _clickables.Count > 0
                ? new RealCursor(transform)
                : new FakeCursor();
        }

        private void OnDisable()
        {
            _clickedSetting?.OnUnclick();
            _clickedSetting = null;
        }

        private void Update()
        {
            if (_closeNextFrame)
            {
                _closeNextFrame = false;
                MenuModder.OnPressCancel();
                return;
            }

            _cursorController.UpdatePosition(UnityEngine.Input.mousePosition);

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                HandleClick();
            }

            if (PressedEnter)
            {
                MenuModder.OnPressEnter();
            }
            else if (PressedCancel)
            {
                _closeNextFrame = true;
            }
        }

        private void HandleClick()
        {
            _clickedSetting?.OnUnclick();
            _clickedSetting = null;

            foreach (var click in _clickables)
            {
                if (click.Rect.OverlapsPoint(UnityEngine.Input.mousePosition))
                {
                    _clickedSetting = click;
                    click.OnClick();
                    break;
                }
            }
        }

        public void AddClickable(RectTransform rect, Action onClick, Action onUnclick)
        {
            _clickables.Add(new Clickable(rect, onClick, onUnclick));
        }

        private bool PressedEnter => Main.ModdingAPI.InputHandler.GetButtonDown(ButtonType.UIConfirm);
        private bool PressedCancel => Main.ModdingAPI.InputHandler.GetButtonDown(ButtonType.UICancel);

        class Clickable
        {
            public RectTransform Rect { get; }
            private readonly Action _onClick;
            private readonly Action _onUnclick;

            internal void OnClick() => _onClick?.Invoke();

            internal void OnUnclick() => _onUnclick?.Invoke();

            public Clickable(RectTransform rect, Action onClick, Action onUnclick)
            {
                Rect = rect;
                _onClick = onClick;
                _onUnclick = onUnclick;
            }
        }
    }
}
