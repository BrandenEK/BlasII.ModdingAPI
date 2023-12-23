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

        private Sprite _cursorIcon;
        public Sprite CursorIcon => _cursorIcon;

        protected internal override void OnInitialize()
        {
            LocalizationHandler.RegisterDefaultLanguage("en");
            FileHandler.LoadDataAsSprite("cursor.png", out _cursorIcon, 32, true);

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
        }

        private void DisplayModListOnMenu()
        {
            // Calculate mod list text
            var sb = new StringBuilder();
            foreach (var mod in Main.ModLoader.AllMods)
            {
                sb.AppendLine($"{mod.Name} v{mod.Version}");
            }

            // Create shadow text
            UIModder.CreateRect("ModList", UIModder.Parents.MainMenu.GetChild(0))
                .SetXRange(0, 0)
                .SetYRange(1, 1)
                .SetPivot(0, 1)
                .SetSize(400, 100)
                .SetPosition(30, -18)
                .AddText()
                .SetContents(sb.ToString())
                .SetAlignment(TextAlignmentOptions.TopLeft)
                .SetFontSize(40)
                .SetColor(new Color(0.773f, 0.451f, 0.314f))
                .AddShadow();

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
