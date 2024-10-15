using BlasII.ModdingAPI.Assets;
using BlasII.ModdingAPI.Files;
using BlasII.ModdingAPI.Helpers;
using BlasII.ModdingAPI.Input;
using BlasII.ModdingAPI.UI;
using Il2CppTGK.Game.Components.UI;
using Il2CppTMPro;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BlasII.ModdingAPI;

internal class ModdingAPI : BlasIIMod
{
    public ModdingAPI() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private bool _initializedUI = false;

    private Sprite _cursorIcon;
    public Sprite CursorIcon => _cursorIcon;

    protected internal override void OnInitialize()
    {
        LocalizationHandler.RegisterDefaultLanguage("en");
        FileHandler.LoadDataAsSprite("cursor.png", out _cursorIcon, new SpriteImportOptions()
        {
            PixelsPerUnit = 40
        });

        AssetStorage.Initialize();
        InputStorage.Initialize();
    }

    protected internal override void OnSceneLoaded(string sceneName)
    {
        if (SceneHelper.MenuSceneLoaded)
        {
            if (!_initializedUI)
            {
                UIModder.Fonts.Initialize();
                UIModder.Parents.Initialize();
                _initializedUI = true;
                FindGameVersion();
            }

            DisplayModListOnMenu();
        }
    }

    private void DisplayModListOnMenu()
    {
        // Create text for mod list
        StringBuilder fullText = new();
        StringBuilder shadowText = new();
        foreach (var mod in ModHelper.LoadedMods.OrderBy(GetModPriority).ThenBy(x => x.Name))
        {
            fullText.AppendLine(GetModText(mod, true));
            shadowText.AppendLine(GetModText(mod, false));
        }

        // Create shadow text
        var obj = UIModder.Create(new RectCreationOptions()
        {
            Name = "ModList",
            Parent = UIModder.Parents.MainMenu.GetChild(0),
            XRange = new Vector2(0, 1),
            YRange = new Vector2(0, 1),
            Pivot = new Vector2(0, 1),
            Position = new Vector2(20, -15),
            Size = new Vector2(400, 100)
        }).AddText(new TextCreationOptions()
        {
            Contents = shadowText.ToString(),
            Alignment = TextAlignmentOptions.TopLeft,
            FontSize = 32,
            Color = Color.black
        });

        // Duplicate shadow for real text
        GameObject real = Object.Instantiate(obj.gameObject, obj.transform);
        TextMeshProUGUI st = real.GetComponent<TextMeshProUGUI>();
        st.richText = true;
        st.text = fullText.ToString();
        st.rectTransform.anchoredPosition = new Vector2(-1, 2);
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
