using BlasII.ModdingAPI.Input;
using MelonLoader;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    [RegisterTypeInIl2Cpp]
    internal class MenuComponent : MonoBehaviour
    {
        private bool _closeNextFrame = false;

        private void Update()
        {
            if (_closeNextFrame)
            {
                _closeNextFrame = false;
                Main.ModdingAPI.MenuHandler.OnPressCancel();
                return;
            }

            if (PressedEnter)
            {
                Main.ModdingAPI.MenuHandler.OnPressEnter();
            }
            else if (PressedCancel)
            {
                _closeNextFrame = true;
            }
        }

        private bool PressedEnter => Main.ModdingAPI.InputHandler.GetButtonDown(ButtonType.UIConfirm);
        private bool PressedCancel => Main.ModdingAPI.InputHandler.GetButtonDown(ButtonType.UICancel);
    }
}
