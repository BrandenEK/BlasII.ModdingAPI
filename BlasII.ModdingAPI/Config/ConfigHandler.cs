using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace BlasII.ModdingAPI.Config;

/// <summary>
/// Provides access to saving and loading configuration properties
/// </summary>
public class ConfigHandler
{
    private readonly BlasIIMod _mod;

    internal ConfigHandler(BlasIIMod mod) => _mod = mod;

    /// <summary>
    /// Loads the properties from the config file
    /// </summary>
    public T Load<T>() where T : new()
    {
        T config;
        try
        {
            config = JsonConvert.DeserializeObject<T>(File.ReadAllText(ConfigPath));
        }
        catch
        {
            ModLog.Error($"Failed to read config - Using default", _mod);
            config = new T();
        }

        Save(config);
        return config;
    }

    /// <summary>
    /// Saves the current properties to the config file
    /// </summary>
    public void Save<T>(T config)
    {
        JsonSerializerSettings jss = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        };

        string json = JsonConvert.SerializeObject(config, jss);
        File.WriteAllText(ConfigPath, json);
    }

    private string ConfigPath
    {
        get
        {
            var path = new FileInfo(Path.Combine(_mod.FileHandler.ModdingFolder, "config", $"{_mod.Name}.cfg"));
            path.Directory.Create();
            return path.FullName;
        }
    }
}
