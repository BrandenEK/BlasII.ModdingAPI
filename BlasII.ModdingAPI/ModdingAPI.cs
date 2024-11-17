using BlasII.ModdingAPI.Assets;
using BlasII.ModdingAPI.Extensions;
using BlasII.ModdingAPI.Files;
using BlasII.ModdingAPI.Helpers;
using BlasII.ModdingAPI.Input;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using Il2CppTMPro;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI;

internal class ModdingAPI : BlasIIMod
{
    public ModdingAPI() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    protected internal override void OnInitialize()
    {
        AssetStorage.Initialize();
        InputStorage.Initialize();

        ImportAll();
    }

    protected internal override void OnSceneLoaded(string sceneName)
    {
        if (SceneHelper.MenuSceneLoaded)
        {
            DisplayModListOnMenu();

            if (VersionHelper.GameVersion == "Unknown")
                FindGameVersion();
        }
    }

    private readonly Dictionary<string, Sprite> _spriteImports = new();
    private readonly Dictionary<string, Sprite> _spriteExports = new();

    private readonly List<int> _states = new List<int>()
    {
        -965558939, // Idle
        -1574991317, // Talking start
        521498251, // Talking
        -1969186395, // Running start
        1160887219, // Running
        1884254511, // Running stop
    };

    protected internal override void OnUpdate()
    {
        if (!SceneHelper.GameSceneLoaded)
            return;

        if (!_isPlaying)
            return;

        var sr = CoreCache.PlayerSpawn.PlayerInstance.GetComponentsInChildren<SpriteRenderer>().First(x => x.name == "armor");
        var sprite = sr.sprite;

        if (sprite == null || sprite.name == "2x2_transparent")
            return;

        // When finding new sprite, add it to export
        if (!_spriteExports.ContainsKey(sprite.name))
        {
            ModLog.Info("New sprite: " + sprite.name);
            _spriteExports.Add(sprite.name, sprite);
        }
    }

    protected internal override void OnLateUpdate()
    {
        if (!SceneHelper.GameSceneLoaded)
            return;

        var sr = CoreCache.PlayerSpawn.PlayerInstance.GetComponentsInChildren<SpriteRenderer>().First(x => x.name == "armor");
        var sprite = sr.sprite;

        if (sprite == null || string.IsNullOrEmpty(sprite.name) || sprite.name == "2x2_transparent")
            return;

        // When playing, force the anim
        if (_isPlaying)
        {
            Play();
        }

        // When finding new sprite, add it to export
        //if (!_spriteExports.ContainsKey(sprite.name))
        //{
        //    ModLog.Info("New sprite: " + sprite.name);
        //    _spriteExports.Add(sprite.name, sprite);
        //}

        // When on an imported sprite, replace it
        if (_spriteImports.TryGetValue(sprite.name, out Sprite output))
        {
            //ModLog.Error("Replacing sprite: " + sprite.name);
            sr.sprite = output;
        }
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad1))
        {
            ExportAll();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad2))
        {
            var anim = sr.GetComponent<Animator>();
            ModLog.Error("Current anim state: " + anim.GetCurrentAnimatorStateInfo(0).nameHash);
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad3))
        {
            ModLog.Warn("Starting load...");

            InputHandler.InputBlocked = true;
            _isPlaying = true;
        }
    }

    private bool _isPlaying = false;
    private int _currentState = 0;
    private float _currentTime = 0f;

    private const float ANIM_STEP = 0.02f;

    private void Play()
    {
        var anim = CoreCache.PlayerSpawn.PlayerInstance.GetComponentsInChildren<Animator>().First(x => x.name == "armor");
        anim.Play(_states[_currentState], 0, _currentTime);

        _currentTime += ANIM_STEP;
        if (_currentTime >= 1)
        {
            _currentTime = 0f;
            _currentState++;
            if (_currentState >= _states.Count)
            {
                _currentState = 0;
                InputHandler.InputBlocked = false;
                _isPlaying = false;
            }
        }
    }

    private void ImportAll()
    {
        ModLog.Warn("Starting Import...");

        string dir = Path.Combine(FileHandler.ModdingFolder, "skins");
        foreach (var file in Directory.GetFiles(dir, "*.json", SearchOption.TopDirectoryOnly))
        {
            Import(Path.GetFileNameWithoutExtension(file));
        }
    }

    private void Import(string animation)
    {
        // Import info list from skins folder
        var json = File.ReadAllText(Path.Combine(FileHandler.ModdingFolder, "skins", $"{animation}.json"));
        var infos = JsonConvert.DeserializeObject<SpriteInfo[]>(json);

        // Import texture from skins folder
        var bytes = File.ReadAllBytes(Path.Combine(FileHandler.ModdingFolder, "skins", $"{animation}.png"));
        var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes, false);
        texture.filterMode = FilterMode.Point;

        // Load each sprite from the texture based on its info
        foreach (var info in infos)
        {
            ModLog.Info($"Importing {info.Name}");

            var rect = new Rect(info.Position, 0, info.Width, info.Height);
            var sprite = Sprite.Create(texture, rect, new Vector2(info.Pivot, 0), info.PixelsPerUnit);
            sprite.hideFlags = HideFlags.DontUnloadUnusedAsset;

            _spriteImports.Add(info.Name, sprite);
        }
    }

    private void ExportAll()
    {
        ModLog.Warn("Starting Export...");

        var names = _spriteExports.Keys.Select(x => x[0..x.LastIndexOf('_')]).Distinct();
        foreach (var name in names)
        {
            var sprites = _spriteExports
                .Where(x => x.Key.StartsWith(name))
                .OrderBy(x => int.Parse(x.Key[(x.Key.LastIndexOf('_') + 1)..]))
                .Select(x => x.Value);

            Export(name, sprites);
        }
    }

    private void Export(string animation, IEnumerable<Sprite> sprites)
    {
        // Create entire animation texture
        int width = (int)sprites.Sum(x => x.rect.width);
        int height = (int)sprites.Max(x => x.rect.height);
        Texture2D tex = new Texture2D(width, height);

        // Fill transparent
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                tex.SetPixel(i, j, new Color32(0, 0, 0, 0));

        // Create empty info list
        SpriteInfo[] infos = new SpriteInfo[sprites.Count()];

        // Copy each sprite to the texture and save info
        int x = 0, idx = 0;
        foreach (var sprite in sprites)
        {
            ModLog.Info($"Exporting {sprite.name}");
            int w = (int)sprite.rect.width;
            int h = (int)sprite.rect.height;
            Graphics.CopyTexture(sprite.GetSlicedTexture(), 0, 0, 0, 0, w, h, tex, 0, 0, x, 0);

            infos[idx] = new SpriteInfo()
            {
                Name = sprite.name,
                PixelsPerUnit = (int)sprite.pixelsPerUnit,
                Position = x,
                Width = w,
                Height = h,
                Pivot = sprite.pivot.x / w,
            };

            x += w;
            idx++;
        }

        // Save texture to file
        string texturePath = Path.Combine(FileHandler.ContentFolder, $"{animation}.png");
        File.WriteAllBytes(texturePath, tex.EncodeToPNG());

        // Save info list to file
        string infoPath = Path.Combine(FileHandler.ContentFolder, $"{animation}.json");
        File.WriteAllText(infoPath, JsonConvert.SerializeObject(infos, Formatting.Indented));
    }

    class SpriteInfo
    {
        public string Name { get; init; }
        public int PixelsPerUnit { get; init; }
        public int Position { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public float Pivot { get; init; }
    }

    private void DisplayModListOnMenu()
    {
        // Find parent and font
        Transform parent = Object.FindObjectOfType<CanvasScaler>()?.transform.Find("Interfaces/MainMenuWindow_prefab(Clone)").GetChild(0);
        TMP_FontAsset font = Object.FindObjectOfType<TextMeshProUGUI>()?.font;
        if (parent == null || font == null)
            return;

        // Create text for mod list
        StringBuilder fullText = new();
        StringBuilder shadowText = new();
        foreach (var mod in ModHelper.LoadedMods.OrderBy(GetModPriority).ThenBy(x => x.Name))
        {
            fullText.AppendLine(GetModText(mod, true));
            shadowText.AppendLine(GetModText(mod, false));
        }

        // Create rect transform for shadow
        RectTransform r = new GameObject().AddComponent<RectTransform>();
        r.name = "Mod list";
        r.SetParent(parent, false);
        r.anchorMin = Vector2.zero;
        r.anchorMax = Vector2.one;
        r.pivot = new Vector2(0, 1);
        r.anchoredPosition = new Vector2(20, -15);
        r.sizeDelta = new Vector2(400, 100);

        // Create text for shadow
        TextMeshProUGUI t = r.gameObject.AddComponent<TextMeshProUGUI>();
        t.alignment = TextAlignmentOptions.TopLeft;
        t.color = Color.black;
        t.enableWordWrapping = false;
        t.font = font;
        t.fontSize = 32;
        t.text = shadowText.ToString();

        // Duplicate shadow for real text
        GameObject real = Object.Instantiate(r.gameObject, r.transform);
        TextMeshProUGUI st = real.GetComponent<TextMeshProUGUI>();
        st.rectTransform.anchoredPosition = new Vector2(-1, 2);
        st.richText = true;
        st.text = fullText.ToString();
    }

    private void FindGameVersion()
    {
        // Store game version
        var versionObject = Object.FindObjectOfType<SetGameVersionText>();
        string versionText = versionObject.GetComponentInChildren<TextMeshProUGUI>().text;

        int spaceIdx = versionText.IndexOf(' ');
        if (spaceIdx >= 0)
            versionText = versionText[..spaceIdx];

        int dashIndex = versionText.IndexOf("-");
        if (dashIndex >= 0)
            versionText = versionText[..dashIndex];

        VersionHelper.GameVersion = versionText;
        ModLog.Info($"Detected game version: " + versionText);
    }

    private int GetModPriority(BlasIIMod mod)
    {
        if (mod == this)
            return -1;

        if (mod.Name.EndsWith("Framework"))
            return 0;

        return 1;
    }

    private string GetModText(BlasIIMod mod, bool addColor)
    {
        string line = $"{mod.Name} v{mod.Version}";

        if (!addColor)
            return line;

        string color = mod == this || mod.Name.EndsWith("Framework") ? "7CA7BF" : "D3D3D3";
        return $"<color=#{color}>{line}</color>";
    }
}
