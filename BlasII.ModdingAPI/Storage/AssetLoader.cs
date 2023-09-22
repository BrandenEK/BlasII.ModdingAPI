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

        public static void LoadObjectsOfType<T>(Dictionary<string, T> storage, string prefix, string[] names) where T : ScriptableObject
        {
            storage.Clear();
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
        }

        [System.Obsolete("Pass in a list of names to prevent ids from changing across versions")]
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
    }
}
