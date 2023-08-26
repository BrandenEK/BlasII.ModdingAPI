using System.Reflection;

namespace BlasII.ModdingAPI
{
    public abstract class BlasIIMod
    {
        private readonly string id = Assembly.GetExecutingAssembly().GetName().Name;
        internal string Id => id;

        private readonly string version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        internal string Version => version;

        public abstract string Name { get; }

        protected internal virtual void OnInitialize() { }

        protected internal virtual void OnAllInitialized() { }

        protected internal virtual void OnDispose() { }

        protected internal virtual void OnUpdate() { }

        protected internal virtual void OnSceneLoaded(string sceneName) { }

        protected internal virtual void OnSceneUnloaded(string sceneName) { }

        public void Log(object message) => Main.Log(Name, message);

        public void LogWarning(object warning) => Main.LogWarning(Name, warning);

        public void LogError(object error) => Main.LogError(Name, error);
    }
}
