using Il2CppTMPro;
using UnityEngine;

namespace BlasII.ModdingAPI.UI;

/// <summary>
/// Settings used when creating a new TextMeshProUGUI
/// </summary>
public class TextCreationOptions
{
    /// <summary> Default: "" </summary>
    public string Contents { get; init; } = string.Empty;

    /// <summary> Default: White </summary>
    public Color Color { get; init; } = Color.white;

    /// <summary> Default: 16.0 </summary>
    public float FontSize { get; init; } = 16f;

    /// <summary> Default: Centered </summary>
    public TextAlignmentOptions Alignment { get; init; } = TextAlignmentOptions.Center;

    /// <summary> Default: false </summary>
    public bool WordWrap { get; init; } = false;

    /// <summary> Default: Blasphemous </summary>
    public TMP_FontAsset Font { get; init; } = UIModder.Fonts.Default;
}
