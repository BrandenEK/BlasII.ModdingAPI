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
        private bool _isNewGame = false;
        private int _currentSlot = 0;
        private bool _closeNextFrame = false;

        // Menu objects
        private readonly ObjectCache<MainMenuWindowLogic> _mainMenuCache;
        private readonly ObjectCache _slotsMenuCache;

        // Menu lists
        public bool IsMenuActive => CurrentMenuList.IsActive;
        private MenuList CurrentMenuList => _isNewGame ? _newGameMenus : _loadGameMenus;
        private readonly MenuList _newGameMenus;
        private readonly MenuList _loadGameMenus;

        public bool AllowGameStart { get; private set; }
        private bool PressedEnter => Main.ModdingAPI.InputHandler.GetButtonDown(ButtonType.UIConfirm);
        private bool PressedCancel => Main.ModdingAPI.InputHandler.GetButtonDown(ButtonType.UICancel);

        public MenuHandler()
        {
            _mainMenuCache = new(() => Object.FindObjectOfType<MainMenuWindowLogic>());
            _slotsMenuCache = new(() => _mainMenuCache.Value.slotsMenuView.transform.parent.gameObject);

            _newGameMenus = new(OnFinishMenu, OnCancelMenu);
            _loadGameMenus = new(OnFinishMenu, OnCancelMenu);
        }

        public void RegisterNewGameMenu(BaseMenu menu) => _newGameMenus.AddMenu(menu);
        public void RegisterLoadGameMenu(BaseMenu menu) => _loadGameMenus.AddMenu(menu);

        public void Update()
        {
            if (!IsMenuActive) return;

            if (_closeNextFrame)
            {
                _closeNextFrame = false;
                CurrentMenuList.ShowPreviousMenu();
                return;
            }

            if (PressedEnter)
            {
                CurrentMenuList.ShowNextMenu();
            }
            else if (PressedCancel)
            {
                _closeNextFrame = true;
            }
        }

        public void OnTryStartGame(int slot, bool isNewGame)
        {
            _currentSlot = slot;
            _isNewGame = isNewGame;

            if (CurrentMenuList.IsEmpty)
            {
                OnFinishMenu();
                return;
            }

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

            CurrentMenuList.StartMenu();
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
            if (_isNewGame) _mainMenuCache.Value.NewGame(_currentSlot);
            else _mainMenuCache.Value.LoadGame(_currentSlot);
            AllowGameStart = false;
        }
    }
}
