using System.Collections.Generic;

namespace BlasII.ModdingAPI.Config
{
    public class ConfigHandler
    {
        private readonly BlasIIMod _mod;

        private readonly Dictionary<string, string> _properties = new();

        public ConfigHandler(BlasIIMod mod)
        {
            _mod = mod;

            DeserializeProperties(_mod.FileHandler.LoadConfig());
        }

        public bool GetBool(string key)
        {
            if (_properties.TryGetValue(key, out string value) && bool.TryParse(value, out bool result))
                return result;

            _mod.LogError($"Failed to locate valid bool property for '{key}'");
            return false;
        }

        public int GetInt(string key)
        {
            if (_properties.TryGetValue(key, out string value) && int.TryParse(value, out int result))
                return result;

            _mod.LogError($"Failed to locate valid int property for '{key}'");
            return 0;
        }

        public float GetFloat(string key)
        {
            if (_properties.TryGetValue(key, out string value) && float.TryParse(value, out float result))
                return result;

            _mod.LogError($"Failed to locate valid float property for '{key}'");
            return 0.0f;
        }

        public string GetString(string key)
        {
            if (_properties.TryGetValue(key, out string value))
                return value;

            _mod.LogError($"Failed to locate valid string property for '{key}'");
            return string.Empty;
        }

        // Custom properties

        public void RegisterDefaultProperties(Dictionary<string, object> defaults)
        {
            foreach (var mapping in defaults)
            {
                if (!_properties.ContainsKey(mapping.Key))
                    _properties.Add(mapping.Key, mapping.Value.ToString());
            }

            _mod.FileHandler.SaveConfig(SerializeProperties());
        }

        private string[] SerializeProperties()
        {
            string[] properties = new string[_properties.Count];
            int currentIdx = 0;

            foreach (var mapping in _properties)
            {
                properties[currentIdx++] = $"{mapping.Key}: {mapping.Value}";
            }

            return properties;
        }

        private void DeserializeProperties(string[] properties)
        {
            foreach (string line in properties)
            {
                // Skip lines without a colon
                int colon = line.IndexOf(':');
                if (colon < 0)
                    continue;

                // Get key and value for each pair
                string key = line[..colon].Trim();
                string value = line[(colon + 1)..].Trim();

                // Add property
                _properties.Add(key, value);
            }
        }
    }
}
