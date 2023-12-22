using BlasII.ModdingAPI.Assets;
using BlasII.ModdingAPI.Input;
using BlasII.ModdingAPI.Menus;
using BlasII.ModdingAPI.UI;
using Il2CppTGK.Game.Components.Misc;
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
            LocalizationHandler.RegisterDefaultLanguage("en");

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

            LogWarning("Scene visible: " + Cursor.visible);
            //Cursor.visible = false;
        }

        protected internal override void OnLateUpdate()
        {
            bool showCursor = MenuModder.IsMenuActive;
            Cursor.visible = showCursor;
            Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;

            if (UnityEngine.Input.GetKeyDown(KeyCode.O))
            {
                Cursor.SetCursor(null, CursorMode.Auto);
                Cursor.lockState = CursorLockMode.None;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.P))
            {
                var cursor = new GameObject("MouseVisibility").AddComponent<MouseCursorVisibilityController>();
                Cursor.SetCursor(cursor.hiddenTexture, CursorMode.ForceSoftware);
                Cursor.lockState = CursorLockMode.Locked;
            }
            //LogWarning(MenuModder.IsMenuActive + ": " + Cursor.visible);
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
