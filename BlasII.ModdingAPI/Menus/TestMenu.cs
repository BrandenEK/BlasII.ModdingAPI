using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    internal class TestMenu : BaseMenu
    {
        public TestMenu() : base("test", 5) { }

        protected internal override GameObject CreateUI(GameObject ui)
        {
            return ui;
        }
    }
}
