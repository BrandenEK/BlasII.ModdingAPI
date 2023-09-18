using Il2CppTGK.Game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlasII.ModdingAPI.Persistence
{
    public abstract class SaveData
    {
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

        private static string GetPathForSlot(int slot)
        {
            return $"{CoreCache.StorageManager.BuildPath("Savegames/")}savegame_{slot}_modded.bin";
        }
    }
}
