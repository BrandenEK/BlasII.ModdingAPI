﻿using System;

namespace BlasII.ModdingAPI.Persistence;

/// <summary>
/// A mod that saves data with the global save file
/// </summary>
//public interface IGlobalPersistentMod
//{
//    ///// <summary>
//    ///// The type of the serialized data
//    ///// </summary>
//    //public Type GlobalDataType { get; }

//    ///// <summary>
//    ///// Saves the global data to an object
//    ///// </summary>
//    //public GlobalSaveData SaveGlobal();

//    ///// <summary>
//    ///// Loads the global data from an object
//    ///// </summary>
//    //public void LoadGlobal(GlobalSaveData data);
//}

/// <summary>
/// A mod that saves data with the global save file
/// </summary>
public interface IGlobalPersistentMod<TData> where TData : GlobalSaveData
{
    /// <summary>
    /// Saves the global data to an object
    /// </summary>
    public TData SaveGlobal();

    /// <summary>
    /// Loads the global data from an object
    /// </summary>
    public void LoadGlobal(TData data);
}


// TEMP !!!
public class TestGlobalSaveData : GlobalSaveData
{
    public int Number { get; set; }
}
