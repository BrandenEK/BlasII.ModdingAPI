using Il2CppTGK.Game;
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
/// Used to store data with the global save file
/// </summary>
public class GlobalSaveData
{
    /// <summary>
    /// Saves the global save file for all persistent mods
    /// </summary>
    internal static void Save()
    {
        ModLog.Custom($"Saving global data", Color.Blue);

        var datas = LoadFile();

        Main.ModLoader.ProcessModFunction(mod =>
        {
            Type modType = mod.GetType().GetInterfaces()
                .Where(i => i.IsGenericType)
                .FirstOrDefault(i => i.GetGenericTypeDefinition().IsAssignableFrom(typeof(IGlobalPersistentMod<>)));

            if (modType == null)
                return;

            var save = modType.GetMethod(nameof(IGlobalPersistentMod<GlobalSaveData>.SaveGlobal), BindingFlags.Instance | BindingFlags.Public);
            object data = save.Invoke(mod, []);

            datas[mod.Id] = JsonConvert.SerializeObject(data);
        });

        SaveFile(datas);
    }

    /// <summary>
    /// Saves the json to the global save file
    /// </summary>
    private static void SaveFile(Dictionary<string, string> datas)
    {
        var sb = new StringBuilder();

        foreach (var kvp in datas)
        {
            sb.AppendLine(kvp.Key);
            sb.AppendLine(kvp.Value);
        }

        try
        {
            File.WriteAllText(GetGlobalDataPath(), sb.ToString());
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to save global data: {e.GetType()}");
        }
    }

    /// <summary>
    /// Loads the global save file for all persistent mods
    /// </summary>
    internal static void Load()
    {
        ModLog.Custom($"Loading global data", Color.Blue);

        var datas = LoadFile();

        Main.ModLoader.ProcessModFunction(mod =>
        {
            Type modType = mod.GetType().GetInterfaces()
                .Where(i => i.IsGenericType)
                .FirstOrDefault(i => i.GetGenericTypeDefinition().IsAssignableFrom(typeof(IGlobalPersistentMod<>)));

            if (modType == null)
                return;

            Type dataType = modType.GetGenericArguments()[0];

            if (!datas.TryGetValue(mod.Id, out string json))
            {
                ModLog.Warn($"No global data could be found for mod {mod.Id}");
                return;
            }

            GlobalSaveData data = JsonConvert.DeserializeObject(json, dataType) as GlobalSaveData;

            var load = modType.GetMethod(nameof(IGlobalPersistentMod<GlobalSaveData>.LoadGlobal), BindingFlags.Instance | BindingFlags.Public);
            load.Invoke(mod, [data]);
        });
    }

    /// <summary>
    /// Loads the json from the global save file
    /// </summary>
    private static Dictionary<string, string> LoadFile()
    {
        var datas = new Dictionary<string, string>();

        try
        {
            string[] lines = File.ReadAllLines(GetGlobalDataPath());
            for (int i = 0; i < lines.Length - 1; i += 2)
            {
                datas.Add(lines[i], lines[i + 1]);
            }
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to load global data: {e.GetType()}");
        }

        return datas;
    }

    /// <summary>
    /// Deletes the modded global save file
    /// </summary>
    internal static void Delete()
    {
        ModLog.Custom($"Deleting global data", Color.Blue);

        try
        {
            string path = GetGlobalDataPath();
            File.Delete(path);
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to delete global data: {e.GetType()}");
        }
    }

    /// <summary>
    /// Returns the file path of the global save file
    /// </summary>
    private static string GetGlobalDataPath()
    {
        return CoreCache.StorageManager.BuildPath("GlobalData_modded.data");
    }
}
