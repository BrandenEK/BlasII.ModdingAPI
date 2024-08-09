using BlasII.ModdingAPI.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.ModdingAPI
{
    internal class ModLoader
    {
        private readonly List<BlasIIMod> mods = new();

        public IEnumerable<BlasIIMod> AllMods => mods;

        private bool _initialized = false;
        private bool _loadedMenu = false;

        public string GameVersion { get; internal set; } = "Unknown";

        private GameObject _modObject;
        public GameObject ModObject => _modObject;

        /// <summary>
        /// Loops over the list of registered mods and performs an action on each one
        /// </summary>
        public void ProcessModFunction(System.Action<BlasIIMod> action)
        {
            foreach (var mod in mods)
            {
                try
                {
                    action(mod);
                }
                catch (System.Exception e)
                {
                    mod.LogError($"Encountered error: {e.Message}\n{e.CleanStackTrace()}");
                }
            }
        }

        /// <summary>
        /// Initializes all mods
        /// </summary>
        public void Initialize()
        {
            if (_initialized)
                return;

            _modObject = new GameObject("Mod object");
            Object.DontDestroyOnLoad(_modObject);

            Main.Log(ModInfo.MOD_NAME, "Initializing mods...");
            ProcessModFunction(mod => mod.OnInitialize());
            Main.Log(ModInfo.MOD_NAME, "All mods initialized!");
            ProcessModFunction(mod => mod.OnAllInitialized());
            _initialized = true;
        }

        /// <summary>
        /// Disposes all mods
        /// </summary>
        public void Dispose()
        {
            ProcessModFunction(mod => mod.OnDispose());

            Main.Log(ModInfo.MOD_NAME, "All mods disposed!");
        }

        /// <summary>
        /// Updates all mods
        /// </summary>
        public void Update()
        {
            if (!_initialized)
                return;

            ProcessModFunction(mod => mod.OnUpdate());
        }

        /// <summary>
        /// Late updates all mods
        /// </summary>
        public void LateUpdate()
        {
            if (!_initialized)
                return;

            ProcessModFunction(mod => mod.OnLateUpdate());
        }

        /// <summary>
        /// Processes a LoadScene event for all mods
        /// </summary>
        public void SceneLoaded(string sceneName)
        {
            if (SceneHelper.CurrentScene != string.Empty)
                return;

            if (sceneName == "MainMenu")
            {
                if (_loadedMenu)
                    ProcessModFunction(mod => mod.OnExitGame());
                _loadedMenu = true;
            }

            Main.LogSpecial(ModInfo.MOD_NAME, "Loaded scene: " + sceneName);

            SceneHelper.CurrentScene = sceneName;
            ProcessModFunction(mod => mod.OnSceneLoaded(sceneName));
        }

        /// <summary>
        /// Processes an UnloadScene event for all mods
        /// </summary>
        public void SceneUnloaded(string sceneName)
        {
            SceneHelper.CurrentScene = string.Empty;
            ProcessModFunction(mod => mod.OnSceneUnloaded(sceneName));
        }

        /// <summary>
        /// Sets the unloaded flag when the main menu is loaded
        /// </summary>
        public void UnitySceneLoaded(string sceneName)
        {
            if (sceneName == "Empty")
                SceneHelper.CurrentScene = string.Empty;
        }

        /// <summary>
        /// Registers a new mod whenever it is first created
        /// </summary>
        public void RegisterMod(BlasIIMod mod)
        {
            if (mods.Any(m => m.Id == mod.Id))
            {
                Main.LogError("Mod Loader", $"Mod with id '{mod.Id}' already exists!");
                return;
            }

            Main.LogCustom("Mod Loader", $"Registering mod: {mod.Id} ({mod.Version})", System.Drawing.Color.Green);
            mods.Add(mod);
        }

        /// <summary>
        /// Checks whether a mod is already loaded
        /// </summary>
        public bool IsModLoaded(string modId, out BlasIIMod mod)
        {
            return (mod = mods.FirstOrDefault(m => m.Id == modId)) != null;
        }
    }
}
