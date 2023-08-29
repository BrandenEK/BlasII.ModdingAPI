using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace BlasII.ModdingAPI
{
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
            rootPath = $"{Directory.GetCurrentDirectory()}\\"; // Change to get full path
            configPath = Path.GetFullPath($"Modding\\config\\{mod.Name}.cfg");
            dataPath = Path.GetFullPath($"Modding\\data\\{mod.Name}\\");
            keybindingsPath = Path.GetFullPath($"Modding\\keybindings\\{mod.Name}.txt");
            levelsPath = Path.GetFullPath($"Modding\\levels\\{mod.Name}\\");
            localizationPath = Path.GetFullPath($"Modding\\localization\\{mod.Name}.txt");
        }

        // General

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

        public void WriteToFile(string fileName, string text)
        {
            File.WriteAllText(rootPath + fileName, text);
        }

        // Config

        public void SaveConfig<T>(T config)
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
        }

        public T LoadConfig<T>() where T : new()
        {
            if (ReadFileContents(configPath, out string text))
            {
                return JsonConvert.DeserializeObject<T>(text);
            }
            else
            {
                T config = new();
                SaveConfig(config);
                return config;
            }
        }

        // Data

        public bool LoadDataAsText(string fileName, out string output)
        {
            return ReadFileContents(dataPath + fileName, out output);
        }

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

        public bool LoadDataAsArray(string fileName, out string[] output)
        {
            return ReadFileLines(dataPath + fileName, out output);
        }

        public bool LoadDataAsSprite(string fileName, int pixelsPerUnit, Vector2 pivot, bool usePointFilter, out Sprite output)
        {
            if (ReadFileBytes(dataPath + fileName, out byte[] bytes))
            {
                var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                texture.LoadImage(bytes, false);

                if (usePointFilter)
                    texture.filterMode = FilterMode.Point;

                output = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, pixelsPerUnit, 0, SpriteMeshType.Tight, Vector4.zero);
                return true;
            }

            output = null;
            return false;
        }

        // Keybindings

        internal void LoadKeybindings()
        {

        }

        // Levels

        internal void LoadLevels()
        {

        }

        // Localization

        internal void LoadLocalization()
        {

        }
    }
}