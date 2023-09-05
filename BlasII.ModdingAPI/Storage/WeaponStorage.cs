using Il2CppTGK.Game.Components.Attack.Data;
using Il2CppTGK.Game.Components.Defense.Data;
using Il2CppTGK.Game.WeaponMemory;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Storage
{
    public static class WeaponStorage
    {
        internal static void Initialize()
        {
            AssetLoader.LoadObjectsOfType(_weapons, _weaponLookup);
            AssetLoader.LoadObjectsOfType(_weaponMemories);
            AssetLoader.LoadObjectsOfType(_armors);
        }

        // Weapons

        private static readonly Dictionary<string, WeaponID> _weapons = new();

        public static bool TryGetWeapon(string id, out WeaponID weapon) => _weapons.TryGetValue(id, out weapon);

        public static IEnumerable<WeaponID> GetAllWeapons() => _weapons.OrderBy(x => x.Key).Select(x => x.Value);

        private static readonly Dictionary<string, string> _weaponLookup = new()
        {
            { "Censer", "WE01" },
            { "Rosary Blade", "WE02" },
            { "Rapier", "WE03" },
        };

        // Weapon memories

        private static readonly Dictionary<string, WeaponMemoryID> _weaponMemories = new();

        public static bool TryGetWeaponMemory(string id, out WeaponMemoryID weaponMemory) => _weaponMemories.TryGetValue(id, out weaponMemory);

        public static IEnumerable<WeaponMemoryID> GetAllWeaponMemories() => _weaponMemories.OrderBy(x => x.Key).Select(x => x.Value);

        // Armors

        private static readonly Dictionary<string, ArmorID> _armors = new();

        public static bool TryGetArmor(string id, out ArmorID armor) => _armors.TryGetValue(id, out armor);

        public static IEnumerable<ArmorID> GetAllArmors() => _armors.OrderBy(x => x.Key).Select(x => x.Value);
    }
}
