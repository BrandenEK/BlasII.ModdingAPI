using HarmonyLib;
using Il2CppTGK.Framework;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.Game.Managers;

namespace BlasII.ModdingAPI;

[HarmonyPatch(typeof(Core), nameof(Core.OnDestroy))]
class Core_OnDestroy_Patch
{
    public static void Prefix() => Main.ModLoader.Dispose();

    //public static void Postfix() => ModLog.Warn("Core OnDestroy Post");
}

// Spams errors in console
//[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.OnInitialize))]
//class tx
//{
//    public static void Postfix()
//    {
//        ModLog.Error("Manager OnInitialize");
//    }
//}

[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.OnAllInitialized))]
class ty
{
    public static void Postfix() => ModLog.Error("Manager OnAllInitialized");
}
[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.OnDispose))]
class tz
{
    public static void Postfix() => ModLog.Error("Manager OnDispose");
}

[HarmonyPatch(typeof(Core), nameof(Core.Awake))]
class ta
{
    //public static void Prefix() => ModLog.Info("Core Awake Pre");

    public static void Postfix() => Main.ModLoader.PreInitialize();
}
[HarmonyPatch(typeof(Core), nameof(Core.CreateManagers))]
class tb
{
    //public static void Prefix() => ModLog.Info("Core CreateManagers Pre");

    public static void Postfix()
    {
        Main.ModLoader.Initialize();
        Main.ModLoader.PostInitialize();
    }
}
[HarmonyPatch(typeof(Core), nameof(Core.Initialize))]
class tc
{
    //public static void Prefix() => ModLog.Info("Core Initialize Pre");

    //public static void Postfix() => ModLog.Info("Core Initialize Post");
}
[HarmonyPatch(typeof(Core), nameof(Core.InitializeCoroutine))]
class td
{
    //public static void Prefix() => ModLog.Info("Core InitializeCoroutine Pre");

    //public static void Postfix() => ModLog.Info("Core InitializeCoroutine Post");
}



[HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnAllInitialized))]
class Mod_AllInitialized_Patch
{
    //public static void Postfix() => Main.ModLoader.Initialize();
}

//[HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnDispose))]
//class Mod_Dispose_Patch
//{
//    public static void Postfix() => Main.ModLoader.Dispose();
//}

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