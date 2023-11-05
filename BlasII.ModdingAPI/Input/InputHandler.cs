using Il2CppTGK.Game;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.ModdingAPI.Input
{
    public class InputHandler
    {
        private readonly BlasIIMod _mod;

        private readonly Dictionary<string, KeyCode> _keybindings = new();

        public InputHandler(BlasIIMod mod)
        {
            _mod = mod;

            DeserializeKeybindings(_mod.FileHandler.LoadKeybindings());
        }

        // Keys

        public bool GetKey(string action)
        {
            return _keybindings.TryGetValue(action, out KeyCode key) && UnityEngine.Input.GetKey(key);
        }

        public bool GetKeyDown(string action)
        {
            return _keybindings.TryGetValue(action, out KeyCode key) && UnityEngine.Input.GetKeyDown(key);
        }

        public bool GetKeyUp(string action)
        {
            return _keybindings.TryGetValue(action, out KeyCode key) && UnityEngine.Input.GetKeyUp(key);
        }

        // Buttons

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

        // Axes

        public float GetAxis(AxisType axis)
        {
            return GetAxis(MapAxisToString(axis));
        }

        public float GetAxis(string axisName)
        {
            return CoreCache.Input.GetAxis(axisName);
        }

        // Mapping

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

                ButtonType.ChangeWeapon => "Change Weapon",
                ButtonType.NextWeapon => "Next Weapon",
                ButtonType.PrevWeapon => "Prev Weapon",
                ButtonType.ChangeWeaponSlot1 => "Change Weapon Slot 1",
                ButtonType.ChangeWeaponSlot2 => "Change Weapon Slot 2",
                ButtonType.ChangeWeaponSlot3 => "Change Weapon Slot 3",

                ButtonType.UIConfirm => "UI Confirm",
                ButtonType.UICancel => "UI Cancel",
                ButtonType.UIBumperLeft => "UI Shoulder Left",
                ButtonType.UITriggerLeft => "UI Shoulder Left 2",
                ButtonType.UIBumperRight => "UI Shoulder Right",
                ButtonType.UITriggerRight => "UI Shoulder Right 2",
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

        private string MapAxisToString(AxisType axis)
        {
            return axis switch
            {
                AxisType.MoveHorizontal => "Move Horizontal",
                AxisType.MoveVertical => "Move Vertical",
                AxisType.MoveHorizontalFake => "Fake Move Horizontal",
                AxisType.MoveVerticalFake => "Fake Move Vertical",
                AxisType.MoveRHorizontal => "Move RHorizontal",
                AxisType.MoveRVertical => "Move RVertical",
                AxisType.MoveFake => "Fake No Move 2",

                AxisType.UIHorizontal => "UI Horizontal",
                AxisType.UIVertical => "UI Vertical",

                _ => throw new System.Exception("Invalid axis code")
            };
        }

        // Key serialization

        public void RegisterDefaultKeybindings(Dictionary<string, KeyCode> defaults)
        {
            foreach (var mapping in defaults)
            {
                if (!_keybindings.ContainsKey(mapping.Key))
                    _keybindings.Add(mapping.Key, mapping.Value);
            }

            _mod.FileHandler.SaveKeybindings(SerializeKeyBindings());
        }

        private string[] SerializeKeyBindings()
        {
            string[] keys = new string[_keybindings.Count];
            int currentIdx = 0;

            foreach (var mapping in _keybindings)
            {
                keys[currentIdx++] = $"{mapping.Key}: {mapping.Value}";
            }

            return keys;
        }

        private void DeserializeKeybindings(string[] keys)
        {
            foreach (string line in keys)
            {
                // Skip lines without a colon
                int colon = line.IndexOf(':');
                if (colon < 0)
                    continue;

                // Get action and key for each pair
                string key = line[..colon].Trim();
                string value = line[(colon + 1)..].Trim();

                // Parse value to a key code
                if (!System.Enum.TryParse(typeof(KeyCode), value, out object keyCode))
                {
                    _mod.LogError($"Failed to convert '{value}' to a keycode. Using default instead.");
                    continue;
                }

                // Add custom keybinding
                _keybindings.Add(key, (KeyCode)keyCode);
            }
        }
    }
}
