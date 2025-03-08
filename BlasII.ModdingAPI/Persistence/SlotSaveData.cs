using Newtonsoft.Json;
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
        var datas = new Dictionary<string, string>();

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
    private static Dictionary<string, string> LoadFile(int slot)
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
