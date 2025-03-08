using HarmonyLib;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Managers;

namespace BlasII.ModdingAPI.Persistence;

// Slot patches

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.ResetPersistence))]
class SaveDataManager_ResetPersistence_Patch
{
    public static void Postfix()
    {
        SlotSaveData.Reset();
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.SaveGame), [])]
class SaveDataManager_SaveGame1_Patch
{
    public static void Postfix()
    {
        SlotSaveData.Save(CoreCache.SaveData.CurrentSaveSlot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.SaveGame), typeof(int))]
class SaveDataManager_SaveGame2_Patch
{
    public static void Postfix(int slot)
    {
        SlotSaveData.Save(slot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.SaveGameInEnding))]
class SaveDataManager_SaveGameInEnding_Patch
{
    public static void Postfix()
    {
        SlotSaveData.Save(CoreCache.SaveData.CurrentSaveSlot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.LoadGame))]
class SaveDataManager_LoadGame_Patch
{
    public static void Postfix(int slot)
    {
        SlotSaveData.Load(slot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.LoadGameWithoutReset))]
class SaveDataManager_LoadGameWithoutReset_Patch
{
    public static void Postfix(int slot)
    {
        SlotSaveData.Load(slot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.DeleteSlot))]
class SaveDataManager_DeleteSlot_Patch
{
    public static void Postfix(int slot)
    {
        SlotSaveData.Delete(slot);
    }
}

[HarmonyPatch(typeof(SaveDataManager), nameof(SaveDataManager.CopySlot))]
class SaveDataManager_CopySlot_Patch
{
    public static void Postfix(int slotSrc, int slotDest)
    {
        SlotSaveData.Copy(slotSrc, slotDest);
    }
}

// Global patches

[HarmonyPatch(typeof(GlobalSaveDataManager), nameof(GlobalSaveDataManager.SaveData))]
class GlobalSaveDataManager_SaveData_Patch
{
    public static void Postfix()
    {
        GlobalSaveData.Save();
    }
}

[HarmonyPatch(typeof(GlobalSaveDataManager), nameof(GlobalSaveDataManager.LoadData))]
class GlobalSaveDataManager_LoadData_Patch
{
    public static void Postfix()
    {
        GlobalSaveData.Load();
    }
}

[HarmonyPatch(typeof(GlobalSaveDataManager), nameof(GlobalSaveDataManager.DeleteData))]
class GlobalSaveDataManager_DeleteData_Patch
{
    public static void Postfix()
    {
        GlobalSaveData.Delete();
    }
}
