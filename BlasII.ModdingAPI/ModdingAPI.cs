using BlasII.ModdingAPI.Assets;
using BlasII.ModdingAPI.Helpers;
using BlasII.ModdingAPI.Input;
using BlasII.ModdingAPI.Persistence;
using Il2CppTGK.Game.Components.UI;
using Il2CppTMPro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI;

internal class ModdingAPI : BlasIIMod, IGlobalPersistentMod
{
    public System.Type GlobalDataType { get; } = typeof(TestGlobalSaveData);

    public ModdingAPI() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    public void Load(GlobalSaveData data)
    {
        TestGlobalSaveData testData = data as TestGlobalSaveData;

        ModLog.Info("Loaded global: " + testData.Number);
    }

    public GlobalSaveData Save()
    {
        ModLog.Info("Saved global: " + Time.frameCount);
        return new TestGlobalSaveData()
        {
            Number = Time.frameCount
        };
    }

    protected internal override void OnInitialize()
    {
        AssetStorage.Initialize();
        InputStorage.Initialize();
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

    void old()
    {
        var dict = new Dictionary<string, GlobalSaveData>();

        foreach (var mod in ModHelper.LoadedMods)
        {
            ModLog.Error(mod.Id);
            //foreach (var x in mod.GetType().GetInterfaces())
            //{
            //    ModLog.Info(x.Name);
            //    ModLog.Warn(x.GetGenericTypeDefinition());
            //    ModLog.Warn(x.GetGenericArguments()[0].Name);
            //}

            //foreach (var @interface in mod.GetType().GetInterfaces())
            //{
            //    if (!@interface.IsGenericType || !@interface.GetGenericTypeDefinition().IsAssignableFrom(typeof(IGlobalPersistentMod<>)))
            //        continue;

            //    // This mod implements the interface and this is the type of save data
            //    System.Type dataType = @interface.GetGenericArguments()[0];
            //    ModLog.Warn(dataType.Name);


            //}

            //System.Type globalMod = mod.GetType().GetInterfaces()
            //    .Where(i => i.IsGenericType)
            //    .Select(i => i.GetGenericTypeDefinition())
            //    .FirstOrDefault(i => i.IsAssignableFrom(typeof(IGlobalPersistentMod<>)));

            //bool isGlobal = globalMod != null;
            //ModLog.Info("IS gloval: " + isGlobal);

            //if (isGlobal)
            //{
            //    ModLog.Warn("savedata type: " + globalMod.GetGenericArguments()[0].Name);

            //    ModLog.Warn("Global mod: " + mod.Id);
            //    foreach (var x in mod.GetType().GenericTypeArguments)
            //        ModLog.Info(x.Name);
            //}

            //if (mod != this)
            ////if (!mod.GetType().GetInterfaces().Contains(typeof(IGlobalPersistentMod<>)))
            //    continue;

            //typeof(IGlobalPersistentMod<>).MakeGenericType()
            //GlobalSaveData data = ((IGlobalPersistentMod<>)mod).Save();
        }
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
