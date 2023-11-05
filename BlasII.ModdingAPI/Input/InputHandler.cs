using Il2CppTGK.Game;

namespace BlasII.ModdingAPI.Input
{
    public class InputHandler
    {
        public InputHandler(string[] keybindings)
        {

        }

        public bool GetButton(ButtonType button)
        {
            return GetButton(MapButtonToString(button));
        }
        public bool GetButton(string buttonName)
        {
            return CoreCache.Input.GetButton(buttonName);
        }

        public bool GetButtonDown(ButtonType button)
        {
            return GetButtonDown(MapButtonToString(button));
        }
        public bool GetButtonDown(string buttonName)
        {
            return CoreCache.Input.GetButtonDown(buttonName);
        }

        public bool GetButtonUp(ButtonType button)
        {
            return GetButtonUp(MapButtonToString(button));
        }
        public bool GetButtonUp(string buttonName)
        {
            return CoreCache.Input.GetButtonUp(buttonName);
        }

        private string MapButtonToString(ButtonType button)
        {
            return button switch
            {
                ButtonType.Attack => "Attack",
                ButtonType.Dash => "Dash",
                ButtonType.Flask => "Flask",
                ButtonType.Interact => "Interact",
                ButtonType.Jump => "Jump",
                ButtonType.Prayer => "Prayer",
                ButtonType.WeaponArt => "Weapon Art",

                ButtonType.MoveHorizontal => "Move Horizontal",
                ButtonType.MoveVertical => "Move Vertical",
                ButtonType.MoveHorizontalFake => "Fake Move Horizontal",
                ButtonType.MoveVerticalFake => "Fake Move Vertical",
                ButtonType.MoveRHorizontal => "Move RHorizontal",
                ButtonType.MoveRVertical => "Move RVertical",
                ButtonType.MoveFake => "Fake No Move 2",

                ButtonType.ChangeWeapon => "Change Weapon",
                ButtonType.NextWeapon => "Next Weapon",
                ButtonType.PrevWeapon => "Prev Weapon",
                ButtonType.ChangeWeaponSlot1 => "Change Weapon Slot 1",
                ButtonType.ChangeWeaponSlot2 => "Change Weapon Slot 2",
                ButtonType.ChangeWeaponSlot3 => "Change Weapon Slot 3",

                ButtonType.UIConfirm => "UI Confirm",
                ButtonType.UICancel => "UI Cancel",
                ButtonType.UIHorizontal => "UI Horizontal",
                ButtonType.UIHorizontalRight => "UI Horizontal Right",
                ButtonType.UIVertical => "UI Vertical",
                ButtonType.UIVerticalRight => "UI Vertical Right",
                ButtonType.UIShoulderLeft => "UI Shoulder Left",
                ButtonType.UIShoulderLeft2 => "UI Shoulder Left 2",
                ButtonType.UIShoulderRight => "UI Shoulder Right",
                ButtonType.UIShoulderRight2 => "UI Shoulder Right 2",
                ButtonType.UICenter1 => "UI Center 1",
                ButtonType.UICenter2 => "UI Center 2",
                ButtonType.UITopRow1 => "UI TopRow 1",
                ButtonType.UITopRow2 => "UI TopRow 2",
                ButtonType.UIL3 => "UI L3",
                ButtonType.UIR3 => "UI R3",

                ButtonType.Engagement => "Engagement",
                ButtonType.Inventory => "Inventory",
                ButtonType.Pause => "Pause",
                ButtonType.L3 => "L3",
                ButtonType.R3 => "R3",
                _ => throw new System.Exception("Invalid button code")
            };
        }
    }
}
