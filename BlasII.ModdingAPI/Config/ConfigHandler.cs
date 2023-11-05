using System.Collections.Generic;

namespace BlasII.ModdingAPI.Config
{
    public class ConfigHandler
    {
        private readonly BlasIIMod _mod;

        private readonly Dictionary<string, object> _properties = new();

        public ConfigHandler(BlasIIMod mod)
        {
            _mod = mod;

            DeserializeProperties(_mod.FileHandler.LoadConfig());
        }

        public T GetProperty<T>(string key)
        {
            if (!_properties.TryGetValue(key, out object value))
            {
                _mod.LogError($"Property '{key}' does not exist!");
                return default;
            }

            return (T)value;
        }

        // Custom properties

        public void RegisterDefaultProperties(Dictionary<string, object> defaults)
        {
            foreach (var mapping in defaults)
            {
                // If the property wasn't in the config, just add it
                if (!_properties.ContainsKey(mapping.Key))
                {
                    _properties.Add(mapping.Key, mapping.Value);
                    continue;
                }

                // Validate the property, and possibly replace it with default
                string currentValue = (string)_properties[mapping.Key];
                switch (mapping.Value)
                {
                    case bool _:
                        if (bool.TryParse(currentValue, out bool boolValue))
                        {
                            _properties[mapping.Key] = boolValue;
                            continue;
                        }
                        break;
                    case int _:
                        if (int.TryParse(currentValue, out int intValue))
                        {
                            _properties[mapping.Key] = intValue;
                            continue;
                        }
                        break;
                    case float _:
                        if (float.TryParse(currentValue, out float floatValue))
                        {
                            _properties[mapping.Key] = floatValue;
                            continue;
                        }
                        break;
                    case string _:
                        continue;
                    default:
                        _mod.LogError($"Property '{mapping.Key}' has an invalid type!");
                        continue;
                }

                _mod.LogError($"Property '{mapping.Key}' is invalid.  Using default instead.");
                _properties[mapping.Key] = mapping.Value;
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
