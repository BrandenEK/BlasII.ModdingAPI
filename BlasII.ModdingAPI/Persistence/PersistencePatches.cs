using HarmonyLib;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers;
using Il2CppTGK.Persistence;
using System.Reflection;

namespace BlasII.ModdingAPI.Persistence;

[HarmonyPatch]
class Mod_Save1_Patch
{
    public static MethodInfo TargetMethod()
    {
        return AccessTools.Method(typeof(GuiltManager), nameof(GuiltManager.BuildCurrentPersistentState), System.Array.Empty<System.Type>());
    }

    public static void Postfix() => SaveData.SaveGame(CoreCache.SaveData.CurrentSaveSlot);
}

[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.BuildCurrentPersistentState), typeof(PersistentData))]
class Mod_Save2_Patch
{
    public static void Postfix() => SaveData.SaveGame(CoreCache.SaveData.CurrentSaveSlot);
}

[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.SetCurrentPersistentState))]
class Mod_Load_Patch
{
    public static void Postfix() => SaveData.LoadGame(CoreCache.SaveData.CurrentSaveSlot);
}

[HarmonyPatch(typeof(GuiltManager), nameof(GuiltManager.ResetPersistence))]
class Mod_Reset_Patch
{
    public static void Postfix() => SaveData.ResetGame();
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.DeleteSlot))]
class Mod_Delete_Patch
{
    public static void Postfix(int slot) => SaveData.DeleteDataFromFile(slot);
}
