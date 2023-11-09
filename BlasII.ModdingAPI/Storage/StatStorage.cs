using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Game.Components.StatsSystem;
using Il2CppTGK.Game.Components.StatsSystem.Data;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Storage
{
    public static class StatStorage
    {
        public static StatsComponent PlayerStats => AssetStorage.PlayerStats;

        // Main stats

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetMainStat(string id, out MainStatID mainStat) => AssetStorage.MainStats.TryGetAsset(id, out mainStat);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, MainStatID>> GetAllMainStats() => AssetStorage.MainStats.AllAssets;

        // Value stats

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetValueStat(string id, out ValueStatID valueStat) => AssetStorage.ValueStats.TryGetAsset(id, out valueStat);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, ValueStatID>> GetAllValueStats() => AssetStorage.ValueStats.AllAssets;

        // Range stats

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetRangeStat(string id, out RangeStatID rangeStat) => AssetStorage.RangeStats.TryGetAsset(id, out rangeStat);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, RangeStatID>> GetAllRangeStats() => AssetStorage.RangeStats.AllAssets;

        // Bonus stats

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetBonusStat(string id, out BonuseableValueStatID bonusStat) => AssetStorage.BonusStats.TryGetAsset(id, out bonusStat);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, BonuseableValueStatID>> GetAllBonusStats() => AssetStorage.BonusStats.AllAssets;

        // Modifiable stats

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetModifiableStat(string id, out ModifiableStatID modifiableStat) => AssetStorage.ModifiableStats.TryGetAsset(id, out modifiableStat);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, ModifiableStatID>> GetAllModifiableStats() => AssetStorage.ModifiableStats.AllAssets;
    }
}
