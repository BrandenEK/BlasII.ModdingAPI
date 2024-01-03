using BlasII.ModdingAPI.Utils;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.Game;
using UnityEngine;
using BlasII.ModdingAPI.UI;

namespace BlasII.ModdingAPI.Menus
{
    internal class MenuModder
    {
        // Temporary settings
        private static bool _isNewGame = false;
        private static int _currentSlot = 0;

        // Menu objects
        private static readonly ObjectCache<MainMenuWindowLogic> _mainMenuCache;
        private static readonly ObjectCache _slotsMenuCache;

        // Menu lists
        public static bool IsMenuActive => CurrentMenuList.IsActive;
        private static MenuList CurrentMenuList => _isNewGame ? _newGameMenus : _loadGameMenus;
        private static readonly MenuList _newGameMenus;
        private static readonly MenuList _loadGameMenus;

        static MenuModder()
        {
            _mainMenuCache = new(() => Object.FindObjectOfType<MainMenuWindowLogic>());
            _slotsMenuCache = new(() => _mainMenuCache.Value.slotsMenuView.transform.parent.gameObject);

            _newGameMenus = new(OnFinishMenu, OnCancelMenu);
            _loadGameMenus = new(OnFinishMenu, OnCancelMenu);
        }

        public static bool AllowGameStart { get; private set; }

        public static void AddNewGameMenu(BaseMenu menu) => _newGameMenus.AddMenu(menu);

        public static void AddLoadGameMenu(BaseMenu menu) => _loadGameMenus.AddMenu(menu);

        public static void OnPressEnter() => CurrentMenuList.ShowNextMenu();

        public static void OnPressCancel() => CurrentMenuList.ShowPreviousMenu();

        public static void OnTryStartGame(int slot, bool isNewGame)
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
        private static void StartMenu()
        {
            _slotsMenuCache.Value.SetActive(false);
            CoreCache.Input.ClearAllInputBlocks();
            CurrentMenuList.StartMenu();
        }

        /// <summary>
        /// Whenever 'B' is pressed on the first menu, return to slots screen
        /// </summary>
        private static void OnCancelMenu()
        {
            _mainMenuCache.Value.OpenSlotMenu();
            _mainMenuCache.Value.slotsList.SelectElement(_currentSlot);
        }

        /// <summary>
        /// Whenever 'A' is pressed on the final menu, actually start the game
        /// </summary>
        private static void OnFinishMenu()
        {
            AllowGameStart = true;
            if (_isNewGame) _mainMenuCache.Value.NewGame(_currentSlot);
            else _mainMenuCache.Value.LoadGame(_currentSlot);
            AllowGameStart = false;
        }

        /// <summary>
        /// Creates a new empty menu UI
        /// </summary>
        public static MenuComponent CreateBaseMenu(BlasIIMod mod, string header, bool isFirst, bool isLast)
        {
            // Create copy for new settings menu
            var settingsMenu = Object.Instantiate(_slotsMenuCache.Value, _slotsMenuCache.Value.transform.parent);
            Object.Destroy(settingsMenu.transform.Find("SlotsList").gameObject);

            // Change text of title
            var title = settingsMenu.transform.Find("Header").GetComponent<UIPixelTextWithShadow>();
            mod.LocalizationHandler.AddPixelTextLocalizer(title, header);

            // Change text of buttons
            var newBtn = settingsMenu.transform.Find("Buttons/Button A/New").gameObject;
            var continueBtn = settingsMenu.transform.Find("Buttons/Button A/Continue").gameObject;
            var cancelBtn = settingsMenu.transform.Find("Buttons/Back").gameObject;

            newBtn.SetActive(true);
            continueBtn.SetActive(false);
            cancelBtn.SetActive(true);

            Main.ModdingAPI.LocalizationHandler.AddPixelTextLocalizer(
                newBtn.GetComponentInChildren<UIPixelTextWithShadow>(), isLast ? (_isNewGame ? "btnbgn" : "btncnt") : "btnnxt");
            Main.ModdingAPI.LocalizationHandler.AddPixelTextLocalizer(
                cancelBtn.GetComponentInChildren<UIPixelTextWithShadow>(), isFirst ? "btncnc" : "btnpvs");

            // Create holder for options and all settings
            UIModder.CreateRect(new RectCreationOptions()
            {
                Name = "Main Section",
                Parent = settingsMenu.transform,
                Position = new Vector2(0, -30),
                Size = new Vector2(1800, 750)
            });

            return settingsMenu.AddComponent<MenuComponent>();
        }
    }
}
