using BlasII.ModdingAPI.Audio;
using BlasII.ModdingAPI.Config;
using BlasII.ModdingAPI.Files;
using BlasII.ModdingAPI.Input;
using BlasII.ModdingAPI.Localization;
using System.Drawing;

namespace BlasII.ModdingAPI
{
    /// <summary>
    /// The main class for the mod, handles access to the API
    /// </summary>
    public abstract class BlasIIMod
    {
        // Mod info

        private readonly string id;
        internal string Id => id;

        private readonly string name;
        internal string Name => name;

        private readonly string author;
        internal string Author => author;

        private readonly string version;
        internal string Version => version;

        // Helpers

        /// <summary>
        /// Handles scene loading, such as checking if on main menu
        /// </summary>
        public LoadStatus LoadStatus => loadStatus;
        private readonly LoadStatus loadStatus = new();

        /// <summary>
        /// A persistent gameobject for all mods to use
        /// </summary>
        public UnityEngine.GameObject ModObject => Main.ModLoader.ModObject;

        /// <summary>
        /// The build version of the game executable
        /// </summary>
        public string GameVersion => Main.ModLoader.GameVersion;

        // Handlers

        /// <summary>
        /// Handles playing audio, such as UI sfx
        /// </summary>
        public AudioHandler AudioHandler => _audioHandler;
        private readonly AudioHandler _audioHandler;

        /// <summary>
        /// Handles storing and retrieving config properties
        /// </summary>
        public ConfigHandler ConfigHandler => _configHandler;
        private readonly ConfigHandler _configHandler;

        /// <summary>
        /// Handles file IO, such as such loading data or writing to a file
        /// </summary>
        public FileHandler FileHandler => _fileHandler;
        private readonly FileHandler _fileHandler;

        /// <summary>
        /// Handles player input, such as custom keybindings
        /// </summary>
        public InputHandler InputHandler => _inputHandler;
        private readonly InputHandler _inputHandler;

        /// <summary>
        /// Handles translations, such as automatic localization on language change
        /// </summary>
        public LocalizationHandler LocalizationHandler => _localizationHandler;
        private readonly LocalizationHandler _localizationHandler;

        // Events

        /// <summary>
        /// Called when starting the game, at the same time as other managers
        /// </summary>
        protected internal virtual void OnInitialize() { }

        /// <summary>
        /// Called when exiting the game, at the same time as other managers
        /// </summary>
        protected internal virtual void OnDispose() { }

        /// <summary>
        /// Called every frame after initialization
        /// </summary>
        protected internal virtual void OnUpdate() { }

        /// <summary>
        /// Called when a new level is loaded, including the main menu
        /// </summary>
        protected internal virtual void OnSceneLoaded(string sceneName) { }

        /// <summary>
        /// Called when an old level is unloaded, including the main menu
        /// </summary>
        protected internal virtual void OnSceneUnloaded(string sceneName) { }

        /// <summary>
        /// Called when starting a new game on the main menu, after data is reset
        /// </summary>
        protected internal virtual void OnNewGameStarted() { }

        // Logging

        /// <summary>
        /// Logs a message in gray to the console
        /// </summary>
        public void Log(object message) => Main.Log(Name, message);

        /// <summary>
        /// Logs a message in yellow to the console
        /// </summary>
        public void LogWarning(object warning) => Main.LogWarning(Name, warning);

        /// <summary>
        /// Logs a message in red to the console
        /// </summary>
        public void LogError(object error) => Main.LogError(Name, error);

        /// <summary>
        /// Logs a message in any color to the console
        /// </summary>
        public void LogCustom(object message, Color color) => Main.LogCustom(Name, message, color);

        // Constructor

        /// <summary>
        /// Initializes and registers a new BlasII mod
        /// </summary>
        public BlasIIMod(string id, string name, string author, string version)
        {
            // Set data
            this.id = id;
            this.name = name;
            this.author = author;
            this.version = version;

            // Set handlers
            _fileHandler = new FileHandler(this);

            _audioHandler = new AudioHandler();
            _configHandler = new ConfigHandler(this);
            _inputHandler = new InputHandler(this);
            _localizationHandler = new LocalizationHandler(this);

            // Register mod
            Main.ModLoader.RegisterMod(this);
        }
    }
}
