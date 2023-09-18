using MelonLoader;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace BlasII.ModdingAPI
{
    internal class Main : MelonMod
    {
        public static ModLoader ModLoader { get; private set; }
        public static ModdingAPI ModdingAPI { get; private set; }

        public override void OnInitializeMelon()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadMissingAssemblies);

            ModLoader = new ModLoader();
            ModdingAPI = new ModdingAPI();
        }

        public override void OnUpdate() => ModLoader.Update();

        public override void OnSceneWasLoaded(int _, string sceneName) => ModLoader.UnitySceneLoaded(sceneName);

        private Assembly LoadMissingAssemblies(object send, ResolveEventArgs args)
        {
            string assemblyPath = Path.GetFullPath($"Modding\\data\\{args.Name[..args.Name.IndexOf(",")]}.dll");

            if (File.Exists(assemblyPath))
            {
                LogWarning("Modding API", "Successfully loaded missing assembly: " + args.Name);
                return Assembly.LoadFrom(assemblyPath);
            }
            else
            {
                LogWarning("Modding API", "Failed to load missing assembly: " + args.Name);
                return null;
            }
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
    }
}