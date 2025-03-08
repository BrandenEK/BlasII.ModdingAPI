using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BlasII.ModdingAPI.Persistence;

public class GlobalSaveData
{
    internal static void Save()
    {
        ModLog.Custom($"Saving global data", Color.Blue);
        var data = new Dictionary<string, GlobalSaveData>();

        //Main.ModLoader.ProcessModFunction(mod =>
        //{
        //    if (mod is IGlobalPersistentMod persistentMod)
        //        data.Add(mod.Id, persistentMod.Save());
        //});
    }

    internal static void Load()
    {
        ModLog.Custom($"Loading global data", Color.Blue);
        //var data = new Dictionary<string, GlobalSaveData>();

        //try
        //{
        //    string json = string.Empty;
        //    data = JsonConvert.DeserializeObject<Dictionary<string, GlobalSaveData>>(json, new JsonSerializerSettings
        //    {
        //        TypeNameHandling = TypeNameHandling.Auto
        //    });
        //}
        //catch (Exception e)
        //{
        //    ModLog.Error($"Failed to global load data: {e.GetType()}");
        //}

        //Main.ModLoader.ProcessModFunction(mod =>
        //{
        //    if (mod.GetType().IsAssignableFrom(typeof(IGlobalPersistentMod<>)))
        //    {
        //        mod.GetType().gener
        //    }

        //    if (mod is IGlobalPersistentMod persistentMod && data.TryGetValue(mod.Id, out GlobalSaveData save))
        //        persistentMod.Load(save);
        //});
    }

    internal static void Delete()
    {
        ModLog.Custom($"Deleting global data", Color.Blue);
    }
}
