using System;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    public abstract class BaseMenu
    {
        private readonly string _title;
        internal string Title => _title;

        private readonly int _priority;
        internal int Priority => _priority;

        public BaseMenu(string title, int priority)
        {
            _title = title;
            _priority = priority;
        }

        // Using menu

        public virtual void OnStart() { }

        public virtual void OnFinish() { }

        public virtual void OnCancel() { }

        public virtual void OnShow() { }

        public virtual void OnHide() { }

        // UI display

        internal MenuComponent UI { get; private set; }

        internal void CreateUI(bool isFirst, bool isLast)
        {
            if (UI != null) return;

            UI = Main.ModdingAPI.MenuHandler.CreateBaseMenu(_title, isFirst, isLast);
            CreateUI(UI.transform.Find("Main Section"));
        }

        protected internal abstract void CreateUI(Transform ui);

        protected void AddClickable(RectTransform rect, Action onClick) => UI.AddClickable(rect, onClick, null);

        protected void AddClickable(RectTransform rect, Action onClick, Action onUnclick) => UI.AddClickable(rect, onClick, onUnclick);
    }
}