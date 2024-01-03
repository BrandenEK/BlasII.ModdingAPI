using UnityEngine;

namespace BlasII.ModdingAPI.UI;

/// <summary>
/// Settings used when creating a new Image
/// </summary>
public class ImageCreationOptions
{
    /// <summary> Default: null </summary>
    public Sprite Sprite { get; init; } = null;

    /// <summary> Default: White </summary>
    public Color Color { get; init; } = Color.white;
}
