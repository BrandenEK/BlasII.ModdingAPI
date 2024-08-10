using BlasII.ModdingAPI.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlasII.ModdingAPI
{
    internal class ModLoader
    {
        private readonly List<BlasIIMod> _mods = new();

        private bool _initialized = false;
        private bool _loadedMenu = false;

        private UnityEngine.GameObject _modObject;
        public UnityEngine.GameObject ModObject => _modObject;

        public ModLoader()
        {
            ModHelper.LoadedMods = _mods;
        }

        /// <summary>
        /// Loops over the list of registered mods and performs an action on each one
        /// </summary>
        public void ProcessModFunction(System.Action<BlasIIMod> action)
        {
            foreach (var mod in _mods)
            {
                try
                {
                    action(mod);
                }
                catch (System.Exception e)
                {
                    ModLog.Error($"Encountered error: {e.Message}\n{e.CleanStackTrace()}", mod);
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

            LogSpecial("Initialization");
            _modObject = new UnityEngine.GameObject("Mod object");
            UnityEngine.Object.DontDestroyOnLoad(_modObject);

            ModLog.Info("Initializing mods...");
            ProcessModFunction(mod => mod.OnInitialize());
            ModLog.Info("All mods initialized!");
            ProcessModFunction(mod => mod.OnAllInitialized());
            _initialized = true;
        }

        /// <summary>
        /// Disposes all mods
        /// </summary>
        public void Dispose()
        {
            ProcessModFunction(mod => mod.OnDispose());

            ModLog.Info("All mods disposed!");
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

            LogSpecial("Loaded scene: " + sceneName);

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
            if (_mods.Any(m => m.Id == mod.Id))
            {
                ModLog.Log($"Mod with id '{mod.Id}' already exists!", "Mod Loader", ModLog.LogLevel.Error);
                return;
            }

            ModLog.Log($"Registering mod: {mod.Id} ({mod.Version})", "Mod Loader", System.Drawing.Color.Green);
            _mods.Add(mod);
        }

        /// <summary>
        /// Formats the message for scene loading
        /// </summary>
        private void LogSpecial(string message)
        {
            var sb = new StringBuilder();
            int length = message.Length;
            for (int i = 0; i < length; i++)
                sb.Append('-');
            string line = sb.ToString();

            ModLog.Info(string.Empty);
            ModLog.Info(line);
            ModLog.Info(message);
            ModLog.Info(line);
            ModLog.Info(string.Empty);
        }
    }
}
