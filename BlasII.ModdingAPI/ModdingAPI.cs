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

        FileHandler.LoadDataAsJson("sprites.json", out SpriteInfo[] infos);
        foreach (var info in infos)
        {
            ModLog.Warn("Loading " + info.Name);
            bool result = FileHandler.LoadDataAsSprite($"{info.Name}.png", out Sprite s, new SpriteImportOptions()
            {
                PixelsPerUnit = (int)info.PixelsPerUnit,
                Pivot = new Vector2(info.XPivot, info.YPivot),
            });

            ModLog.Info("REsult: " + result);
            _sprites.Add(info.Name, s);
        }

        //foreach (string path in Directory.GetFiles(Path.Combine(FileHandler.ModdingFolder, "data", "Modding API")))
        //{
        //    string file = Path.GetFileName(path);
        //    ModLog.Warn("Loaded sprite: " + file);
        //    FileHandler.LoadDataAsSprite(file, out Sprite s, new Files.SpriteImportOptions()
        //    {
        //        Pivot = new Vector2(0.5f, 0)
        //    });
        //    _sprites.Add(Path.GetFileNameWithoutExtension(path), s);
        //}
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

    private Dictionary<string, SpriteInfo> _spriteInfos = new();
    private Dictionary<string, Sprite> _sprites = new();

    protected internal override void OnLateUpdate()
    {
        if (!SceneHelper.GameSceneLoaded)
            return;

        var sr = CoreCache.PlayerSpawn.PlayerInstance.GetComponentsInChildren<SpriteRenderer>().First(x => x.name == "armor");
        var sprite = sr.sprite;

        if (sprite == null)
            return;

        if (sprite.name.StartsWith("TPO_idle_wieldingLightWeapon") && !_spriteInfos.ContainsKey(sprite.name))
        {
            ModLog.Info("New sprite: " + sprite.name);
            _spriteInfos.Add(sprite.name, new SpriteInfo()
            {
                Name = sprite.name,
                PixelsPerUnit = sprite.pixelsPerUnit,
                XPivot = sprite.pivot.x / sprite.rect.width,
                YPivot = sprite.pivot.y / sprite.rect.height,
                Sprite = sprite,
                //Size = new V2()
                //{
                //    X = sprite.rect.width,
                //    Y = sprite.rect.height
                //},
                //Pivot = new V2()
                //{
                //    X = sprite.pivot.x,
                //    Y = sprite.pivot.y
                //}
            });
        }

        if (_sprites.ContainsKey(sprite.name))
        {
            ModLog.Error("Replacing sprite: " + sprite.name);
            sr.sprite = _sprites[sprite.name];
        }
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad1))
        {
            Export();
        }
    }

    private void Export()
    {
        ModLog.Warn("Exporting");
        var sprites = _spriteInfos.Values.OrderBy(x => x.Name).Select(x => x.Sprite);

        // Create entire animation texture
        int width = (int)sprites.Sum(x => x.rect.width);
        int height = (int)sprites.Max(x => x.rect.height);
        Texture2D tex = new Texture2D(width, height);

        // Fill transparent
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                tex.SetPixel(i, j, new Color32(0, 0, 0, 0));

        // Create empty info list
        SpriteExportInfo[] infos = new SpriteExportInfo[sprites.Count()];

        // Copy each sprite to the texture and save info
        int x = 0, idx = 0;
        foreach (var sprite in sprites)
        {
            int w = (int)sprite.rect.width;
            int h = (int)sprite.rect.height;
            Graphics.CopyTexture(sprite.GetSlicedTexture(), 0, 0, 0, 0, w, h, tex, 0, 0, x, 0);

            infos[idx] = new SpriteExportInfo()
            {
                PixelsPerUnit = sprite.pixelsPerUnit,
                Position = x,
                Width = w,
                Height = h,
                Pivot = sprite.pivot.x / w,
            };

            x += w;
            idx++;
        }

        // Save texture to file
        string texturePath = Path.Combine(FileHandler.ContentFolder, "TPO_idle_wieldingLightWeapon.png");
        File.WriteAllBytes(texturePath, tex.EncodeToPNG());

        // Save info list to file
        string infoPath = Path.Combine(FileHandler.ContentFolder, "TPO_idle_wieldingLightWeapon.json");
        File.WriteAllText(infoPath, JsonConvert.SerializeObject(infos, Formatting.Indented));

        //foreach (var s in sprites.Select(x => x.Sprite))
        //{
        //    string p = Path.Combine(FileHandler.ContentFolder, $"{s.name}.png");
        //    File.WriteAllBytes(p, s.GetSlicedTexture().EncodeToPNG());
        //}

        //string path = Path.Combine(FileHandler.ContentFolder, "sprites.json");
        //File.WriteAllText(path, JsonConvert.SerializeObject(_spriteInfos.Values, Formatting.Indented));
    }

    class SpriteExportInfo
    {
        public float PixelsPerUnit { get; init; }
        public int Position { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public float Pivot { get; init; }
    }

    class SpriteInfo
    {
        public string Name { get; init; }
        public float PixelsPerUnit { get; init; }
        public float XPivot { get; init; }
        public float YPivot { get; init; }

        [JsonIgnore]
        public Sprite Sprite { get; init; }
    }

    //class SpriteInfo
    //{
    //    public string Name { get; init; }
    //    public float PixelsPerUnit { get; init; }
    //    public V2 Size { get; init; }
    //    public V2 Pivot { get; init; }
    //}

    class V2
    {
        public float X { get; set; }
        public float Y { get; set; }
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
