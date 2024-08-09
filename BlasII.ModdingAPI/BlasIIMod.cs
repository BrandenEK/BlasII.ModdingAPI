using BlasII.ModdingAPI.Audio;
using BlasII.ModdingAPI.Config;
using BlasII.ModdingAPI.Files;
using BlasII.ModdingAPI.Input;
using BlasII.ModdingAPI.Localization;
using BlasII.ModdingAPI.Menus;
using BlasII.ModdingAPI.Messages;
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
        /// A persistent gameobject for all mods to use
        /// </summary>
        public UnityEngine.GameObject ModObject => Main.ModLoader.ModObject;

        /// <summary>
        /// Checks whether a mod is loaded, and returns it if so
        /// </summary>
        public bool IsModLoaded(string modId, out BlasIIMod mod) => Main.ModLoader.IsModLoaded(modId, out mod);

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

        /// <summary>
        /// Handles displaying menus when beginning or loading a game
        /// </summary>
        public MenuHandler MenuHandler => _menuHandler;
        private readonly MenuHandler _menuHandler;

        /// <summary>
        /// Handles sending and receiving messages, such as listening for specific broadcasts
        /// </summary>
        public MessageHandler MessageHandler => _messageHandler;
        private readonly MessageHandler _messageHandler;

        // Events

        /// <summary>
        /// Called when starting the game, at the same time as other managers
        /// </summary>
        protected internal virtual void OnInitialize() { }

        /// <summary>
        /// Called when starting the game, after all other mods have been initialized
        /// </summary>
        protected internal virtual void OnAllInitialized() { }

        /// <summary>
        /// Called when exiting the game, at the same time as other managers
        /// </summary>
        protected internal virtual void OnDispose() { }

        /// <summary>
        /// Called every frame after initialization
        /// </summary>
        protected internal virtual void OnUpdate() { }

        /// <summary>
        /// Called at the end of every frame after initialization
        /// </summary>
        protected internal virtual void OnLateUpdate() { }

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
        protected internal virtual void OnNewGame() { }

        /// <summary>
        /// Called when loading an existing game on the main menu, after data is reset
        /// </summary>
        protected internal virtual void OnLoadGame() { }

        /// <summary>
        /// Called when quiting a game, after returning to the main menu
        /// </summary>
        protected internal virtual void OnExitGame() { }

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
            _audioHandler = new AudioHandler();
            _configHandler = new ConfigHandler(this);
            _fileHandler = new FileHandler(this);
            _inputHandler = new InputHandler(this);
            _localizationHandler = new LocalizationHandler(this);
            _menuHandler = new MenuHandler(this);
            _messageHandler = new MessageHandler(this);

            // Register mod
            Main.ModLoader.RegisterMod(this);
        }
    }
}
