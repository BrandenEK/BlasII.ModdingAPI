using System.Collections.Generic;

namespace BlasII.ModdingAPI.Config
{
    public class ConfigHandler
    {
        private readonly BlasIIMod _mod;

        private readonly Dictionary<string, string> _tempProperties = new();
        private readonly Dictionary<string, object> _properties = new();

        internal ConfigHandler(BlasIIMod mod)
        {
            _mod = mod;

            DeserializeProperties(_mod.FileHandler.LoadConfig());
        }

        /// <summary>
        /// Gets the value of the specified property in the config
        /// </summary>
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

        /// <summary>
        /// Sets the value of the specified property in the config
        /// </summary>
        public void SetProperty<T>(string key, T value)
        {
            if (!_properties.TryGetValue(key, out object currentValue))
            {
                _mod.LogError($"Property '{key}' does not exist!");
                return;
            }

            if (currentValue.GetType() != typeof(T))
            {
                _mod.LogError($"Property '{key}' is the wrong type!");
                return;
            }

            _properties[key] = value;
        }

        /// <summary>
        /// Saves the current properties to the config file
        /// </summary>
        public void SaveConfig()
        {
            _mod.FileHandler.SaveConfig(SerializeProperties());
        }

        // Custom properties

        /// <summary>
        /// Specifies which properties will be loaded from the config file and registers their defaults
        /// </summary>
        public void RegisterDefaultProperties(Dictionary<string, object> defaults)
        {
            foreach (var mapping in defaults)
            {
                // If the property wasn't in the config, use default
                if (!_tempProperties.TryGetValue(mapping.Key, out string currentValue))
                {
                    _properties.Add(mapping.Key, mapping.Value);
                    continue;
                }

                // If the property was not a valid type, skip property
                if (!ParseProperty(currentValue, mapping.Value, out object realValue))
                {
                    _mod.LogError($"Property '{mapping.Key}' has an invalid type!");
                    continue;
                }

                // If the property was invalid, use default
                if (realValue == null)
                {
                    _mod.LogError($"Property '{mapping.Key}' is invalid.  Using default instead.");
                    _properties.Add(mapping.Key, mapping.Value);
                    continue;
                }

                _properties.Add(mapping.Key, realValue);
            }

            _tempProperties.Clear();
            SaveConfig();
        }

        /// <summary>
        /// Attempts to parse the property into its specified type and returns whether it was a valid type
        /// </summary>
        private bool ParseProperty(string currentValue, object defaultValue, out object realValue)
        {
            realValue = null;
            switch (defaultValue)
            {
                case bool _:
                    if (bool.TryParse(currentValue, out bool boolValue))
                        realValue = boolValue;
                    return true;
                case int _:
                    if (int.TryParse(currentValue, out int intValue))
                        realValue = intValue;
                    return true;
                case float _:
                    if (float.TryParse(currentValue, out float floatValue))
                        realValue = floatValue;
                    return true;
                case string _:
                    realValue = currentValue;
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Converts the properties dictionary to a string[] to be saved to a file
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
