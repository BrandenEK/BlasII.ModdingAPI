using UnityEngine;

namespace BlasII.ModdingAPI.UI;

/// <summary>
/// Settings used when creating a new RectTransform
/// </summary>
public class RectCreationOptions
{
    /// <summary> Default: "New Rect" </summary>
    public string Name { get; init; } = "New Rect";

    /// <summary> Default: Canvas </summary>
    public Transform Parent { get; init; } = UIModder.Parents.Default;

    /// <summary> Default: (0.5, 0.5) </summary>
    public Vector2 XRange { get; init; } = new Vector2(0.5f, 0.5f);

    /// <summary> Default: (0.5, 0.5) </summary>
    public Vector2 YRange { get; init; } = new Vector2(0.5f, 0.5f);

    /// <summary> Default: (0.5, 0.5) </summary>
    public Vector2 Pivot { get; init; } = new Vector2(0.5f, 0.5f);

    /// <summary> Default: (0, 0) </summary>
    public Vector2 Position { get; init; } = Vector2.zero;

    /// <summary> Default: (100, 100) </summary>
    public Vector2 Size { get; init; } = new Vector2(100, 100);
}
