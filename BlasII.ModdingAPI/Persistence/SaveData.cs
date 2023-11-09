using Il2CppTGK.Game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlasII.ModdingAPI.Persistence
{
    /// <summary>
    /// Used to save and load persistent data for a mod
    /// </summary>
    public abstract class SaveData
    {
        /// <summary>
        /// After collecting save data for all persistent mods, serialize it and save to a file
        /// </summary>
        internal static void SaveDataToFile(int slot, Dictionary<string, SaveData> data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                File.WriteAllText(GetPathForSlot(slot), json);
            }
            catch (Exception)
            {
                Main.ModdingAPI.LogError("Failed to save data for slot " + slot);
            }
        }

        /// <summary>
        /// Load and deserialize save data for all persistent mods, then give it to them
        /// </summary>
        internal static Dictionary<string, SaveData> LoadDataFromFile(int slot)
        {
            try
            {
                string json = File.ReadAllText(GetPathForSlot(slot));
                return JsonConvert.DeserializeObject<Dictionary<string, SaveData>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (Exception)
            {
                Main.ModdingAPI.LogError("Failed to load data for slot " + slot);
                return new Dictionary<string, SaveData>();
            }
        }

        /// <summary>
        /// Deletes the modded save data when the main save file is deleted
        /// </summary>
        internal static void DeleteDataFromFile(int slot)
        {
            try
            {
                string path = GetPathForSlot(slot);
                File.Delete(path);
            }
            catch (Exception)
            {
                Main.ModdingAPI.LogError("Failed to delete data for slot " + slot);
            }
        }

        /// <summary>
        /// Based on the slot number, calculates the file path for the modded save data
        /// </summary>
        private static string GetPathForSlot(int slot)
        {
            return $"{CoreCache.StorageManager.BuildPath("Savegames/")}savegame_{slot}_modded.bin";
        }
    }
}
