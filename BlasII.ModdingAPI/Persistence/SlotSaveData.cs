using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace BlasII.ModdingAPI.Persistence;

/// <summary>
/// Used to store data with a slot's save file
/// </summary>
public abstract class SlotSaveData
{
    /// <summary>
    /// Resets the slot's save file for all persistent mods
    /// </summary>
    internal static void Reset()
    {
        //ModLog.Custom($"Resetting data for all slots", Color.Blue);

        //Main.ModLoader.ProcessModFunction(mod =>
        //{
        //    if (mod is ISlotPersistentMod persistentMod)
        //        persistentMod.ResetSlot();
        //});
    }

    /// <summary>
    /// Saves the slot's save file for all persistent mods
    /// </summary>
    internal static void Save(int slot)
    {
        //ModLog.Custom($"Saving data for slot {slot}", Color.Blue);
        //var data = new Dictionary<string, SlotSaveData>();

        //Main.ModLoader.ProcessModFunction(mod =>
        //{
        //    if (mod is ISlotPersistentMod persistentMod)
        //        data.Add(mod.Id, persistentMod.SaveSlot());
        //});

        //try
        //{
        //    string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
        //    {
        //        TypeNameHandling = TypeNameHandling.Auto
        //    });
        //    File.WriteAllText(GetPathForSlot(slot), json);
        //}
        //catch (Exception e)
        //{
        //    ModLog.Error($"Failed to save data for slot {slot}: {e.GetType()}");
        //}
    }

    /// <summary>
    /// Saves the json to the slot's save file
    /// </summary>
    private static void SaveFile(Dictionary<string, string> datas)
    {

    }

    /// <summary>
    /// Loads the slot's save file for all persistent mods
    /// </summary>
    internal static void Load(int slot)
    {
        //ModLog.Custom($"Loading data for slot {slot}", Color.Blue);
        //var data = new Dictionary<string, SlotSaveData>();

        //try
        //{
        //    string json = File.ReadAllText(GetPathForSlot(slot));
        //    data = JsonConvert.DeserializeObject<Dictionary<string, SlotSaveData>>(json, new JsonSerializerSettings
        //    {
        //        TypeNameHandling = TypeNameHandling.Auto
        //    });
        //}
        //catch (Exception e)
        //{
        //    ModLog.Error($"Failed to load data for slot {slot}: {e.GetType()}");
        //}

        //Main.ModLoader.ProcessModFunction(mod =>
        //{
        //    if (mod is ISlotPersistentMod persistentMod && data.TryGetValue(mod.Id, out SlotSaveData save))
        //        persistentMod.LoadSlot(save);
        //});
    }

    /// <summary>
    /// Loads the json from the slot's save file
    /// </summary>
    private static Dictionary<string, string> LoadFile()
    {
        return null;
    }

    /// <summary>
    /// Deletes the slot's save file
    /// </summary>
    internal static void Delete(int slot)
    {
        ModLog.Custom($"Deleting data for slot {slot}", Color.Blue);

        try
        {
            string path = GetPathForSlot(slot);
            File.Delete(path);
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to delete data for slot {slot}: {e.GetType()}");
        }
    }

    /// <summary>
    /// Copies the slot's save file
    /// </summary>
    internal static void Copy(int slotSrc, int slotDest)
    {
        ModLog.Custom($"Copying data for slot {slotSrc} to slot {slotDest}", Color.Blue);

        try
        {
            string pathSrc = GetPathForSlot(slotSrc);
            string pathDest = GetPathForSlot(slotDest);
            File.Copy(pathSrc, pathDest, true);
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to copy data for slot {slotSrc} to slot {slotDest}: {e.GetType()}");
        }
    }

    /// <summary>
    /// Returns the file path of the slot's save file
    /// </summary>
    private static string GetPathForSlot(int slot)
    {
        return Path.Combine(Main.ModdingAPI.FileHandler.SavegamesFolder, $"savegame_{slot}_modded.bin");
    }
}
