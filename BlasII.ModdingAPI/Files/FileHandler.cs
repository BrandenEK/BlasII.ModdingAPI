using Il2CppRewired.UI;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using UnityEngine;

namespace BlasII.ModdingAPI.Files
{
    /// <summary>
    /// Provides access to loading data files and other IO methods
    /// </summary>
    public class FileHandler
    {
        private readonly string rootPath;
        private readonly string configPath;
        private readonly string dataPath;
        private readonly string keybindingsPath;
        private readonly string levelsPath; // Might not be needed
        private readonly string localizationPath;

        internal FileHandler(BlasIIMod mod)
        {
            rootPath = $"{Directory.GetCurrentDirectory()}/";
            configPath = Path.GetFullPath($"Modding/config/{mod.Name}.cfg");
            dataPath = Path.GetFullPath($"Modding/data/{mod.Name}/");
            keybindingsPath = Path.GetFullPath($"Modding/keybindings/{mod.Name}.txt");
            levelsPath = Path.GetFullPath($"Modding/levels/{mod.Name}/");
            localizationPath = Path.GetFullPath($"Modding/localization/{mod.Name}.txt");
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
        /// Writes data to a text file in the game's root directory
        /// </summary>
        public void WriteToFile(string fileName, string text)
        {
            File.WriteAllText(rootPath + fileName, text);
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
            return ReadFileContents(dataPath + fileName, out output);
        }

        /// <summary>
        /// Loads the data as a json object, if it exists
        /// </summary>
        public bool LoadDataAsJson<T>(string fileName, out T output)
        {
            if (ReadFileContents(dataPath + fileName, out string text))
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
            return ReadFileLines(dataPath + fileName, out output);
        }

        /// <summary>
        /// Loads the data as a Texture2D, if it exists
        /// </summary>
        public bool LoadDataAsTexture(string fileName, out Texture2D output)
        {
            if (!ReadFileBytes(dataPath + fileName, out byte[] bytes))
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
            if (!ReadFileBytes(dataPath + fileName, out byte[] bytes))
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


        //public bool LoadDataAsSpritesheet(string fileName, out Sprite[] output)
        //{
        //    if (!ReadFileBytes(dataPath + fileName, out byte[] bytes))
        //    {
        //        output = null;
        //        return false;
        //    }

        //    var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        //    texture.LoadImage(bytes, false);

        //    if (usePointFilter)
        //        texture.filterMode = FilterMode.Point;

        //    int totalWidth = texture.width, totalHeight = texture.height, count = 0;
        //    output = new Sprite[totalWidth * totalHeight / size / size];

        //    for (int i = totalHeight - size; i >= 0; i -= size)
        //    {
        //        for (int j = 0; j < totalWidth; j += size)
        //        {
        //            Sprite sprite = Sprite.Create(texture, new Rect(j, i, size, size), pivot, pixelsPerUnit, 0, SpriteMeshType.Tight, border);
        //            RegisterSpriteOnObject(sprite);
        //            output[count] = sprite;
        //            count++;
        //        }
        //    }

        //    return true;
        //}

        /// <summary>
        /// Whenever a sprite is created, it gets dereferenced in the next scene, so add it to a persistent object to keep it around in memory
        /// </summary>
        private void RegisterSpriteOnObject(Sprite sprite)
        {
            var obj = new GameObject(sprite?.name ?? "Empty");
            obj.transform.SetParent(Main.ModLoader.ModObject.transform);

            var sr = obj.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;
            sr.enabled = false;
        }

        // Config

        /// <summary>
        /// Loads the contents of the config file, or an empty list
        /// </summary>
        internal string[] LoadConfig()
        {
            return ReadFileLines(configPath, out string[] output) ? output : System.Array.Empty<string>();
        }

        /// <summary>
        /// Saves the contents of the config file
        /// </summary>
        internal void SaveConfig(string[] properties)
        {
            EnsureDirectoryExists(configPath);
            File.WriteAllLines(configPath, properties);
        }

        // Keybindings

        /// <summary>
        /// Loads the contents of the keybindings file, or an empty list
        /// </summary>
        internal string[] LoadKeybindings()
        {
            return ReadFileLines(keybindingsPath, out string[] output) ? output : System.Array.Empty<string>();
        }

        /// <summary>
        /// Saves the contents of the keybindings file
        /// </summary>
        internal void SaveKeybindings(string[] keys)
        {
            EnsureDirectoryExists(keybindingsPath);
            File.WriteAllLines(keybindingsPath, keys);
        }

        // Levels

        internal void LoadLevels()
        {

        }

        // Localization

        /// <summary>
        /// Loads the contents of the localization file, or an empty list
        /// </summary>
        internal string[] LoadLocalization()
        {
            return ReadFileLines(localizationPath, out string[] output) ? output : System.Array.Empty<string>();
        }

        // Obsolete

        /// <summary>
        /// Loads the data as a Sprite[], if it exists
        /// </summary>
        [Obsolete("Replaced with LoadDataAsSpritesheet")]
        public bool LoadDataAsTexture(string fileName, out Sprite[] output, int size, int pixelsPerUnit, bool usePointFilter, Vector2 pivot, Vector4 border)
        {
            if (!ReadFileBytes(dataPath + fileName, out byte[] bytes))
            {
                output = null;
                return false;
            }

            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            texture.LoadImage(bytes, false);

            if (usePointFilter)
                texture.filterMode = FilterMode.Point;

            int totalWidth = texture.width, totalHeight = texture.height, count = 0;
            output = new Sprite[totalWidth * totalHeight / size / size];

            for (int i = totalHeight - size; i >= 0; i -= size)
            {
                for (int j = 0; j < totalWidth; j += size)
                {
                    Sprite sprite = Sprite.Create(texture, new Rect(j, i, size, size), pivot, pixelsPerUnit, 0, SpriteMeshType.Tight, border);
                    RegisterSpriteOnObject(sprite);
                    output[count] = sprite;
                    count++;
                }
            }

            return true;
        }

        /// <summary>
        /// Loads the data as a Sprite[], if it exists
        /// </summary>
        [Obsolete("Replaced with LoadDataAsSpritesheet")]
        public bool LoadDataAsTexture(string fileName, out Sprite[] output)
        {
            return LoadDataAsTexture(fileName, out output, 30, 32, true, new Vector2(0.5f, 0.5f), Vector4.zero);
        }

        /// <summary>
        /// Loads the data as a Sprite[], if it exists
        /// </summary>
        [Obsolete("Replaced with LoadDataAsSpritesheet")]
        public bool LoadDataAsTexture(string fileName, out Sprite[] output, int size, int pixelsPerUnit, bool usePointFilter)
        {
            return LoadDataAsTexture(fileName, out output, size, pixelsPerUnit, usePointFilter, new Vector2(0.5f, 0.5f), Vector4.zero);
        }

        /// <summary>
        /// Loads the data as a Sprite, if it exists
        /// </summary>
        [Obsolete("Pass in new SpriteImportOptions instead")]
        public bool LoadDataAsSprite(string fileName, out Sprite output, int pixelsPerUnit, bool usePointFilter, Vector2 pivot, Vector4 border)
        {
            var options = new SpriteImportOptions()
            {
                PixelsPerUnit = pixelsPerUnit,
                UsePointFilter = usePointFilter,
                Pivot = pivot,
                Border = border
            };
            return LoadDataAsSprite(fileName, options, out output);
        }

        /// <summary>
        /// Loads the data as a Sprite, if it exists
        /// </summary>
        [Obsolete("Pass in new SpriteImportOptions instead")]
        public bool LoadDataAsSprite(string fileName, out Sprite output, int pixelsPerUnit, bool usePointFilter)
        {
            var options = new SpriteImportOptions()
            {
                PixelsPerUnit = pixelsPerUnit,
                UsePointFilter = usePointFilter
            };
            return LoadDataAsSprite(fileName, options, out output);
        }
    }
}