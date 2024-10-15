
namespace BlasII.ModdingAPI.Input;

/// <summary>
/// An in-game button
/// </summary>
public enum ButtonType
{
    // Actions

    /// <summary> X </summary>
    Attack = 0,
    /// <summary> RT </summary>
    Dash = 1,
    /// <summary> LB </summary>
    Flask = 2,
    /// <summary> Y </summary>
    Interact = 3,
    /// <summary> A </summary>
    Jump = 4,
    /// <summary> B </summary>
    Prayer = 5,
    /// <summary> LT </summary>
    WeaponArt = 6,

    // Weapon changing

    /// <summary> Input </summary>
    ChangeWeapon = 40,
    /// <summary> RB </summary>
    NextWeapon = 41,
    /// <summary> Input </summary>
    PrevWeapon = 42,
    /// <summary> Input </summary>
    ChangeWeaponSlot1 = 43,
    /// <summary> Input </summary>
    ChangeWeaponSlot2 = 44,
    /// <summary> Input </summary>
    ChangeWeaponSlot3 = 45,

    // UI

    /// <summary> A </summary>
    UIConfirm = 60,
    /// <summary> B </summary>
    UICancel = 61,
    /// <summary> LB </summary>
    UIBumperLeft = 62,
    /// <summary> LT </summary>
    UITriggerLeft = 63,
    /// <summary> RB </summary>
    UIBumperRight = 64,
    /// <summary> RT </summary>
    UITriggerRight = 65,
    /// <summary> Input </summary>
    UICenter1 = 66,
    /// <summary> Input </summary>
    UICenter2 = 67,
    /// <summary> Input </summary>
    UITopRow1 = 68,
    /// <summary> Input </summary>
    UITopRow2 = 69,
    /// <summary> L3 </summary>
    UIL3 = 70,
    /// <summary> R3 </summary>
    UIR3 = 71,

    // Other

    /// <summary> Input </summary>
    Engagement = 80,
    /// <summary> I </summary>
    Inventory = 81,
    /// <summary> M </summary>
    Pause = 82,
    /// <summary> L3 </summary>
    L3 = 83,
    /// <summary> R3 </summary>
    R3 = 84,
}
