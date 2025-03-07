
namespace BlasII.ModdingAPI.Persistence;

/// <summary>
/// Allows a mod to save and load persistent data with the game files
/// </summary>
public interface IPersistentMod
{
    /// <summary>
    /// Saves an object containing the mod's persistent data
    /// </summary>
    public SaveData SaveGame();

    /// <summary>
    /// Loads an object containing the mod's persistent data
    /// </summary>
    public void LoadGame(SaveData data);

    /// <summary>
    /// Resets the mod's persistent data
    /// </summary>
    public void ResetGame();
}
