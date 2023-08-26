using HarmonyLib;
using Il2CppTGK.Game.Managers;

namespace BlasII.ModdingAPI
{
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
}
