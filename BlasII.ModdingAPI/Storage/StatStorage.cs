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
            AssetLoader.LoadObjectsOfType(_valueStats);
            AssetLoader.LoadObjectsOfType(_rangeStats);
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

        // Value stats

        private static readonly Dictionary<string, ValueStatID> _valueStats = new();

        public static bool TryGetValueStat(string id, out ValueStatID valueStat) => _valueStats.TryGetValue(id, out valueStat);

        // Range stats

        private static readonly Dictionary<string, RangeStatID> _rangeStats = new();

        public static bool TryGetRangeStat(string id, out RangeStatID rangeStat) => _rangeStats.TryGetValue(id, out rangeStat);
    }
}
