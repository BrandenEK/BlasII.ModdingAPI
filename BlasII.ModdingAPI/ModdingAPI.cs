
namespace BlasII.ModdingAPI
{
    internal class ModdingAPI : BlasIIMod
    {
        public ModdingAPI() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        protected internal override void OnInitialize()
        {
            LogError("Initialize");
        }

        protected internal override void OnAllInitialized()
        {
            LogError("All initialized");
        }

        protected internal override void OnDispose()
        {
            LogError("Dispose");
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
