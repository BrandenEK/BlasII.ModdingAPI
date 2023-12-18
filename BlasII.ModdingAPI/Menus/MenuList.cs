using System;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Menus
{
    internal class MenuList
    {
        private readonly List<BaseMenu> _menus = new();
        private int _currentMenu;

        private readonly Action onFinish;
        private readonly Action onCancel;

        public bool IsEmpty => _menus.Count == 0;
        public bool IsActive => _currentMenu != -1;

        public MenuList(Action onFinish, Action onCancel)
        {
            _currentMenu = -1;

            this.onFinish = onFinish;
            this.onCancel = onCancel;
        }

        /// <summary>
        /// Adds a menu to the list in the correct spot based on priority
        /// </summary>
        public void AddMenu(BaseMenu menu)
        {
            for (int i = 0; i < _menus.Count; i++)
            {
                if (menu.Priority < _menus[i].Priority)
                    _menus.Insert(i, menu);
            }
            _menus.Add(menu);
        }

        /// <summary>
        /// Activates a specific menu
        /// </summary>
        private void ShowMenu(int index)
        {
            BaseMenu menu = _menus[index];
            //menu.MenuUI.SetActive(true);
            menu.OnShow();

            _currentMenu = index;
        }

        /// <summary>
        /// Deactivates a specific menu
        /// </summary>
        private void HideMenu(int index)
        {
            BaseMenu menu = _menus[index];
            menu.OnHide();
            //menu.MenuUI.SetActive(false);
        }

        /// <summary>
        /// Opens the first menu
        /// </summary>
        public void StartMenu()
        {
            if (IsEmpty) return;

            foreach (var menu in _menus)
                menu.OnStart();

            ShowMenu(0);
        }

        /// <summary>
        /// Hides the current menu and shows the next one.  Calls onFinish at the end
        /// </summary>
        public void ShowNextMenu()
        {
            Main.ModdingAPI.Log("Showing next menu");

            // If there is another menu, move to it
            if (_currentMenu < _menus.Count - 1)
            {
                HideMenu(_currentMenu);
                ShowMenu(_currentMenu + 1);
                return;
            }

            // Otherwise, finish the menu
            _currentMenu = -1;
            foreach (var menu in _menus)
                menu.OnFinish();
            onFinish();
        }

        /// <summary>
        /// Hides the current menu and shows the previous one.  Calls onCancel at the beginning
        /// </summary>
        public void ShowPreviousMenu()
        {
            Main.ModdingAPI.Log("Showing previous menu");
            HideMenu(_currentMenu);

            // If there is another menu, move to it
            if (_currentMenu > 0)
            {
                ShowMenu(_currentMenu - 1);
                return;
            }

            // Otherwise, cancel the menu
            _currentMenu = -1;
            foreach (var menu in _menus)
                menu.OnCancel();
            onCancel();
        }
    }
}