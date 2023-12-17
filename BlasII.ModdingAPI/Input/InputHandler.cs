using Il2CppTGK.Game;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.ModdingAPI.Input
{
    /// <summary>
    /// Provides access to custom keybindings and better input
    /// </summary>
    public class InputHandler
    {
        private readonly BlasIIMod _mod;

        private bool _registered = false;
        private readonly Dictionary<string, KeyCode> _keybindings = new();

        internal InputHandler(BlasIIMod mod) => _mod = mod;

        // Blocking

        /// <summary>
        /// Controls whether the input is blocked
        /// </summary>
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

        /// <summary>
        /// Checks whether the key was held on this frame
        /// </summary>
        public bool GetKey(string action)
        {
            return _keybindings.TryGetValue(action, out KeyCode key) && UnityEngine.Input.GetKey(key);
        }

        /// <summary>
        /// Checks whether the key was pressed on this frame
        /// </summary>
        public bool GetKeyDown(string action)
        {
            return _keybindings.TryGetValue(action, out KeyCode key) && UnityEngine.Input.GetKeyDown(key);
        }

        /// <summary>
        /// Checks whether the key was released on this frame
        /// </summary>
        public bool GetKeyUp(string action)
        {
            return _keybindings.TryGetValue(action, out KeyCode key) && UnityEngine.Input.GetKeyUp(key);
        }

        // Buttons

        /// <summary>
        /// Checks whether the button was held on this frame
        /// </summary>
        public bool GetButton(ButtonType button)
        {
            return InputStorage.TryGetButton(button, out var input) && CoreCache.Input.GetButton(input);
        }

        /// <summary>
        /// Checks whether the button was pressed on this frame
        /// </summary>
        public bool GetButtonDown(ButtonType button)
        {
            return InputStorage.TryGetButton(button, out var input) && CoreCache.Input.GetButtonDown(input);
        }

        /// <summary>
        /// Checks whether the button was released on this frame
        /// </summary>
        public bool GetButtonUp(ButtonType button)
        {
            return InputStorage.TryGetButton(button, out var input) && CoreCache.Input.GetButtonUp(input);
        }

        // Axes

        /// <summary>
        /// Checks the current direction of this axis
        /// </summary>
        public float GetAxis(AxisType axis)
        {
            return InputStorage.TryGetAxis(axis, out var input) ? CoreCache.Input.GetAxis(input) : 0;
        }

        // Custom keybindings

        /// <summary>
        /// Specifies which keybindings will be loaded and registers their defaults
        /// </summary>
        public void RegisterDefaultKeybindings(Dictionary<string, KeyCode> defaults)
        {
            if (_registered)
            {
                _mod.LogWarning("InputHandler has already been registered!");
                return;
            }
            _registered = true;

            foreach (var mapping in defaults)
            {
                _keybindings.Add(mapping.Key, mapping.Value);
            }

            DeserializeKeybindings(_mod.FileHandler.LoadKeybindings());
            _mod.FileHandler.SaveKeybindings(SerializeKeyBindings());
        }

        /// <summary>
        /// When saving the keybindings to a file, convert them to a list of strings
        /// </summary>
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

        /// <summary>
        /// When loading the keybindings from a file, convert and validate their keycodes
        /// </summary>
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

                // If the keybinding wasn't in the defaults, skip
                if (!_keybindings.ContainsKey(key))
                {
                    continue;
                }

                // If the keybinding was not a valid type, skip
                if (!System.Enum.TryParse(typeof(KeyCode), value, out object keyCode))
                {
                    _mod.LogError($"Keybinding '{key}' is invalid.  Using default instead.");
                    continue;
                }

                // Update the valid keybinding
                _keybindings[key] = (KeyCode)keyCode;
            }
        }
    }
}
