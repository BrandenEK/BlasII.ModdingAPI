using System.Collections.Generic;

namespace BlasII.ModdingAPI.Config
{
    public class ConfigHandler
    {
        private readonly BlasIIMod _mod;

        private readonly Dictionary<string, string> _tempProperties = new();
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

            if (value.GetType() != typeof(T))
            {
                _mod.LogError($"Property '{key}' is the wrong type!");
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
                if (!_tempProperties.TryGetValue(mapping.Key, out string currentValue))
                {
                    _properties.Add(mapping.Key, mapping.Value);
                    continue;
                }

                // Validate the property, and possibly replace it with default
                object resultValue = null;
                switch (mapping.Value)
                {
                    case bool _:
                        if (bool.TryParse(currentValue, out bool bResult))
                            resultValue = bResult;
                        break;
                    case int _:
                        if (int.TryParse(currentValue, out int iResult))
                            resultValue = iResult;
                        break;
                    case float _:
                        if (float.TryParse(currentValue, out float fResult))
                            resultValue = fResult;
                        break;
                    case string _:
                        resultValue = currentValue;
                        break;
                    default:
                        _mod.LogError($"Property '{mapping.Key}' has an invalid type!");
                        continue;
                }

                if (resultValue == null)
                {
                    _mod.LogError($"Property '{mapping.Key}' is invalid.  Using default instead.");
                    _properties.Add(mapping.Key, mapping.Value);
                    continue;
                }

                _properties.Add(mapping.Key, resultValue);
            }

            _tempProperties.Clear();
            _mod.FileHandler.SaveConfig(SerializeProperties());
        }

        /// <summary>
        /// After registering defaults, save all properties
        /// </summary>
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

        /// <summary>
        /// Before initialization, load all properties as strings into a temporary list
        /// </summary>
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
                _tempProperties.Add(key, value);
            }
        }
    }
}
