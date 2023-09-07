﻿using System.Drawing;

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

        // Handlers

        private readonly FileHandler fileHandler;
        public FileHandler FileHandler => fileHandler;

        // Events

        protected internal virtual void OnInitialize() { }

        protected internal virtual void OnAllInitialized() { }

        protected internal virtual void OnDispose() { }

        protected internal virtual void OnUpdate() { }

        protected internal virtual void OnSceneLoaded(string sceneName) { }

        protected internal virtual void OnSceneUnloaded(string sceneName) { }

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
            fileHandler = new FileHandler(this);

            // Register mod
            Main.ModLoader.RegisterMod(this);
        }
    }
}
