using BlasII.ModdingAPI.Persistence;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.ModdingAPI
{
    internal class ModLoader
    {
        private readonly List<BlasIIMod> mods = new();

        public IEnumerable<BlasIIMod> AllMods => mods;

        private bool _initialized = false;

        private string _currentScene = string.Empty;
        public string CurrentScene => _currentScene;

        private GameObject _modObject;
        public GameObject ModObject => _modObject;

        public void Initialize()
        {
            if (_initialized)
                return;

            _modObject = new GameObject("Mod object");
            Object.DontDestroyOnLoad(_modObject);

            Main.Log(ModInfo.MOD_NAME, "Initializing mods...");

            foreach (var mod in mods)
            {
                try
                {
                    mod.OnInitialize();
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }

            _initialized = true;
            Main.Log(ModInfo.MOD_NAME, "All mods initialized!");
        }

        public void Dispose()
        {
            foreach (var mod in mods)
            {
                try
                {
                    mod.OnDispose();
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }

            Main.Log(ModInfo.MOD_NAME, "All mods disposed!");
        }

        public void Update()
        {
            if (!_initialized)
                return;

            foreach (var mod in mods)
            {
                try
                {
                    mod.OnUpdate();
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        public void SceneLoaded(string sceneName)
        {
            if (_currentScene != string.Empty) return;

            Main.LogSpecial(ModInfo.MOD_NAME, "Loaded scene: " + sceneName);
            _currentScene = sceneName;

            foreach (var mod in mods)
            {
                try
                {
                    mod.OnSceneLoaded(sceneName);
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        public void SceneUnloaded(string sceneName)
        {
            foreach (var mod in mods)
            {
                try
                {
                    mod.OnSceneUnloaded(sceneName);
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
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
                try
                {
                    mod.OnNewGameStarted();
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        public void LanguageChanged()
        {
            foreach (var mod in mods)
            {
                try
                {
                    mod.LocalizationHandler.OnLangaugeChanged();
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        public void SaveGame(int slot)
        {
            var data = new Dictionary<string, SaveData>();

            foreach (var mod in mods)
            {
                try
                {
                    if (mod is IPersistentMod persistentMod)
                    {
                        data.Add(mod.Id, persistentMod.SaveGame());
                    }
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }

            SaveData.SaveDataToFile(slot, data);
        }

        public void LoadGame(int slot)
        {
            var data = SaveData.LoadDataFromFile(slot);

            foreach (var mod in mods)
            {
                try
                {
                    if (mod is IPersistentMod persistentMod && data.TryGetValue(mod.Id, out SaveData save))
                    {
                        persistentMod.LoadGame(save);
                    }
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        public void ResetGame()
        {
            foreach (var mod in mods)
            {
                try
                {
                    if (mod is IPersistentMod persistentMod)
                    {
                        persistentMod.ResetGame();
                    }
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.StackTrace}");
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

            Main.LogCustom("Mod Loader", $"Registering mod: {mod.Id} ({mod.Version})", System.Drawing.Color.Green);
            mods.Add(mod);
        }
    }
}
