using BlasII.ModdingAPI.Persistence;
using HarmonyLib;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.Game.Managers;
using Il2CppTGK.Persistence;

namespace BlasII.ModdingAPI
{
    // Events

    [HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnInitialize))]
    class Mod_Initialize_Patch
    {
        public static void Postfix() => Main.ModLoader.Initialize();
    }

    [HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnAllInitialized))]
    class Mod_AllInitialized_Patch
    {
        public static void Postfix() => Main.ModLoader.AllInitialized();
    }

    [HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnDispose))]
    class Mod_Dispose_Patch
    {
        public static void Postfix() => Main.ModLoader.Dispose();
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
    class Mod_New_Patch
    {
        public static void Postfix() => Main.ModLoader.NewGame();
    }

    // Persistence

    [HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.BuildCurrentPersistentState), typeof(PersistentData))]
    class Mod_Save_Patch
    {
        public static void Postfix() => Main.ModLoader.SaveGame(CoreCache.SaveData.CurrentSaveSlot);
    }

    [HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.SetCurrentPersistentState))]
    class Mod_Load_Patch
    {
        public static void Postfix() => Main.ModLoader.LoadGame(CoreCache.SaveData.CurrentSaveSlot);
    }

    [HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.ResetPersistence))]
    class Mod_Reset_Patch
    {
        public static void Postfix() => Main.ModLoader.ResetGame();
    }

    [HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.DeleteSlot))]
    class Mod_Delete_Patch
    {
        public static void Postfix(int slot) => SaveData.DeleteDataFromFile(slot);
    }
}
