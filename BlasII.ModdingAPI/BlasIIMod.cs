
namespace BlasII.ModdingAPI
{
    public class BlasIIMod
    {
        internal string Id { get; private set; }

        internal string Name { get; private set; }

        internal string Version { get; private set; }

        public BlasIIMod(string id, string name, string version)
        {
            Id = id;
            Name = name;
            Version = version;
        }

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
