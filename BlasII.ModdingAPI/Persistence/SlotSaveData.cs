using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    internal static void ResetGame()
    {
        ModLog.Warn("OLd reset");

        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod is IPersistentMod persistentMod)
                persistentMod.ResetGame();
        });
    }

    /// <summary>
    /// Saves game progress for each mod
    /// </summary>
    internal static void SaveGame(int slot)
    {
        ModLog.Warn("OLd save");
        var data = new Dictionary<string, SlotSaveData>();

        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod is IPersistentMod persistentMod)
                data.Add(mod.Id, persistentMod.SaveGame());
        });

        SaveDataToFile(slot, data);
    }

    /// <summary>
    /// After collecting save data for all persistent mods, serialize it and save to a file
    /// </summary>
    private static void SaveDataToFile(int slot, Dictionary<string, SlotSaveData> data)
    {
        try
        {
            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            File.WriteAllText(GetPathForSlot(slot), json);
        }
        catch (Exception)
        {
            ModLog.Error("Failed to save data for slot " + slot);
        }
    }

    /// <summary>
    /// Loads game progress for each mod
    /// </summary>
    internal static void LoadGame(int slot)
    {
        ModLog.Warn("OLd load");
        var data = LoadDataFromFile(slot);

        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod is IPersistentMod persistentMod && data.TryGetValue(mod.Id, out SlotSaveData save))
                persistentMod.LoadGame(save);
        });
    }

    /// <summary>
    /// Load and deserialize save data for all persistent mods, then give it to them
    /// </summary>
    private static Dictionary<string, SlotSaveData> LoadDataFromFile(int slot)
    {
        try
        {
            string json = File.ReadAllText(GetPathForSlot(slot));
            return JsonConvert.DeserializeObject<Dictionary<string, SlotSaveData>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
        catch (Exception)
        {
            ModLog.Error("Failed to load data for slot " + slot);
            return new Dictionary<string, SlotSaveData>();
        }
    }

    /// <summary>
    /// Deletes the modded save data when the main save file is deleted
    /// </summary>
    internal static void DeleteDataFromFile(int slot)
    {
        ModLog.Warn("OLd delete");
        try
        {
            string path = GetPathForSlot(slot);
            File.Delete(path);
        }
        catch (Exception)
        {
            ModLog.Error("Failed to delete data for slot " + slot);
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
