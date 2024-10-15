using System.Collections.Generic;

namespace BlasII.ModdingAPI.Config;

/// <summary>
/// Provides access to saving and loading configuration properties
/// </summary>
public class LegacyConfigHandler
{
    private readonly BlasIIMod _mod;

    private bool _registered = false;
    private readonly Dictionary<string, object> _properties = new();

    internal LegacyConfigHandler(BlasIIMod mod) => _mod = mod;

    /// <summary>
    /// Gets the value of the specified property in the config
    /// </summary>
    public T GetProperty<T>(string key)
    {
        if (!_properties.TryGetValue(key, out object value))
        {
            ModLog.Error($"Property '{key}' does not exist!", _mod);
            return default;
        }

        if (value.GetType() != typeof(T))
        {
            ModLog.Error($"Property '{key}' is the wrong type!", _mod);
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
            ModLog.Error($"Property '{key}' does not exist!", _mod);
            return;
        }

        if (currentValue.GetType() != typeof(T))
        {
            ModLog.Error($"Property '{key}' is the wrong type!", _mod);
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
        if (_registered)
        {
            ModLog.Warn("ConfigHandler has already been registered!", _mod);
            return;
        }
        _registered = true;

        foreach (var mapping in defaults)
        {
            _properties.Add(mapping.Key, mapping.Value);
        }

        DeserializeProperties(_mod.FileHandler.LoadConfig());
        _mod.FileHandler.SaveConfig(SerializeProperties());
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

            // If the property wasn't in the defaults, skip
            if (!_properties.TryGetValue(key, out object defaultValue))
            {
                continue;
            }

            // If the property was not a valid type, skip
            if (!ParseProperty(value, defaultValue, out object realValue))
            {
                ModLog.Error($"Property '{key}' is invalid.  Using default instead.", _mod);
                continue;
            }

            // Update the valid property
            _properties[key] = realValue;
        }
    }

    /// <summary>
    /// Attempts to parse the property into its specified type and returns whether it is valid
    /// </summary>
    private bool ParseProperty(string newValue, object defaultValue, out object realValue)
    {
        bool isValid;

        switch (defaultValue)
        {
            case bool _:
                isValid = bool.TryParse(newValue, out bool boolValue);
                realValue = boolValue;
                break;
            case int _:
                isValid = int.TryParse(newValue, out int intValue);
                realValue = intValue;
                break;
            case float _:
                isValid = float.TryParse(newValue, out float floatValue);
                realValue = floatValue;
                break;
            case string _:
                isValid = true;
                realValue = newValue;
                break;
            default:
                isValid = false;
                realValue = null;
                break;
        }

        return isValid;
    }
}
