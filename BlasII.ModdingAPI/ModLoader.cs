using System.Collections.Generic;
using System.Drawing;

namespace BlasII.ModdingAPI
{
    internal class ModLoader
    {
        private readonly List<BlasIIMod> mods = new();

        public IEnumerable<BlasIIMod> AllMods => mods;

        private bool initialized = false;
        private bool inLevel = false;

        public void Initialize()
        {
            Main.Log(ModInfo.MOD_NAME, "Initializing mods...");

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

            Main.Log(ModInfo.MOD_NAME, "All mods initialized!");
            initialized = true;
        }

        public void Dispose()
        {
            foreach (var mod in mods)
            {
                mod.OnDispose();
            }

            Main.Log(ModInfo.MOD_NAME, "All mods diposed!");
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
            if (inLevel) return;

            Main.LogSpecial("Modding API", "Loaded scene: " + sceneName);
            inLevel = true;

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

            inLevel = false;
        }

        public void UnitySceneLoaded(string sceneName)
        {
            if (sceneName == "Empty")
                inLevel = false;
        }

        public void RegisterMod(BlasIIMod mod)
        {
            foreach (BlasIIMod m in mods)
            {
                if (m.Id == mod.Id)
                {
                    Main.LogError("Mod Loader", $"Mod with id '{mod.Id}' already exists!");
                    return;
                }
            }

            Main.LogCustom("Mod Loader", $"Registering mod: {mod.Id} ({mod.Version})", Color.Green);
            mods.Add(mod);
        }
    }
}
