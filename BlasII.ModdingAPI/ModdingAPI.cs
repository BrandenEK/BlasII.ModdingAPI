using BlasII.ModdingAPI.Storage;
using BlasII.ModdingAPI.UI;
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
            AbilityStorage.Initialize();
            ItemStorage.Initialize();
            StatStorage.Initialize();
            WeaponStorage.Initialize();

#if GAME1_0
            LogWarning("Initialized API for game version 1.0");
#elif GAME2_0
            LogWarning("Initialized API for game version 2.0");
#endif
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

        protected internal override void OnUpdate()
        {
            if (InputHandler.GetButtonDown(Input.ButtonType.Interact))
                LogWarning("Interact");
            if (InputHandler.GetButtonDown(Input.ButtonType.Flask))
                LogWarning("flask");
            if (InputHandler.GetButtonDown(Input.ButtonType.ChangeWeaponSlot1))
                LogWarning("chage w 1");
            if (InputHandler.GetButtonDown(Input.ButtonType.UITopRow1))
                LogWarning("ui shoulder");
            if (InputHandler.GetButtonDown(Input.ButtonType.UITopRow2))
                LogWarning("ui shoudler 2");
            if (InputHandler.GetButtonDown(Input.ButtonType.UIConfirm))
                LogWarning("ui confirm");

            if (Time.frameCount % 240 == 0)
            {
                LogError("Move h: " + InputHandler.GetAxis(Input.AxisType.MoveHorizontal));
                LogError("Move v: " + InputHandler.GetAxis(Input.AxisType.MoveVertical));
                LogError("ui h: " + InputHandler.GetAxis(Input.AxisType.UIHorizontal));
                LogError("fake move: " + InputHandler.GetAxis(Input.AxisType.MoveFake));
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
        }
    }
}
