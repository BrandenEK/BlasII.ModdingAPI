using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace BlasII.ModdingAPI.Files
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
            rootPath = $"{Directory.GetCurrentDirectory()}/";
            configPath = Path.GetFullPath($"Modding/config/{mod.Name}.cfg");
            dataPath = Path.GetFullPath($"Modding/data/{mod.Name}/");
            keybindingsPath = Path.GetFullPath($"Modding/keybindings/{mod.Name}.txt");
            levelsPath = Path.GetFullPath($"Modding/levels/{mod.Name}/");
            localizationPath = Path.GetFullPath($"Modding/localization/{mod.Name}.txt");
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

        private void EnsureDirectoryExists(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        // Config

        public void SaveConfig<T>(T config)
        {
            EnsureDirectoryExists(configPath);            
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

        public bool LoadDataAsSprite(string fileName, out Sprite output, int pixelsPerUnit, bool usePointFilter, Vector2 pivot, Vector4 border)
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

            output = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, pixelsPerUnit, 0, SpriteMeshType.Tight, border);
            RegisterSpriteOnObject(output);

            return true;
        }

        public bool LoadDataAsSprite(string fileName, out Sprite output)
        {
            return LoadDataAsSprite(fileName, out output, 32, true, new Vector2(0.5f, 0.5f), Vector4.zero);
        }

        public bool LoadDataAsSprite(string fileName, out Sprite output, int pixelsPerUnit, bool usePointFilter)
        {
            return LoadDataAsSprite(fileName, out output, pixelsPerUnit, usePointFilter, new Vector2(0.5f, 0.5f), Vector4.zero);
        }

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

        public bool LoadDataAsTexture(string fileName, out Sprite[] output)
        {
            return LoadDataAsTexture(fileName, out output, 30, 32, true, new Vector2(0.5f, 0.5f), Vector4.zero);
        }

        public bool LoadDataAsTexture(string fileName, out Sprite[] output, int size, int pixelsPerUnit, bool usePointFilter)
        {
            return LoadDataAsTexture(fileName, out output, size, pixelsPerUnit, usePointFilter, new Vector2(0.5f, 0.5f), Vector4.zero);
        }

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

        // Keybindings

        internal string[] LoadKeybindings()
        {
            return ReadFileLines(keybindingsPath, out string[] output) ? output : System.Array.Empty<string>();
        }

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

        internal string[] LoadLocalization()
        {
            return ReadFileLines(localizationPath, out string[] output) ? output : System.Array.Empty<string>();
        }
    }
}