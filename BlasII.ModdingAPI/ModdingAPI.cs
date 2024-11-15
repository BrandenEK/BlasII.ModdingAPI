﻿using BlasII.ModdingAPI.Assets;
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

        Import();
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

    protected internal override void OnLateUpdate()
    {
        if (!SceneHelper.GameSceneLoaded)
            return;

        var sr = CoreCache.PlayerSpawn.PlayerInstance.GetComponentsInChildren<SpriteRenderer>().First(x => x.name == "armor");
        var sprite = sr.sprite;

        if (sprite == null)
            return;

        // When finding new sprite, add it to export
        if (sprite.name.StartsWith("TPO_idle_wieldingLightWeapon") && !_spriteExports.ContainsKey(sprite.name))
        {
            ModLog.Info("New sprite: " + sprite.name);
            _spriteExports.Add(sprite.name, sprite);
        }

        // When on an imported sprite, replace it
        if (_spriteImports.TryGetValue(sprite.name, out Sprite output))
        {
            ModLog.Error("Replacing sprite: " + sprite.name);
            sr.sprite = output;
        }
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad1))
        {
            Export();
        }
    }

    private void Import()
    {
        ModLog.Warn("Importing");

        // Load info list from file
        FileHandler.LoadDataAsJson("TPO_idle_wieldingLightWeapon.json", out SpriteInfo[] infos);

        // Load each sprite from the texture based on its info
        int idx = 0;
        foreach (var info in infos)
        {
            Rect[] rects = [ new Rect(info.Position, 0, info.Width, info.Height) ];

            FileHandler.LoadDataAsVariableSpritesheet("TPO_idle_wieldingLightWeapon.png", rects, out Sprite[] output, new SpriteImportOptions()
            {
                PixelsPerUnit = info.PixelsPerUnit,
                Pivot = new Vector2(info.Pivot, 0),
            });

            string name = $"TPO_idle_wieldingLightWeapon_{idx++}";
            _spriteImports.Add(name, output[0]);
        }
    }

    private void Export()
    {
        ModLog.Warn("Exporting");
        var sprites = _spriteExports.Values.OrderBy(x => x.name);

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
            int w = (int)sprite.rect.width;
            int h = (int)sprite.rect.height;
            Graphics.CopyTexture(sprite.GetSlicedTexture(), 0, 0, 0, 0, w, h, tex, 0, 0, x, 0);

            infos[idx] = new SpriteInfo()
            {
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
        string texturePath = Path.Combine(FileHandler.ContentFolder, "TPO_idle_wieldingLightWeapon.png");
        File.WriteAllBytes(texturePath, tex.EncodeToPNG());

        // Save info list to file
        string infoPath = Path.Combine(FileHandler.ContentFolder, "TPO_idle_wieldingLightWeapon.json");
        File.WriteAllText(infoPath, JsonConvert.SerializeObject(infos, Formatting.Indented));
    }

    class SpriteInfo
    {
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
