using BlasII.ModdingAPI.Assets;
using BlasII.ModdingAPI.Input;
using BlasII.ModdingAPI.UI;
using Il2CppTGK.Game.Components.UI;
using Il2CppTMPro;
using System.Text;
using UnityEngine;

namespace BlasII.ModdingAPI
{
    internal class ModdingAPI : BlasIIMod
    {
        public ModdingAPI() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        private bool _initializedUI = false;

        protected internal override void OnInitialize()
        {
            AssetStorage.Initialize();
            InputStorage.Initialize();
        }

        protected internal override void OnSceneLoaded(string sceneName)
        {
            if (sceneName == "MainMenu")
            {
                if (!_initializedUI)
                {
                    UIModder.Fonts.Initialize();
                    UIModder.Parents.Initialize();
                    _initializedUI = true;
                }

                DisplayModListOnMenu();
            }
                MessageHandler.Broadcast("Test message", "Test content");
                MessageHandler.Send("BlasII.Test", "Special");
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
            UIModder.CreateRect("Mods under", UIModder.Parents.MainMenu.GetChild(0))
                .SetXRange(0, 0)
                .SetYRange(1, 1)
                .SetPivot(0, 1)
                .SetSize(400, 100)
                .SetPosition(30, -20)
                .AddText()
                .SetContents(sb.ToString())
                .SetAlignment(TextAlignmentOptions.TopLeft)
                .SetFontSize(40)
                .SetColor(new Color(0.004f, 0.008f, 0.008f));

            // Create overhead text object
            UIModder.CreateRect("Mods over", UIModder.Parents.MainMenu.GetChild(0))
                .SetXRange(0, 0)
                .SetYRange(1, 1)
                .SetPivot(0, 1)
                .SetSize(400, 100)
                .SetPosition(30, -16)
                .AddText()
                .SetContents(sb.ToString())
                .SetAlignment(TextAlignmentOptions.TopLeft)
                .SetFontSize(40)
                .SetColor(new Color(0.773f, 0.451f, 0.314f));

            // Store game version
            var versionObject = Object.FindObjectOfType<SetGameVersionText>();
            string versionText = versionObject.GetComponentInChildren<TextMeshProUGUI>().text;

            int spaceIdx = versionText.IndexOf(' ');
            if (spaceIdx >= 0)
                versionText = versionText[..spaceIdx];

            int dashIndex = versionText.IndexOf("-");
            if (dashIndex >= 0)
                versionText = versionText[..dashIndex];

            Main.ModLoader.GameVersion = versionText;
        }
    }
}
