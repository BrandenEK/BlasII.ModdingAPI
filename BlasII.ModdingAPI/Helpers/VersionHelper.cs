﻿
namespace BlasII.ModdingAPI.Helpers;

/// <summary>
/// Provides information about the version of the game
/// </summary>
public static class VersionHelper
{
    /// <summary>
    /// The build version of the game executable
    /// </summary>
    public static string GameVersion { get; internal set; } = "Unknown";

}
