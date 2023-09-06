
namespace BlasII.ModdingAPI
{
    public enum LoadStatus
    {
        Unloaded = 0,
        LoadedMainMenu = 1,
        LoadedGame = 2,
        LoadedAny = LoadedMainMenu | LoadedGame,
    }
}
