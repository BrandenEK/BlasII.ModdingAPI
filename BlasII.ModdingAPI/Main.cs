using Il2CppTMPro;
using MelonLoader;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine.UI;

namespace BlasII.ModdingAPI
{
    internal class Main : MelonMod
    {
        private static Main instance;

        public static ModLoader ModLoader { get; private set; }
        public static ModdingAPI ModdingAPI { get; private set; }

        public override void OnInitializeMelon()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadMissingAssemblies);
            instance ??= this;

            ModLoader = new ModLoader();
            ModdingAPI = new ModdingAPI();
        }


        public static void Log(string modName, object message) => MelonLogger.Msg(modName, message);

        public static void LogWarning(string modName, object warning) => MelonLogger.Warning(modName, warning);

        public static void LogError(string modName, object error) => MelonLogger.Error(modName, error);

        public static void LogCustom(string modName, object message, Color color) => MelonLogger.Msg(color, modName, message);

        public static void LogSpecial(string modName, string message)
        {
            int length = message.Length;
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append('-');
            string line = sb.ToString();

            LogCustom(modName, string.Empty, Color.White);
            LogCustom(modName, line, Color.White);
            LogCustom(modName, message, Color.White);
            LogCustom(modName, line, Color.White);
            LogCustom(modName, string.Empty, Color.White);
        }


        public override void OnUpdate() => ModLoader.Update();

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            //MelonLogger.Warning("Scene loaded: " + sceneName);

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

        private Assembly LoadMissingAssemblies(object send, ResolveEventArgs args)
        {
            string assemblyPath = Path.GetFullPath($"Modding\\data\\{args.Name[..args.Name.IndexOf(",")]}.dll");
            LogWarning("Modding API", "Loading missing assembly: " + args.Name);
            return File.Exists(assemblyPath) ? Assembly.LoadFrom(assemblyPath) : null;
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