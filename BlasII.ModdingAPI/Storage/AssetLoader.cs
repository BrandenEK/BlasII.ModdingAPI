using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.ModdingAPI.Storage
{
    internal static class AssetLoader
    {
        public static void LoadObjectsOfType<T>(Dictionary<string, T> storage) where T : ScriptableObject
        {
            storage.Clear();
            foreach (T obj in Resources.FindObjectsOfTypeAll<T>())
            {
                storage.Add(obj.name.Replace(" ", ""), obj);
            }
        }

        // Don't use this
        public static void LoadObjectsOfType<T>(Dictionary<string, T> storage, Dictionary<string, string> lookup) where T : ScriptableObject
        {
            storage.Clear();
            foreach (T obj in Resources.FindObjectsOfTypeAll<T>())
            {
                if (lookup.TryGetValue(obj.name, out string id))
                    storage.Add(id, obj);
            }
        }

        public static void LoadObjectsOfType<T>(Dictionary<string, T> storage, string prefix) where T : ScriptableObject
        {
            storage.Clear();
            var objects = Resources.FindObjectsOfTypeAll<T>().OrderBy(x => x.name);
            int idx = 1;
            foreach (T obj in objects)
            {
                storage.Add(prefix + idx.ToString("00"), obj);
                idx++;
            }
        }

        public static void LoadObjectsOfType<TKey, TValue>(Dictionary<TKey, TValue> storage, string[] replacements) where TKey : struct where TValue : ScriptableObject
        {
            storage.Clear();
            foreach (TValue obj in Resources.FindObjectsOfTypeAll<TValue>())
            {
                string name = obj.name.Replace(" ", "");
                if (replacements != null)
                {
                    foreach (string r in replacements)
                        name = name.Replace(r, "");
                }

                if (Enum.TryParse(name, out TKey type))
                {
                    storage.Add(type, obj);
                }
            }
        }

        public static T[] LoadObjectsOfType<T>() where T : ScriptableObject
        {
            return Resources.FindObjectsOfTypeAll<T>();
        }
    }
}
