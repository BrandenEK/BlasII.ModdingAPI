using Il2CppTGK.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.ModdingAPI.Assets;

internal static class AssetLoader
{
    /// <summary>
    /// Loads all objects of type T and indexes them based on name
    /// </summary>
    public static Dictionary<string, T> LoadObjectsOfType<T>() where T : ScriptableObject
    {
        var storage = new Dictionary<string, T>();

        foreach (T obj in Resources.FindObjectsOfTypeAll<T>())
        {
            storage.Add(obj.name.Replace(" ", ""), obj);
        }

        return storage;
    }

    /// <summary>
    /// Loads all objects of type T and indexes them based on prefix and specified names
    /// </summary>
    public static Dictionary<string, T> LoadObjectsOfType<T>(string prefix, string[] names) where T : ScriptableObject
    {
        var storage = new Dictionary<string, T>();

        foreach (T obj in Resources.FindObjectsOfTypeAll<T>().OrderBy(x => x.name))
        {
            string abilityName = obj.name.Replace(" ", "");
            for (int i = 0; i < names.Length; i++)
            {
                if (abilityName == names[i])
                {
                    storage.Add(prefix + (i + 1).ToString("00"), obj);
                    break;
                }
            }
        }

        return storage;
    }

    /// <summary>
    /// Loads all objects of type T and indexes them based on prefix
    /// </summary>
    [System.Obsolete("Pass in a list of names to prevent ids from changing across versions")]
    public static Dictionary<string, T> LoadObjectsOfType<T>(string prefix) where T : ScriptableObject
    {
        var storage = new Dictionary<string, T>();

        int idx = 1;
        foreach (T obj in Resources.FindObjectsOfTypeAll<T>().OrderBy(x => x.name))
        {
            storage.Add(prefix + idx.ToString("00"), obj);
            idx++;
        }

        return storage;
    }

    /// <summary>
    /// Loads all objects of type T and indexes them based on name
    /// </summary>
    public static IEnumerable<SingleIdAsset<T>> LoadSingleStandard<T>() where T : ScriptableObject
    {
        return Resources.FindObjectsOfTypeAll<T>()
            .Select(x => new SingleIdAsset<T>(x.name.Replace(" ", ""), x))
            .OrderBy(x => x.Id);
    }

    /// <summary>
    /// Loads all objects of type T and indexes them based on name
    /// </summary>
    public static IEnumerable<SingleIdAsset<T>> LoadSingleItem<T>() where T : ItemID
    {
        return Resources.FindObjectsOfTypeAll<T>()
            .Select(x => new SingleIdAsset<T>(x.name.Replace(" ", ""), x))
            .OrderBy(x => int.Parse(x.Id[2..]));
    }

    /// <summary>
    /// Loads all objects of type T and indexes them based on name
    /// </summary>
    public static IEnumerable<DoubleIdAsset<T, E>> LoadDoubleStandard<T, E>(string prefix) where T : ScriptableObject where E : struct, Enum
    {
        return Resources.FindObjectsOfTypeAll<T>()
            .OrderBy(x => x.name)
            .Select((x, i) => new DoubleIdAsset<T, E>(prefix + (i + 1).ToString("00"), ConvertAssetName<E>(x.name.Replace(" ", "")), x))
            .OrderBy(x => x.Id);
    }

    /// <summary>
    /// Loads all objects of type T and indexes them based on name
    /// </summary>
    //public static IEnumerable<DoubleIdAsset<T, E>> LoadDoubleAbility<T, E>(string prefix) where T : ScriptableObject where E : struct, Enum
    //{
    //    return Resources.FindObjectsOfTypeAll<T>()
    //        .OrderBy(x => x.name)
    //        .Select((x, i) => new DoubleIdAsset<T, E>(prefix + (i + 1).ToString("00"), ConvertAssetName<E>(x.name), x))
    //        .OrderBy(x => x.Id);
    //}

    private static E ConvertAssetName<E>(string name) where E : struct, Enum
    {
        string value = Enum.GetNames<E>().FirstOrDefault(name.StartsWith);
        return value == null
            ? (E)Enum.ToObject(typeof(E), 255)
            : Enum.Parse<E>(value);
    }
}
