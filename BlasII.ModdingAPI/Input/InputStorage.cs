using Il2CppTGK.InputSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.ModdingAPI.Input
{
    internal static class InputStorage
    {
        private static readonly Dictionary<ButtonType, InputData> _inputButtons = new();
        private static readonly Dictionary<AxisType, InputData> _inputAxes = new();

        internal static void Initialize()
        {
            var inputs = Resources.FindObjectsOfTypeAll<InputData>();

            AddButtonInput(ButtonType.Attack, "Attack", inputs);
            AddButtonInput(ButtonType.Dash, "Dash", inputs);
            AddButtonInput(ButtonType.Flask, "Flask", inputs);
            AddButtonInput(ButtonType.Interact, "Interact", inputs);
            AddButtonInput(ButtonType.Jump, "Jump", inputs);
            AddButtonInput(ButtonType.Prayer, "Prayer", inputs);
            AddButtonInput(ButtonType.WeaponArt, "Weapon Art", inputs);

            AddButtonInput(ButtonType.ChangeWeapon, "Change Weapon", inputs);
            AddButtonInput(ButtonType.NextWeapon, "Next Weapon", inputs);
            AddButtonInput(ButtonType.PrevWeapon, "Prev Weapon", inputs);
            AddButtonInput(ButtonType.ChangeWeaponSlot1, "Change Weapon Slot 1", inputs);
            AddButtonInput(ButtonType.ChangeWeaponSlot2, "Change Weapon Slot 2", inputs);
            AddButtonInput(ButtonType.ChangeWeaponSlot3, "Change Weapon Slot 3", inputs);

            AddButtonInput(ButtonType.UIConfirm, "UI Confirm", inputs);
            AddButtonInput(ButtonType.UICancel, "UI Cancel", inputs);
            AddButtonInput(ButtonType.UIBumperLeft, "UI Shoulder Left", inputs);
            AddButtonInput(ButtonType.UITriggerLeft, "UI Shoulder Left 2", inputs);
            AddButtonInput(ButtonType.UIBumperRight, "UI Shoulder Right", inputs);
            AddButtonInput(ButtonType.UITriggerRight, "UI Shoulder Right 2", inputs);
            AddButtonInput(ButtonType.UICenter1, "UI Center 1", inputs);
            AddButtonInput(ButtonType.UICenter2, "UI Center 2", inputs);
            AddButtonInput(ButtonType.UITopRow1, "UI TopRow 1", inputs);
            AddButtonInput(ButtonType.UITopRow2, "UI TopRow 2", inputs);
            AddButtonInput(ButtonType.UIL3, "UI L3", inputs);
            AddButtonInput(ButtonType.UIR3, "UI R3", inputs);

            AddButtonInput(ButtonType.Engagement, "Engagement", inputs);
            AddButtonInput(ButtonType.Inventory, "Inventory", inputs);
            AddButtonInput(ButtonType.Pause, "Pause", inputs);
            AddButtonInput(ButtonType.L3, "L3", inputs);
            AddButtonInput(ButtonType.R3, "R3", inputs);

            AddAxisInput(AxisType.MoveHorizontal, "Move Horizontal +", inputs);
            AddAxisInput(AxisType.MoveVertical, "Move Vertical +", inputs);
            AddAxisInput(AxisType.MoveRHorizontal, "Move RHorizontal +", inputs);
            AddAxisInput(AxisType.MoveRVertical, "Move RVertical +", inputs);
            AddAxisInput(AxisType.UIHorizontal, "UI Horizontal +", inputs);
            AddAxisInput(AxisType.UIVertical, "UI Vertical +", inputs);

        }

        public static bool TryGetButton(ButtonType button, out InputData input) => _inputButtons.TryGetValue(button, out input);

        public static bool TryGetAxis(AxisType axis, out InputData input) => _inputAxes.TryGetValue(axis, out input);

        /// <summary>
        /// Finds an input data and registers it as a button type
        /// </summary>
        private static void AddButtonInput(ButtonType button, string name, InputData[] inputs)
        {
            foreach (var input in inputs)
            {
                if (input.name == name)
                {
                    _inputButtons.Add(button, input);
                    return;
                }
            }

            ModLog.Error("Failed to load input data for " + button);
        }

        /// <summary>
        /// Finds an input data and registers it as an axis type
        /// </summary>
        private static void AddAxisInput(AxisType axis, string name, InputData[] inputs)
        {
            foreach (var input in inputs)
            {
                if (input.name == name)
                {
                    _inputAxes.Add(axis, input);
                    return;
                }
            }

            ModLog.Error("Failed to load input data for " + axis);
        }
    }
}
