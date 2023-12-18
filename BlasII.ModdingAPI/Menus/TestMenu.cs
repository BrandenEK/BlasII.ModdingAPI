using BlasII.ModdingAPI.UI;
using UnityEngine;

namespace BlasII.ModdingAPI.Menus
{
    internal class TestMenu : BaseMenu
    {
        private readonly string _body;

        public TestMenu(string header, string body) : base(header, 5)
        {
            _body = body;
        }

        protected internal override void CreateUI(Transform ui)
        {
            var rect = UIModder.CreateRect("Body", ui)
                .SetXRange(0, 1).SetYRange(0, 1).AddText()
                .SetAlignment(Il2CppTMPro.TextAlignmentOptions.Center)
                .SetFontSize(50)
                .SetContents(_body);

            AddClickable(rect.rectTransform, () =>
            {
                Main.ModdingAPI.Log("Clicked info");
            });
        }
    }
}
