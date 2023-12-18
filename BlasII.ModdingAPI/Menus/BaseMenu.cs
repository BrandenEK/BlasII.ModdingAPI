using System;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    public abstract class BaseMenu
    {
        internal string Title { get; }
        internal int Priority { get; }
        internal BlasIIMod OwnerMod { get; set; }

        public BaseMenu(string title, int priority)
        {
            Title = title;
            Priority = priority;
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

            UI = MenuModder.CreateBaseMenu(OwnerMod, Title, isFirst, isLast);
            CreateUI(UI.transform.Find("Main Section"));
        }

        protected internal abstract void CreateUI(Transform ui);

        protected void AddClickable(RectTransform rect, Action onClick) => UI.AddClickable(rect, onClick, null);

        protected void AddClickable(RectTransform rect, Action onClick, Action onUnclick) => UI.AddClickable(rect, onClick, onUnclick);
    }
}