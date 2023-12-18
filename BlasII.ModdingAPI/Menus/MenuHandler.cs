using BlasII.ModdingAPI.Audio;
using BlasII.ModdingAPI.Input;
using BlasII.ModdingAPI.Utils;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    internal class MenuHandler
    {
        // Temporary settings
        private int _currentSlot = 0;
        private bool _closeNextFrame = false;

        // Menu objects
        private readonly ObjectCache<MainMenuWindowLogic> _mainMenuCache;
        private readonly ObjectCache<GameObject> _slotsMenuCache;

        public bool IsMenuActive => false; // fix !!!
        public bool AllowGameStart { get; private set; }
        private bool PressedEnter => Main.ModdingAPI.InputHandler.GetButtonDown(ButtonType.UIConfirm);
        private bool PressedCancel => Main.ModdingAPI.InputHandler.GetButtonDown(ButtonType.UICancel);

        public MenuHandler()
        {
            _mainMenuCache = new(() => Object.FindObjectOfType<MainMenuWindowLogic>());
            _slotsMenuCache = new(() => _mainMenuCache.Value.slotsMenuView.transform.parent.gameObject);
        }

        public void Update()
        {
            //Cursor.visible = IsMenuActive;
            if (!IsMenuActive) return;

            if (_closeNextFrame)
            {
                _closeNextFrame = false;
                OnCancelMenu();
                //_menuList.ShowPreviousMenu();
                return;
            }

            if (PressedEnter)
            {
                //_menuList.ShowNextMenu();
                OnFinishMenu();
            }
            else if (PressedCancel)
            {
                _closeNextFrame = true;
            }
        }

        public void OnPressNewGame(int slot)
        {
            _currentSlot = slot;
            //if (_menuList.IsEmpty)
            //{
            //    OnFinishMenu();
            //    return;
            //}

            StartMenu();
        }

        public void OnPressLoadGame(int slot)
        {
            _currentSlot = slot;
            //if (_menuList.IsEmpty)
            //{
            //    OnFinishMenu();
            //    return;
            //}

            StartMenu();
        }

        /// <summary>
        /// Whenever new game or continue is pressed, open the menus
        /// </summary>
        private void StartMenu()
        {
            Main.ModdingAPI.Log("Start settings menu");
            Cursor.visible = true;

            _slotsMenuCache.Value.SetActive(false);
            CoreCache.Input.ClearAllInputBlocks();

            //_menuList.StartMenu();
        }

        /// <summary>
        /// Whenever 'B' is pressed on the first menu, return to slots screen
        /// </summary>
        private void OnCancelMenu()
        {
            Main.ModdingAPI.Log("Cancel settings menu");
            Cursor.visible = false;

            _mainMenuCache.Value.OpenSlotMenu();
            _mainMenuCache.Value.slotsList.SelectElement(_currentSlot);
        }

        /// <summary>
        /// Whenever 'A' is pressed on the final menu, actually start the game
        /// </summary>
        private void OnFinishMenu()
        {
            Main.ModdingAPI.Log("Finish settings menu");
            Cursor.visible = false;

            Main.ModdingAPI.AudioHandler.PlayEffectUI(UISFX.OpenMenu);

            AllowGameStart = true;
            _mainMenuCache.Value.NewGame(_currentSlot);
            AllowGameStart = false;
        }
    }
}
