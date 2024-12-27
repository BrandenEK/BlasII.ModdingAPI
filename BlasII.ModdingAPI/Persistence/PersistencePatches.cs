using HarmonyLib;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers;
using Il2CppTGK.Persistence;
using System.Reflection;

namespace BlasII.ModdingAPI.Persistence;

//[HarmonyPatch]
//class Mod_Save1_Patch
//{
//    public static MethodInfo TargetMethod()
//    {
//        return AccessTools.Method(typeof(GuiltManager), nameof(GuiltManager.BuildCurrentPersistentState), System.Array.Empty<System.Type>());
//    }

//    public static void Postfix()
//    {
//        ModLog.Info($"Saving game data for slot {CoreCache.SaveData.CurrentSaveSlot}");
//        SaveData.SaveGame(CoreCache.SaveData.CurrentSaveSlot);
//    }
//}
[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.SaveGame), [])]
class Mod_Save2_Patch
{
    public static void Prefix()
    {
        ModLog.Info($"Saving persistent data for slot {CoreCache.SaveData.CurrentSaveSlot}");
        SaveData.SaveGame(CoreCache.SaveData.CurrentSaveSlot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.SaveGame), typeof(int))]
class Mod_Save3_Patch
{
    public static void Prefix(int slot)
    {
        ModLog.Info($"Saving persistent data for slot {slot}");
        SaveData.SaveGame(slot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.LoadGame), typeof(int))]
class Mod_Load_Patch
{
    public static void Prefix(int slot)
    {
        ModLog.Info($"Loading persistent data for slot {slot}");
        SaveData.LoadGame(slot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.ResetPersistence))]
class Mod_Reset_Patch
{
    public static void Prefix()
    {
        ModLog.Info("Resetting persistent data");
        SaveData.ResetGame();
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.DeleteSlot))]
class SaveDataManager_DeleteSlot_Patch
{
    public static void Prefix(int slot)
    {
        ModLog.Info($"Deleting persistent data for slot {slot}");
        SaveData.DeleteDataFromFile(slot);
    }
}
