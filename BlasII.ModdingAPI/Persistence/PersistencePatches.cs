using HarmonyLib;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers;
using Il2CppTGK.Game.Managers.SaveData;
using Il2CppTGK.Persistence;
using System.Drawing;
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

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.SaveGame), [])]
class SaveDataManager_SaveGame1_Patch
{
    public static void Postfix()
    {
        int slot = CoreCache.SaveData.CurrentSaveSlot;
        ModLog.Custom($"Saving data for slot {slot}", Color.Blue);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.SaveGame), typeof(int))]
class SaveDataManager_SaveGame2_Patch
{
    public static void Postfix(int slot)
    {
        ModLog.Custom($"Saving data for slot {slot}", Color.Blue);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.SaveGameInEnding))]
class SaveDataManager_SaveGameInEnding_Patch
{
    public static void Postfix()
    {
        int slot = CoreCache.SaveData.CurrentSaveSlot;
        ModLog.Custom($"Saving data for slot {slot}", Color.Blue);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.LoadGame))]
class SaveDataManager_LoadGame_Patch
{
    public static void Postfix(int slot)
    {
        ModLog.Custom($"Loading data for slot {slot}", Color.Blue);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.LoadGameWithoutReset))]
class SaveDataManager_LoadGameWithoutReset_Patch
{
    public static void Postfix(int slot)
    {
        ModLog.Custom($"Loading data for slot {slot}", Color.Blue);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.DeleteSlot))]
class SaveDataManager_DeleteSlot_Patch
{
    public static void Postfix(int slot)
    {
        ModLog.Custom($"Deleting data for slot {slot}", Color.Blue);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.CopySlot))]
class SaveDataManager_CopySlot_Patch
{
    public static void Postfix(int slotSrc, int slotDest)
    {
        ModLog.Custom($"Copying data for slot {slotSrc} to slot {slotDest}", Color.Blue);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.ResetPersistence))]
class SaveDataManager_ResetPersistence_Patch
{
    public static void Postfix()
    {
        ModLog.Custom($"Resetting data for all slots", Color.Blue);
    }
}
