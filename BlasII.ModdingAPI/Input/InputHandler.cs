using BlasII.ModdingAPI.Storage;
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

        // Blocking

        public bool InputBlocked
        {
            get => CoreCache.Input.InputBlocked;
            set
            {
                if (value)
                {
                    CoreCache.Input.SetInputBlock(true, false);
                }
                else
                {
                    CoreCache.Input.ClearAllInputBlocks();
                }
            }
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
            return InputStorage.TryGetButton(button, out var input) && CoreCache.Input.GetButton(input);
        }

        public bool GetButtonDown(ButtonType button)
        {
            return InputStorage.TryGetButton(button, out var input) && CoreCache.Input.GetButtonDown(input);
        }

        public bool GetButtonUp(ButtonType button)
        {
            return InputStorage.TryGetButton(button, out var input) && CoreCache.Input.GetButtonUp(input);
        }

        // Axes

        public float GetAxis(AxisType axis)
        {
            return InputStorage.TryGetAxis(axis, out var input) ? CoreCache.Input.GetAxis(input) : 0;
        }

        // Custom keybindings

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
