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
            .Select((x, i) => new DoubleIdAsset<T, E>(prefix + (i + 1).ToString("00"), ConvertAssetName<E>(x.name), x))
            .OrderBy(x => x.Id);
    }

    private static E ConvertAssetName<E>(string name) where E : struct, Enum
    {
        string[] wordstoRemove = ["Type", "Ref", "Ability"];

        while (wordstoRemove.Any(name.Trim().EndsWith))
        {
            name = name[..name.LastIndexOf(' ')];
        }
        name = name.Replace(" ", "");

        string value = Enum.GetNames<E>().FirstOrDefault(x => x == name);
        return value == null
            ? (E)Enum.ToObject(typeof(E), 255)
            : Enum.Parse<E>(value);
    }
}
