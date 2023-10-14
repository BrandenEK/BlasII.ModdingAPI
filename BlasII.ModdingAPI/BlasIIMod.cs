using BlasII.ModdingAPI.Audio;
using BlasII.ModdingAPI.Files;
using System.Drawing;

namespace BlasII.ModdingAPI
{
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

        private readonly LoadStatus loadStatus = new();
        public LoadStatus LoadStatus => loadStatus;

        public UnityEngine.GameObject ModObject => Main.ModLoader.ModObject;

        // Handlers

        private readonly FileHandler _fileHandler;
        public FileHandler FileHandler => _fileHandler;

        private readonly AudioHandler _audioHandler;
        public AudioHandler AudioHandler => _audioHandler;

        // Events

        protected internal virtual void OnInitialize() { }

        protected internal virtual void OnDispose() { }

        protected internal virtual void OnUpdate() { }

        protected internal virtual void OnSceneLoaded(string sceneName) { }

        protected internal virtual void OnSceneUnloaded(string sceneName) { }

        protected internal virtual void OnNewGameStarted() { }

        // Logging

        public void Log(object message) => Main.Log(Name, message);

        public void LogWarning(object warning) => Main.LogWarning(Name, warning);

        public void LogError(object error) => Main.LogError(Name, error);

        public void LogCustom(object message, Color color) => Main.LogCustom(Name, message, color);

        // Constructor

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

            // Register mod
            Main.ModLoader.RegisterMod(this);
        }
    }
}
