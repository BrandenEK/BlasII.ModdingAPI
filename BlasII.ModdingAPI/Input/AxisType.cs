
namespace BlasII.ModdingAPI.Input;

/// <summary>
/// An in-game axis
/// </summary>
public enum AxisType
{
    // Movement

    /// <summary> Left stick </summary>
    MoveHorizontal = 0,
    /// <summary> Left stick </summary>
    MoveVertical = 1,
    /// <summary> Right stick </summary>
    MoveRHorizontal = 2,
    /// <summary> Right stick </summary>
    MoveRVertical = 3,

    // UI

    /// <summary> Left stick </summary>
    UIHorizontal = 20,
    /// <summary> Left stick </summary>
    UIVertical = 21,
}
