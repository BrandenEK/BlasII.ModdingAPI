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

        internal GameObject MenuUI { get; set; }

        protected internal abstract GameObject CreateUI(GameObject ui);
    }
}