using BlasII.ModdingAPI.Storage;
using BlasII.ModdingAPI.UI;
using Il2CppTMPro;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI
{
    internal class ModdingAPI : BlasIIMod
    {
        public ModdingAPI() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        protected internal override void OnInitialize()
        {
            AbilityStorage.Initialize();
            ItemStorage.Initialize();
            StatStorage.Initialize();
            WeaponStorage.Initialize();
        }

        protected internal override void OnUpdate()
        {

        }

        protected internal override void OnSceneLoaded(string sceneName)
        {
            if (sceneName == "MainMenu")
            {
                StoreDefaultUI();
                DisplayModListOnMenu();
            }
        }

        protected internal override void OnSceneUnloaded(string sceneName)
        {
            
        }

        private void StoreDefaultUI()
        {
            UIModder.DefaultParent = Object.FindObjectOfType<CanvasScaler>()?.GetComponent<RectTransform>();
            UIModder.DefaultFont = Object.FindObjectOfType<TextMeshProUGUI>()?.font;
        }

        private void DisplayModListOnMenu()
        {
            // Calculate mod list text
            var sb = new StringBuilder();
            foreach (var mod in Main.ModLoader.AllMods)
            {
                sb.AppendLine($"{mod.Name} v{mod.Version}");
            }

            // Create underneath text object
            UIModder.CreateText("Mods under", UIModder.MainMenuParent)
                .SetContents(sb.ToString())
                .SetAlignment(TextAlignmentOptions.TopLeft)
                .SetFontSize(40)
                .SetColor(new Color(0.004f, 0.008f, 0.008f))
                .rectTransform
                .SetXRange(0, 0)
                .SetYRange(1, 1)
                .SetPivot(0, 1)
                .SetSize(400, 100)
                .SetPosition(30, -20);

            // Create overhead text object
            UIModder.CreateText("Mods over", UIModder.MainMenuParent)
                .SetContents(sb.ToString())
                .SetAlignment(TextAlignmentOptions.TopLeft)
                .SetFontSize(40)
                .SetColor(new Color(0.773f, 0.451f, 0.314f))
                .rectTransform
                .SetXRange(0, 0)
                .SetYRange(1, 1)
                .SetPivot(0, 1)
                .SetSize(400, 100)
                .SetPosition(30, -16);
        }
    }
}
