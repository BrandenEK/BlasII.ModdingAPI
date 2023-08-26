
namespace BlasII.ModdingAPI
{
    internal class ModdingAPI : BlasIIMod
    {
        public override string Name => "Modding API";

        protected internal override void OnInitialize()
        {
            LogWarning("Initialize");
        }

        protected internal override void OnAllInitialized()
        {
            LogWarning("All initialized");
        }

        protected internal override void OnDispose()
        {
            LogWarning("Dispose");
        }

        protected internal override void OnUpdate()
        {

        }

        protected internal override void OnSceneLoaded(string sceneName)
        {
            //LogError("Scene loaded: " + sceneName);
        }

        protected internal override void OnSceneUnloaded(string sceneName)
        {
            //LogError("Scene unloaded: " + sceneName);
        }
    }
}
