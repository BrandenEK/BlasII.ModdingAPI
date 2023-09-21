using BlasII.ModdingAPI.Persistence;
using System.Collections.Generic;
using System.Drawing;

namespace BlasII.ModdingAPI
{
    internal class ModLoader
    {
        private readonly List<BlasIIMod> mods = new();

        public IEnumerable<BlasIIMod> AllMods => mods;

        private bool _initialized = false;

        private string _currentScene = string.Empty;
        public string CurrentScene => _currentScene;

        public void Initialize()
        {
            if (_initialized)
                return;

            Main.Log(ModInfo.MOD_NAME, "Initializing mods...");

            foreach (var mod in mods)
            {
                mod.OnInitialize();
            }

            _initialized = true;
            Main.Log(ModInfo.MOD_NAME, "All mods initialized!");
        }

        public void Dispose()
        {
            foreach (var mod in mods)
            {
                mod.OnDispose();
            }

            Main.Log(ModInfo.MOD_NAME, "All mods diposed!");
        }

        public void Update()
        {
            if (!_initialized)
                return;

            foreach (var mod in mods)
            {
                mod.OnUpdate();
            }
        }

        public void SceneLoaded(string sceneName)
        {
            if (_currentScene != string.Empty) return;

            Main.LogSpecial(ModInfo.MOD_NAME, "Loaded scene: " + sceneName);
            _currentScene = sceneName;

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

            _currentScene = string.Empty;
        }

        public void UnitySceneLoaded(string sceneName)
        {
            if (sceneName == "Empty")
                _currentScene = string.Empty;
        }

        public void NewGame()
        {
            foreach (var mod in mods)
            {
                mod.OnNewGameStarted();
            }
        }

        public void SaveGame(int slot)
        {
            var data = new Dictionary<string, SaveData>();

            foreach (var mod in mods)
            {
                if (mod is IPersistentMod persistentMod)
                {
                    data.Add(mod.Id, persistentMod.SaveGame());
                }
            }

            SaveData.SaveDataToFile(slot, data);
        }

        public void LoadGame(int slot)
        {
            var data = SaveData.LoadDataFromFile(slot);

            foreach (var mod in mods)
            {
                if (mod is IPersistentMod persistentMod && data.TryGetValue(mod.Id, out SaveData save))
                {
                    persistentMod.LoadGame(save);
                }
            }
        }

        public void ResetGame()
        {
            foreach (var mod in mods)
            {
                if (mod is IPersistentMod persistentMod)
                {
                    persistentMod.ResetGame();
                }
            }
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
