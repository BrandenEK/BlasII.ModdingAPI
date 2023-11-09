using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Game.Components.Attack.Data;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Storage
{
    public static class WeaponStorage
    {
        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetWeapon(string id, out WeaponID weapon) => AssetStorage.Weapons.TryGetAsset(id, out weapon);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, WeaponID>> GetAllWeapons() => AssetStorage.Weapons.AllAssets;
    }
}
