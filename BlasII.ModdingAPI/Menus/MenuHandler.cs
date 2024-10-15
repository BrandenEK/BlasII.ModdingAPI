
namespace BlasII.ModdingAPI.Menus;

/// <summary>
/// Provides access to creating in-game menus
/// </summary>
public class MenuHandler
{
    private readonly BlasIIMod _mod;

    internal MenuHandler(BlasIIMod mod) => _mod = mod;

    /// <summary>
    /// Registers a menu to appear before starting a new game
    /// </summary>
    public void RegisterNewGameMenu(BaseMenu menu)
    {
        ModLog.Info($"Registering NewGame menu: " + menu.GetType().Name);
        menu.OwnerMod = _mod;
        MenuModder.AddNewGameMenu(menu);
    }

    /// <summary>
    /// Registers a menu to appear before loading an existing game
    /// </summary>
    public void RegisterLoadGameMenu(BaseMenu menu)
    {
        ModLog.Info($"Registering LoadGame menu: " + menu.GetType().Name);
        menu.OwnerMod = _mod;
        MenuModder.AddLoadGameMenu(menu);
    }
}
