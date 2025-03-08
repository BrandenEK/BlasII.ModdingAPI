
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Persistence;

public class GlobalSaveData
{
    internal static void Save()
    {
        var data = new Dictionary<string, GlobalSaveData>();

        //Main.ModLoader.ProcessModFunction(mod =>
        //{
        //    if (mod is IGlobalPersistentMod persistentMod)
        //        data.Add(mod.Id, persistentMod.Save());
        //});
    }

    internal static void Load()
    {
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
}
