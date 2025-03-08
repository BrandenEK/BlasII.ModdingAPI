﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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
        ModLog.Custom($"Resetting data for all slots", Color.Blue);

        Main.ModLoader.ProcessModFunction(mod =>
        {
            Type modType = GetInterfaceType(mod);

            if (modType == null)
                return;

            var reset = modType.GetMethod(nameof(ISlotPersistentMod<SlotSaveData>.ResetSlot), BindingFlags.Instance | BindingFlags.Public);
            reset.Invoke(mod, []);
        });
    }

    /// <summary>
    /// Saves the slot's save file for all persistent mods
    /// </summary>
    internal static void Save(int slot)
    {
        ModLog.Custom($"Saving data for slot {slot}", Color.Blue);
        
        var datas = LoadFile(slot);

        Main.ModLoader.ProcessModFunction(mod =>
        {
            Type modType = GetInterfaceType(mod);

            if (modType == null)
                return;

            var save = modType.GetMethod(nameof(ISlotPersistentMod<SlotSaveData>.SaveSlot), BindingFlags.Instance | BindingFlags.Public);
            object data = save.Invoke(mod, []);

            datas[mod.Id] = JsonConvert.SerializeObject(data);
        });

        SaveFile(slot, datas);
    }

    /// <summary>
    /// Saves the json to the slot's save file
    /// </summary>
    private static void SaveFile(int slot, Dictionary<string, string> datas)
    {
        var sb = new StringBuilder();

        foreach (var kvp in datas)
        {
            sb.AppendLine(kvp.Key);
            sb.AppendLine(kvp.Value);
        }

        try
        {
            File.WriteAllText(GetSlotDataPath(slot), sb.ToString());
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to save data for slot {slot}: {e.GetType()}");
        }
    }

    /// <summary>
    /// Loads the slot's save file for all persistent mods
    /// </summary>
    internal static void Load(int slot)
    {
        ModLog.Custom($"Loading data for slot {slot}", Color.Blue);

        var datas = LoadFile(slot);

        Main.ModLoader.ProcessModFunction(mod =>
        {
            Type modType = GetInterfaceType(mod);

            if (modType == null)
                return;

            Type dataType = modType.GetGenericArguments()[0];

            if (!datas.TryGetValue(mod.Id, out string json))
            {
                ModLog.Warn($"No slot data could be found for mod {mod.Id}");
                return;
            }

            SlotSaveData data = JsonConvert.DeserializeObject(json, dataType) as SlotSaveData;

            var load = modType.GetMethod(nameof(ISlotPersistentMod<SlotSaveData>.LoadSlot), BindingFlags.Instance | BindingFlags.Public);
            load.Invoke(mod, [data]);
        });
    }

    /// <summary>
    /// Loads the json from the slot's save file
    /// </summary>
    private static Dictionary<string, string> LoadFile(int slot)
    {
        var datas = new Dictionary<string, string>();

        try
        {
            string[] lines = File.ReadAllLines(GetSlotDataPath(slot));
            for (int i = 0; i < lines.Length - 1; i += 2)
            {
                datas.Add(lines[i], lines[i + 1]);
            }
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to load data for slot {slot}: {e.GetType()}");
        }

        return datas;
    }

    /// <summary>
    /// Deletes the slot's save file
    /// </summary>
    internal static void Delete(int slot)
    {
        ModLog.Custom($"Deleting data for slot {slot}", Color.Blue);

        try
        {
            string path = GetSlotDataPath(slot);
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
            string pathSrc = GetSlotDataPath(slotSrc);
            string pathDest = GetSlotDataPath(slotDest);
            File.Copy(pathSrc, pathDest, true);
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to copy data for slot {slotSrc} to slot {slotDest}: {e.GetType()}");
        }
    }

    /// <summary>
    /// Returns the interface type if the mod implements it
    /// </summary>
    private static Type GetInterfaceType(BlasIIMod mod)
    {
        return mod.GetType().GetInterfaces()
            .Where(i => i.IsGenericType)
            .FirstOrDefault(i => i.GetGenericTypeDefinition().IsAssignableFrom(typeof(ISlotPersistentMod<>)));
    }

    /// <summary>
    /// Returns the file path of the slot's save file
    /// </summary>
    private static string GetSlotDataPath(int slot)
    {
        return Path.Combine(Main.ModdingAPI.FileHandler.SavegamesFolder, $"savegame_{slot}_modded.bin");
    }
}
