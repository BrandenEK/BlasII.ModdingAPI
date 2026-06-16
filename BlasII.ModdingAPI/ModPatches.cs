using HarmonyLib;
using Il2CppTGK.Framework;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.Game.Managers;

namespace BlasII.ModdingAPI;

[HarmonyPatch(typeof(Core), nameof(Core.Awake))]
class Core_Awake_Patch
{
    public static void Postfix() => Main.ModLoader.PreInitialize();
}

[HarmonyPatch(typeof(Core), nameof(Core.CreateManagers))]
class Core_CreateManagers_Patch
{
    public static void Postfix()
    {
        Main.ModLoader.Initialize();
        Main.ModLoader.PostInitialize();
    }
}

[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.OnGlobalRoomLoaded))]
class Mod_SceneLoaded_Patch
{
    public static void Postfix(Room newRoom) => Main.ModLoader.SceneLoaded(newRoom?.Name ?? string.Empty);
}

[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.OnGlobalBeforeChangeRoom))]
class Mod_SceneUnloaded_Patch
{
    public static void Postfix(Room oldRoom) => Main.ModLoader.SceneUnloaded(oldRoom?.Name ?? string.Empty);
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.OnShow))]
class Mod_LoadMenu_Patch
{
    public static void Postfix() => Main.ModLoader.SceneLoaded("MainMenu");
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.NewGame))]
class Mod_NewGame_Patch
{
    public static void Postfix(bool __runOriginal)
    {
        if (__runOriginal)
            Main.ModLoader.ProcessModFunction(mod => mod.OnNewGame());
    }
}

[HarmonyPatch(typeof(MainMenuWindowLogic), nameof(MainMenuWindowLogic.LoadGame))]
class Mod_LoadGame_Patch
{
    public static void Postfix(bool __runOriginal)
    {
        if (__runOriginal)
            Main.ModLoader.ProcessModFunction(mod => mod.OnLoadGame());
    }
}