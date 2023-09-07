
namespace BlasII.ModdingAPI
{
    public class LoadStatus
    {
        public bool GameSceneLoaded => AnySceneLoaded && !MenuSceneLoaded;

        public bool MenuSceneLoaded => Main.ModLoader.CurrentScene == "MainMenu";

        public bool AnySceneLoaded => Main.ModLoader.CurrentScene.Length > 0;

        public string CurrentScene => Main.ModLoader.CurrentScene;
    }
}
