using Il2CppTGK.Game;
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
            
        }

        protected internal override void OnAllInitialized()
        {
            
        }

        protected internal override void OnDispose()
        {
            
        }

        protected internal override void OnUpdate()
        {

        }

        protected internal override void OnSceneLoaded(string sceneName)
        {
            if (sceneName == "MainMenu")
            {
                DisplayModListOnMenu();
            }
        }

        protected internal override void OnSceneUnloaded(string sceneName)
        {
            
        }

        private void DisplayModListOnMenu()
        {
            // Do this better

            // Find canvas object
            CanvasScaler canvas = UnityEngine.Object.FindObjectOfType<CanvasScaler>();
            if (canvas == null) return;

            // Calculate menu text
            var sb = new StringBuilder("\n\n");
            foreach (var mod in Main.ModLoader.AllMods)
            {
                sb.AppendLine($"{mod.Name} v{mod.Version}");
            }

            // Find the version text and add to it
            foreach (TextMeshProUGUI childText in canvas.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (childText.text.Contains("1.0.5"))
                {
                    childText.text += sb.ToString();
                    childText.alignment = TextAlignmentOptions.TopRight;
                }
            }
        }
    }
}
