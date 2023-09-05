using System.Collections.Generic;
using UnityEngine;

namespace BlasII.ModdingAPI
{
    internal static class AssetLoader
    {
        public static void LoadObjectsOfType<T>(Dictionary<string, T> storage) where T : ScriptableObject
        {
            storage.Clear();
            foreach (T obj in Resources.FindObjectsOfTypeAll<T>())
            {
                storage.Add(obj.name, obj);
            }
        }

        public static void LoadObjectsOfType<T>(Dictionary<string, T> storage, Dictionary<string, string> lookup) where T : ScriptableObject
        {
            storage.Clear();
            foreach (T obj in Resources.FindObjectsOfTypeAll<T>())
            {
                if (lookup.TryGetValue(obj.name, out string id))
                    storage.Add(id, obj);
            }
        }
    }
}
