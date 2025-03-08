using System;

namespace BlasII.ModdingAPI.Persistence;

/// <summary>
/// A mod that saves data with the global save file
/// </summary>
public interface IGlobalPersistentMod
{
    /// <summary>
    /// The type of the serialized data
    /// </summary>
    public Type GlobalDataType { get; }

    /// <summary>
    /// Saves the global data to an object
    /// </summary>
    public GlobalSaveData Save();

    /// <summary>
    /// Loads the global data from an object
    /// </summary>
    public void Load(GlobalSaveData data);
}


// TEMP !!!
public class TestGlobalSaveData : GlobalSaveData
{
    public int Number { get; set; }
}
