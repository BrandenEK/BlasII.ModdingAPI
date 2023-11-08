using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.ModdingAPI.Assets
{
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
    }
}
