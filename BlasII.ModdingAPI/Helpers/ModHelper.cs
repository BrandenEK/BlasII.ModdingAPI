using System;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Helpers;

/// <summary>
/// Provides information about which mods are loaded
/// </summary>
public static class ModHelper
{
    /// <summary>
    /// All mods that have been loaded
    /// </summary>
    public static IEnumerable<BlasIIMod> LoadedMods { get; internal set; }

    // Search by predicate

    /// <summary>
    /// Retrieves a mod based on a predicate
    /// </summary>
    public static BlasIIMod GetMod(Func<BlasIIMod, bool> predicate)
    {
        return LoadedMods.First(predicate);
    }

    /// <summary>
    /// Attempts to retrieve a mod based on a predicate
    /// </summary>
    public static bool TryGetMod(Func<BlasIIMod, bool> predicate, out BlasIIMod mod)
    {
        return (mod = LoadedMods.FirstOrDefault(predicate)) != null;
    }

    /// <summary>
    /// Checks if a mod is loaded based on a predicate
    /// </summary>
    public static bool IsModLoaded(Func<BlasIIMod, bool> predicate)
    {
        return LoadedMods.Any(predicate);
    }

    // Search by Id

    /// <summary>
    /// Retrieves a mod by its id
    /// </summary>
    public static BlasIIMod GetModById(string id) => GetMod(x => x.Id == id);

    /// <summary>
    /// Attempts to retrieve a mod by its id
    /// </summary>
    public static bool TryGetModById(string id, out BlasIIMod mod) => TryGetMod(x => x.Id == id, out mod);

    /// <summary>
    /// Checks if a mod is loaded by its id
    /// </summary>
    public static bool IsModLoadedById(string id) => IsModLoaded(x => x.Id == id);

    // Search by name

    /// <summary>
    /// Retrieves a mod by its name
    /// </summary>
    public static BlasIIMod GetModByName(string name) => GetMod(x => x.Name == name);

    /// <summary>
    /// Attempts to retrieve a mod by its name
    /// </summary>
    public static bool TryGetModByName(string name, out BlasIIMod mod) => TryGetMod(x => x.Name == name, out mod);

    /// <summary>
    /// Checks if a mod is loaded by its name
    /// </summary>
    public static bool IsModLoadedByName(string name) => IsModLoaded(x => x.Name == name);
}
