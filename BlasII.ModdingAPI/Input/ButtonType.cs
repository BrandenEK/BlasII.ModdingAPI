
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

        // Movement
        MoveHorizontal,
        MoveVertical,
        MoveHorizontalFake,
        MoveVerticalFake,
        MoveRHorizontal,
        MoveRVertical,
        MoveFake,

        // Weapon changing
        ChangeWeapon,
        NextWeapon,
        PrevWeapon,
        ChangeWeaponSlot1,
        ChangeWeaponSlot2,
        ChangeWeaponSlot3,

        // UI
        UIConfirm,
        UICancel,
        UIHorizontal,
        UIHorizontalRight,
        UIVertical,
        UIVerticalRight,
        UIShoulderLeft,
        UIShoulderLeft2,
        UIShoulderRight,
        UIShoulderRight2,
        UICenter1,
        UICenter2,
        UITopRow1,
        UITopRow2,
        UIL3,
        UIR3,

        // Other
        Engagement,
        Inventory,
        Pause,
        L3,
        R3,
    }
}
