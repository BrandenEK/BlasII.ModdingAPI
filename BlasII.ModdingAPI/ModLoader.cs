using System;
using System.Collections.Generic;
using System.Text;

namespace BlasII.ModdingAPI
{
    internal class ModLoader
    {
        private readonly List<BlasIIMod> mods = new();

        private bool initialized = false;

        public void Initialize()
        {
            foreach (var mod in mods)
            {
                mod.OnInitialize();
            }
        }

        public void AllInitialized()
        {
            foreach (var mod in mods)
            {
                mod.OnAllInitialized();
            }
            initialized = true;
        }

        public void Dispose()
        {
            foreach (var mod in mods)
            {
                mod.OnDispose();
            }
            initialized = false;
        }

        public void Update()
        {
            if (!initialized)
                return;

            foreach (var mod in mods)
            {
                mod.OnUpdate();
            }
        }

        public void SceneLoaded(string sceneName)
        {
            Main.LogSpecial("Modding API", "Loaded scene: " + sceneName);

            foreach (var mod in mods)
            {
                mod.OnSceneLoaded(sceneName);
            }
        }

        public void SceneUnloaded(string sceneName)
        {
            foreach (var mod in mods)
            {
                mod.OnSceneUnloaded(sceneName);
            }
        }

        public void RegisterMod(BlasIIMod mod)
        {
            foreach (BlasIIMod m in mods)
            {
                if (m.Id == mod.Id)
                    return;
            }

            Main.LogCustom("Mod Loader", "Registering mod: " + mod.Id, ConsoleColor.Green);
            mods.Add(mod);
            // Do something else to register the mod?
        }

        public string CalculateModListText()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n");

            foreach (var mod in mods)
            {
                sb.AppendLine($"{mod.Name} v{mod.Version}");
            }

            return sb.ToString();
        }
    }
}
