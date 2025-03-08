using Il2CppTGK.Game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BlasII.ModdingAPI.Persistence;

public class GlobalSaveData
{
    internal static void Save()
    {
        ModLog.Custom($"Saving global data", Color.Blue);
        var sb = new StringBuilder();

        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod is IGlobalPersistentMod persistentMod)
            {
                sb.AppendLine(mod.Id);
                sb.AppendLine(JsonConvert.SerializeObject(persistentMod.Save()));
            }
        });

        try
        {
            File.WriteAllText(GetGlobalDataPath(), sb.ToString());
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to save global data: {e.GetType()}");
        }
    }

    internal static void Load()
    {
        ModLog.Custom($"Loading global data", Color.Blue);
        var datas = new Dictionary<string, string>();

        try
        {
            string[] lines = File.ReadAllLines(GetGlobalDataPath());
            for (int i = 0; i < lines.Length - 1; i += 2)
            {
                datas.Add(lines[0], lines[1]);
            }
        }
        catch (Exception e)
        {
            ModLog.Error($"Failed to load global data: {e.GetType()}");
        }

        Main.ModLoader.ProcessModFunction(mod =>
        {
            if (mod is IGlobalPersistentMod persistentMod && datas.TryGetValue(mod.Id, out string json))
            {
                GlobalSaveData data = JsonConvert.DeserializeObject(json, persistentMod.GlobalDataType) as GlobalSaveData;
                persistentMod.Load(data);
            }
        });
    }

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
