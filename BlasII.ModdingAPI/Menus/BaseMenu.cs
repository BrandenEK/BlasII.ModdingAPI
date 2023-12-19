using System;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    /// <summary>
    /// The abstract form of an in-game menu
    /// </summary>
    public abstract class BaseMenu
    {
        internal string Title { get; }
        internal int Priority { get; }
        internal BlasIIMod OwnerMod { get; set; }
        internal MenuComponent UI { get; private set; }

        /// <summary>
        /// Initializes a menu with the given parameters
        /// </summary>
        public BaseMenu(string title, int priority)
        {
            Title = title;
            Priority = priority;
        }

        /// <summary>
        /// Called when the menus are first opened
        /// </summary>
        public virtual void OnStart() { }

        /// <summary>
        /// Called when the menus are closed by starting the game
        /// </summary>
        public virtual void OnFinish() { }

        /// <summary>
        /// Called when the menus are closed by returning to the title screen
        /// </summary>
        public virtual void OnCancel() { }

        /// <summary>
        /// Called when this specific menu is opened
        /// </summary>
        public virtual void OnShow() { }

        /// <summary>
        /// Called when this specific menu is closed
        /// </summary>
        public virtual void OnHide() { }

        /// <summary>
        /// Creates the specific UI for this menu if it doesnt already exist
        /// </summary>
        internal void CreateUI(bool isFirst, bool isLast)
        {
            if (UI != null) return;

            UI = MenuModder.CreateBaseMenu(OwnerMod, Title, isFirst, isLast);
            CreateUI(UI.transform.Find("Main Section"));
        }

        /// <summary>
        /// Adds all menu-specific UI to its base UI object
        /// </summary>
        protected internal abstract void CreateUI(Transform ui);

        /// <summary>
        /// Adds an event to occur whenever this object is clicked on
        /// </summary>
        protected void AddClickable(RectTransform rect, Action onClick) => UI.AddClickable(rect, onClick, null);

        /// <summary>
        /// Adds an event to occur whenever this object is clicked on or clicked off
        /// </summary>
        protected void AddClickable(RectTransform rect, Action onClick, Action onUnclick) => UI.AddClickable(rect, onClick, onUnclick);
    }
}