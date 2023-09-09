using Il2CppTGK.Game.Components.StatsSystem;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.StatsSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Storage
{
    public static class StatStorage
    {
        internal static void Initialize()
        {
            AssetLoader.LoadObjectsOfType(_mainStats);
            AssetLoader.LoadObjectsOfType(_valueStats);
            AssetLoader.LoadObjectsOfType(_rangeStats);
            AssetLoader.LoadObjectsOfType(_bonusStats);
            AssetLoader.LoadObjectsOfType(_modifiableStats);
        }

        private static StatsComponent _playerStats;
        public static StatsComponent PlayerStats
        {
            get
            {
                if (_playerStats == null)
                    _playerStats = CoreCache.PlayerSpawn.PlayerInstance.GetComponent<StatsComponent>();
                return _playerStats;
            }
        }

        // Main stats

        private static readonly Dictionary<string, MainStatID> _mainStats = new();

        public static bool TryGetMainStat(string id, out MainStatID mainStat) => _mainStats.TryGetValue(id, out mainStat);

        public static IEnumerable<KeyValuePair<string, MainStatID>> GetAllMainStats() => _mainStats.OrderBy(x => x.Key);

        // Value stats

        private static readonly Dictionary<string, ValueStatID> _valueStats = new();

        public static bool TryGetValueStat(string id, out ValueStatID valueStat) => _valueStats.TryGetValue(id, out valueStat);

        public static IEnumerable<KeyValuePair<string, ValueStatID>> GetAllValueStats() => _valueStats.OrderBy(x => x.Key);

        // Range stats

        private static readonly Dictionary<string, RangeStatID> _rangeStats = new();

        public static bool TryGetRangeStat(string id, out RangeStatID rangeStat) => _rangeStats.TryGetValue(id, out rangeStat);

        public static IEnumerable<KeyValuePair<string, RangeStatID>> GetAllRangeStats() => _rangeStats.OrderBy(x => x.Key);

        // Bonus stats

        private static readonly Dictionary<string, BonuseableValueStatID> _bonusStats = new();

        public static bool TryGetBonusStat(string id, out BonuseableValueStatID bonusStat) => _bonusStats.TryGetValue(id, out bonusStat);

        public static IEnumerable<KeyValuePair<string, BonuseableValueStatID>> GetAllBonusStats() => _bonusStats.OrderBy(x => x.Key);

        // Modifiable stats

        private static readonly Dictionary<string, ModifiableStatID> _modifiableStats = new();

        public static bool TryGetModifiableStat(string id, out ModifiableStatID modifiableStat) => _modifiableStats.TryGetValue(id, out modifiableStat);

        public static IEnumerable<KeyValuePair<string, ModifiableStatID>> GetAllModifiableStats() => _modifiableStats.OrderBy(x => x.Key);
    }
}
