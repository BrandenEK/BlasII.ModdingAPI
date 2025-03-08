using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace BlasII.ModdingAPI.Persistence;

/// <summary>
/// Used to save and load persistent data for a mod
/// </summary>
public abstract class SlotSaveData
{
    /// <summary>
    /// Resets game progress for each mod
    /// </summary>
    internal static void Reset()
    {
        ModLog.Custom($"Resetting data for all slots", Color.Blue);

        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod is ISlotPersistentMod persistentMod)
                persistentMod.ResetSlot();
        });
    }

    /// <summary>
    /// Saves game progress for each mod
    /// </summary>
    internal static void Save(int slot)
    {
        ModLog.Custom($"Saving data for slot {slot}", Color.Blue);
        var data = new Dictionary<string, SlotSaveData>();

        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod is ISlotPersistentMod persistentMod)
                data.Add(mod.Id, persistentMod.SaveSlot());
        });

        try
        {
            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            File.WriteAllText(GetPathForSlot(slot), json);
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to save data for slot {slot}: {e.GetType()}");
        }
    }

    /// <summary>
    /// Loads game progress for each mod
    /// </summary>
    internal static void Load(int slot)
    {
        ModLog.Custom($"Loading data for slot {slot}", Color.Blue);
        var data = new Dictionary<string, SlotSaveData>();

        try
        {
            string json = File.ReadAllText(GetPathForSlot(slot));
            data = JsonConvert.DeserializeObject<Dictionary<string, SlotSaveData>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to load data for slot {slot}: {e.GetType()}");
        }

        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod is ISlotPersistentMod persistentMod && data.TryGetValue(mod.Id, out SlotSaveData save))
                persistentMod.LoadSlot(save);
        });
    }

    /// <summary>
    /// Deletes the modded save data when the main save file is deleted
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
    /// Copies the modded save data when the main save file is copied
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
    /// Based on the slot number, calculates the file path for the modded save data
    /// </summary>
    private static string GetPathForSlot(int slot)
    {
        return Path.Combine(Main.ModdingAPI.FileHandler.SavegamesFolder, $"savegame_{slot}_modded.bin");
    }
}
