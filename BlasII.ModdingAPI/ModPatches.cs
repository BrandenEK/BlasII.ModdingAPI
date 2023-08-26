using HarmonyLib;
using Il2CppTGK.Game.Managers;
using Il2CppTGK.Game;
using MelonLoader;
using UnityEngine;

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

    //[HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnUpdate))]
    //class Mod_Update_Patch
    //{
    //    public static void Postfix()
    //    {
    //        //if (Time.frameCount % 120 == 0)
    //        //    MelonLogger.Error("Manager update");
    //        //Main.TestMod.OnUpdate();
    //        if (Input.GetKeyDown(KeyCode.P))
    //        {
    //            MelonLogger.Error("Key down");
    //        }
    //    }
    //}
}
