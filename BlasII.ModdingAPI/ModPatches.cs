using HarmonyLib;
using Il2Cpp;
using Il2CppTeam17.Platform.UserService;
using Il2CppTGK.Game.Components.UI;
using Il2CppTGK.Game.Managers;
using UnityEngine;

using System.Text;

namespace BlasII.ModdingAPI;

[HarmonyPatch(typeof(AchievementsManager), nameof(AchievementsManager.OnAllInitialized))]
class Mod_AllInitialized_Patch
{
    public static void Postfix() => Main.ModLoader.Initialize();
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

//[HarmonyPatch(typeof(DLCManager), nameof(DLCManager.IsOwned))]
//class DLCManager_IsOwned_Patch1
//{
//    [HarmonyPriority(Priority.First)]
//    public static void Postfix(bool __result, out bool __state)
//    {
//        __state = __result;
//    }
//}
//[HarmonyPatch(typeof(DLCManager), nameof(DLCManager.IsOwned))]
//class DLCManager_IsOwned_Patch2
//{
//    [HarmonyPriority(Priority.Last)]
//    public static void Postfix(ref bool __result, bool __state)
//    {
//        if (__result == __state)
//            return;

//        ModLog.Info("Updating language for all mods");
//        UnityEngine.Application.Quit();
//    }
//}

//[HarmonyPatch(typeof(ChallengesMenuLogic), nameof(ChallengesMenuLogic.OnCreateChallengeData))]
//class t1
//{
//    public static void Postfix(ChallengesMenuLogic __instance, ListData data)
//    {
//        ModLog.Warn("ChallengesMenuLogic.OnCreateChallengeData " + data.row);

//        if (data.row < 8)
//            return;

//        var selectable = data.obj;
//        //ModLog.Info(selectable.transform.DisplayHierarchy(5, true));

//        var challenge = selectable.GetComponent<UISelectableChallenge>();
//        challenge.caption.SetText("Communion of Blood");
//        challenge.icon.sprite = null;
//    }
//}

//[HarmonyPatch(typeof(ChallengesMenuLogic), nameof(ChallengesMenuLogic.OnElementSelected))]
//class t2
//{
//    public static void Postfix(ChallengesMenuLogic __instance, ListData data)
//    {
//        ModLog.Warn("ChallengesMenuLogic.OnElementSelected " + data.row);
//        ModLog.Info(__instance.descriptionText.textControl.normalText.text);

//        if (data.row < 8)
//            return;

//        var selectable = data.obj;
//        //ModLog.Info(selectable.transform.DisplayHierarchy(5, true));


//        __instance.descriptionText.SetText("Enemies <color=#ff9700>restore health</color> after hitting the Penitent One. The Penitent One regains a small amount of health after killing an enemy, with a slightly greater gain on an execution. The Penitent One’s critical hits are also <color=#ff9700>slightly enhanced</color>.");
//    }
//}

//internal static class InfoExtensions
//{
//    // Recursive method that returns the entire hierarchy of an object
//    public static string DisplayHierarchy(this Transform transform, int maxLevel, bool includeComponents)
//    {
//        return transform.DisplayHierarchy_INTERNAL(new StringBuilder(), 0, maxLevel, includeComponents).ToString();
//    }

//    private static StringBuilder DisplayHierarchy_INTERNAL(this Transform transform, StringBuilder currentHierarchy, int currentLevel, int maxLevel, bool includeComponents)
//    {
//        // Indent
//        for (int i = 0; i < currentLevel; i++)
//            currentHierarchy.Append('\t');

//        // Add this object
//        currentHierarchy.Append(transform.name);

//        // Add components
//        if (includeComponents)
//        {
//            currentHierarchy.Append(" - ");
//            foreach (Component c in transform.GetComponents<Component>())
//                currentHierarchy.Append(c.GetIl2CppType().FullName + ", ");
//        }
//        currentHierarchy.AppendLine();

//        // Add children
//        if (currentLevel < maxLevel)
//        {
//            for (int i = 0; i < transform.childCount; i++)
//                currentHierarchy = transform.GetChild(i).DisplayHierarchy_INTERNAL(currentHierarchy, currentLevel + 1, maxLevel, includeComponents);
//        }

//        // Return output
//        return currentHierarchy;
//    }

//    // Displays all states and actions of a playmaker fsm
//    public static void DisplayActions(this PlayMakerFSM fsm)
//    {
//        ModLog.Warn("FSM: " + fsm.name);
//        foreach (var state in fsm.FsmStates)
//        {
//            ModLog.Info("State: " + state.Name);
//            foreach (var action in state.Actions)
//            {
//                ModLog.Error("Action: " + action.Name + ", " + action.GetIl2CppType().Name);
//            }
//        }
//    }
//}