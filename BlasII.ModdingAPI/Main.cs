using Il2CppTMPro;
using MelonLoader;
using System;
using System.Text;
using UnityEngine.UI;

namespace BlasII.ModdingAPI
{
    internal class Main : MelonMod
    {
        private static Main instance;

        public static ModLoader ModLoader { get; private set; }

        public override void OnInitializeMelon()
        {
            ModLoader = new ModLoader();
            instance ??= this;

            ModLoader.RegisterMod(new ModdingAPI());
        }


        public static void Log(string modName, object message) => MelonLogger.Msg(message);

        public static void LogWarning(string modName, object warning) => MelonLogger.Warning(warning);

        public static void LogError(string modName, object error) => MelonLogger.Error(error);

        public static void LogCustom(string modName, object message, ConsoleColor color) => MelonLogger.Msg(color, message);

        public static void LogSpecial(string modName, string message)
        {
            int length = message.Length;
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append('-');
            string line = sb.ToString();

            LogCustom(modName, string.Empty, System.ConsoleColor.White);
            LogCustom(modName, line, System.ConsoleColor.White);
            LogCustom(modName, message, System.ConsoleColor.White);
            LogCustom(modName, line, System.ConsoleColor.White);
            LogCustom(modName, string.Empty, System.ConsoleColor.White);
        }


        public override void OnUpdate() => ModLoader.Update();

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLogger.Warning("Scene loaded: " + sceneName);

            if (sceneName == "LandingScene" || sceneName == "MainMenu")
            {
                AddModListToTitleScreen();
                ModLoader.SceneLoaded("MainMenu");
            }
        }

        private void AddModListToTitleScreen()
        {
            // Do this better
            CanvasScaler canvas = UnityEngine.Object.FindObjectOfType<CanvasScaler>();
            if (canvas == null)
            {
                MelonLogger.Msg("Canvas was null");
                return;
            }

            foreach (TextMeshProUGUI childText in canvas.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (childText.text.Contains("1.0.5"))
                {
                    childText.text += ModLoader.CalculateModListText();
                    childText.alignment = TextAlignmentOptions.TopRight;
                }
            }
        }


        //private void DisplayChildren(Transform parent, int level)
        //{
        //    for (int c = 0; c < parent.childCount; c++)
        //    {
        //        Transform child = parent.GetChild(c);
        //        string line = string.Empty;
        //        for (int i = 0; i < level; i++)
        //            line += "\t";
        //        Log(line + child.name);
        //        DisplayChildren(child, level + 1);
        //    }
        //}
    }
}