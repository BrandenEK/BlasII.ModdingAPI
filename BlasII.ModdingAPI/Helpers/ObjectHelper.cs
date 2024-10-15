using UnityEngine;

namespace BlasII.ModdingAPI.Helpers;

/// <summary>
/// Provides access to persistent gameobjects
/// </summary>
public static class ObjectHelper
{
    /// <summary>
    /// A persistent gameobject for all mods to use
    /// </summary>
    public static GameObject ModObject { get; internal set; }
}
