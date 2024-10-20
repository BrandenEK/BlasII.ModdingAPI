using BlasII.ModdingAPI.Helpers;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace BlasII.ModdingAPI.Files;

/// <summary>
/// Provides access to loading data files and other IO methods
/// </summary>
public class FileHandler
{
    //private readonly string configPath;
    private readonly string contentPath;
    private readonly string dataPath;
    private readonly string keybindingsPath;
    private readonly string localizationPath;

    /// <summary>
    /// The full path of the game's root folder
    /// </summary>
    public string RootFolder { get; } = Directory.GetCurrentDirectory();

    /// <summary>
    /// The full path of the game's modding folder
    /// </summary>
    public string ModdingFolder { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Modding");

    /// <summary>
    /// The full path of this mod's content folder
    /// </summary>
    public string ContentFolder
    {
        get
        {
            EnsureDirectoryExists(contentPath);
            return contentPath;
        }
    }

    internal FileHandler(BlasIIMod mod)
    {
        //configPath = Path.GetFullPath($"Modding/config/{mod.Name}.cfg");
        contentPath = Path.Combine(ModdingFolder, "content", mod.Name);
        dataPath = Path.Combine(ModdingFolder, "data", mod.Name);
        keybindingsPath = Path.Combine(ModdingFolder, "keybindings", $"{mod.Name}.txt");
        localizationPath = Path.Combine(ModdingFolder, "localization", $"{mod.Name}.txt");
    }

    // General

    /// <summary>
    /// Returns the string contents of a file if it could be read
    /// </summary>
    private bool ReadFileContents(string path, out string output)
    {
        if (File.Exists(path))
        {
            output = File.ReadAllText(path);
            return true;
        }

        output = null;
        return false;
    }

    /// <summary>
    /// Returns the string[] contents of a file if it could be read
    /// </summary>
    private bool ReadFileLines(string path, out string[] output)
    {
        if (File.Exists(path))
        {
            output = File.ReadAllLines(path);
            return true;
        }

        output = null;
        return false;
    }

    /// <summary>
    /// Returns the byte[] contents of a file if it could be read
    /// </summary>
    private bool ReadFileBytes(string path, out byte[] output)
    {
        if (File.Exists(path))
        {
            output = File.ReadAllBytes(path);
            return true;
        }

        output = null;
        return false;
    }

    /// <summary>
    /// Before writing to a file, create the directory if it doesnt already exist
    /// </summary>
    private void EnsureDirectoryExists(string path)
    {
        string directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }

    // Data

    /// <summary>
    /// Loads the data as a string, if it exists
    /// </summary>
    public bool LoadDataAsText(string fileName, out string output)
    {
        return ReadFileContents(Path.Combine(dataPath, fileName), out output);
    }

    /// <summary>
    /// Loads the data as a json object, if it exists
    /// </summary>
    public bool LoadDataAsJson<T>(string fileName, out T output)
    {
        if (ReadFileContents(Path.Combine(dataPath, fileName), out string text))
        {
            output = JsonConvert.DeserializeObject<T>(text);
            return true;
        }

        output = default;
        return false;
    }

    /// <summary>
    /// Loads the data as a string[], if it exists
    /// </summary>
    public bool LoadDataAsArray(string fileName, out string[] output)
    {
        return ReadFileLines(Path.Combine(dataPath, fileName), out output);
    }

    /// <summary>
    /// Loads the data as a Texture2D, if it exists
    /// </summary>
    public bool LoadDataAsTexture(string fileName, out Texture2D output)
    {
        if (!ReadFileBytes(Path.Combine(dataPath, fileName), out byte[] bytes))
        {
            output = null;
            return false;
        }

        output = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        output.LoadImage(bytes, false);

        var sprite = Sprite.Create(output, new Rect(0, 0, output.width, output.height), Vector2.zero);
        RegisterSpriteOnObject(sprite);

        return true;
    }

    /// <summary>
    /// Loads the data as a Sprite, if it exists
    /// </summary>
    public bool LoadDataAsSprite(string fileName, out Sprite output, SpriteImportOptions options)
    {
        if (!ReadFileBytes(Path.Combine(dataPath, fileName), out byte[] bytes))
        {
            output = null;
            return false;
        }

        var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes, false);

        if (options.UsePointFilter)
            texture.filterMode = FilterMode.Point;

        var rect = new Rect(0, 0, texture.width, texture.height);
        output = Sprite.Create(texture, rect, options.Pivot, options.PixelsPerUnit, 0, options.MeshType, options.Border);
        RegisterSpriteOnObject(output);

        return true;
    }

    /// <summary>
    /// Loads the data as a Sprite (Default options), if it exists
    /// </summary>
    public bool LoadDataAsSprite(string fileName, out Sprite output) =>
        LoadDataAsSprite(fileName, out output, new SpriteImportOptions());

    /// <summary>
    /// Loads the data as a Sprite[], if it exists
    /// </summary>
    public bool LoadDataAsVariableSpritesheet(string fileName, Rect[] rects, out Sprite[] output, SpriteImportOptions options)
    {
        if (!ReadFileBytes(Path.Combine(dataPath, fileName), out byte[] bytes))
        {
            output = null;
            return false;
        }

        var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes, false);

        if (options.UsePointFilter)
            texture.filterMode = FilterMode.Point;

        output = new Sprite[rects.Length];

        for (int i = 0; i < rects.Length; i++)
        {
            Rect rect = rects[i];
            if (rect.x < 0 || rect.x + rect.width > texture.width ||
                rect.y < 0 || rect.y + rect.height > texture.height)
                throw new Exception($"Invalid rect for {fileName}: {rect}");

            Sprite sprite = Sprite.Create(texture, rect, options.Pivot, options.PixelsPerUnit, 0, options.MeshType, options.Border);
            RegisterSpriteOnObject(sprite);
            output[i] = sprite;
        }

        return true;
    }

    /// <summary>
    /// Loads the data as a Sprite[] (Default options), if it exists
    /// </summary>
    public bool LoadDataAsVariableSpritesheet(string fileName, Rect[] rects, out Sprite[] output) =>
        LoadDataAsVariableSpritesheet(fileName, rects, out output, new SpriteImportOptions());

    /// <summary>
    /// Loads the data as a Sprite[], if it exists
    /// </summary>
    public bool LoadDataAsFixedSpritesheet(string fileName, Vector2 size, out Sprite[] output, SpriteImportOptions options)
    {
        if (!ReadFileBytes(Path.Combine(dataPath, fileName), out byte[] bytes))
        {
            output = null;
            return false;
        }

        var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes, false);

        if (options.UsePointFilter)
            texture.filterMode = FilterMode.Point;

        int totalWidth = texture.width, totalHeight = texture.height, singleWidth = (int)size.x, singleHeight = (int)size.y, count = 0;
        output = new Sprite[totalWidth * totalHeight / singleWidth / singleHeight];

        for (int y = totalHeight - singleHeight; y >= 0; y -= singleHeight)
        {
            for (int x = 0; x < totalWidth; x += singleWidth)
            {
                var rect = new Rect(x, y, singleWidth, singleHeight);
                Sprite sprite = Sprite.Create(texture, rect, options.Pivot, options.PixelsPerUnit, 0, options.MeshType, options.Border);
                RegisterSpriteOnObject(sprite);
                output[count++] = sprite;
            }
        }

        return true;
    }

    /// <summary>
    /// Loads the data as a Sprite[] (Default options), if it exists
    /// </summary>
    public bool LoadDataAsFixedSpritesheet(string fileName, Vector2 size, out Sprite[] output) =>
        LoadDataAsFixedSpritesheet(fileName, size, out output, new SpriteImportOptions());

    /// <summary>
    /// Whenever a sprite is created, it gets dereferenced in the next scene, so add it to a persistent object to keep it around in memory
    /// </summary>
    private void RegisterSpriteOnObject(Sprite sprite)
    {
        var obj = new GameObject(sprite?.name ?? "Empty");
        obj.transform.SetParent(ObjectHelper.ModObject.transform);

        var sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.enabled = false;
    }

    // Config

    ///// <summary>
    ///// Loads the contents of the config file, or an empty string
    ///// </summary>
    //internal string LoadConfig()
    //{
    //    return ReadFileContents(configPath, out string output) ? output : string.Empty;
    //}

    ///// <summary>
    ///// Saves the contents of the config file
    ///// </summary>
    //internal void SaveConfig(string config)
    //{
    //    EnsureDirectoryExists(configPath);
    //    File.WriteAllText(configPath, config);
    //}

    // Keybindings

    /// <summary>
    /// Loads the contents of the keybindings file, or an empty list
    /// </summary>
    internal string[] LoadKeybindings()
    {
        return ReadFileLines(keybindingsPath, out string[] output) ? output : [];
    }

    /// <summary>
    /// Saves the contents of the keybindings file
    /// </summary>
    internal void SaveKeybindings(string[] keys)
    {
        EnsureDirectoryExists(keybindingsPath);
        File.WriteAllLines(keybindingsPath, keys);
    }

    // Localization

    /// <summary>
    /// Loads the contents of the localization file, or an empty list
    /// </summary>
    internal string[] LoadLocalization()
    {
        return ReadFileLines(localizationPath, out string[] output) ? output : [];
    }
}