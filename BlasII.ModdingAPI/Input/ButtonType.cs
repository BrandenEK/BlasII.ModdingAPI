
namespace BlasII.ModdingAPI.Input
{
    public enum ButtonType
    {
        // Actions
        Attack = 0,
        Dash = 1,
        Flask = 2,
        Interact = 3,
        Jump = 4,
        Prayer = 5,
        WeaponArt = 6,

        // Weapon changing
        ChangeWeapon = 40,
        NextWeapon = 41,
        PrevWeapon = 42,
        ChangeWeaponSlot1 = 43,
        ChangeWeaponSlot2 = 44,
        ChangeWeaponSlot3 = 45,

        // UI
        UIConfirm = 60,
        UICancel = 61,
        UIBumperLeft = 62,
        UITriggerLeft = 63,
        UIBumperRight = 64,
        UITriggerRight = 65,
        UICenter1 = 66,
        UICenter2 = 67,
        UITopRow1 = 68,
        UITopRow2 = 69,
        UIL3 = 70,
        UIR3 = 71,

        // Other
        Engagement = 80,
        Inventory = 81,
        Pause = 82,
        L3 = 83,
        R3 = 84,
    }
}
