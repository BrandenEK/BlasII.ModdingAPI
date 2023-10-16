using Il2CppTGK.Framework.Localization;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.Inventory;
using Il2CppTGK.Inventory;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Storage
{
    public static class ItemStorage
    {
        internal static void Initialize()
        {
            AssetLoader.LoadObjectsOfType(_rosaryBeads);
            AssetLoader.LoadObjectsOfType(_prayers);
            AssetLoader.LoadObjectsOfType(_figures);
            AssetLoader.LoadObjectsOfType(_questItems);

            LocalizeAllItems();
        }

        private static InventoryComponent _playerInventory;
        public static InventoryComponent PlayerInventory
        {
            get
            {
                if (_playerInventory == null)
                    _playerInventory = CoreCache.PlayerSpawn?.PlayerInstance?.GetComponent<InventoryComponent>();
                return _playerInventory;
            }
        }

        internal static void LocalizeAllItems()
        {
            foreach (var bead in _rosaryBeads.Values)
                LocalizeItem(bead);
            foreach (var prayer in _prayers.Values)
                LocalizeItem(prayer);
            foreach (var figure in _figures.Values)
                LocalizeItem(figure);
            foreach (var questItem in _questItems.Values)
                LocalizeItem(questItem);
        }

        private static void LocalizeItem(ItemID item)
        {
            CoreCache.Localization.Localize(item.Cast<ILocalizable>());
        }

        // Rosary beads

        private static readonly Dictionary<string, RosaryBeadItemID> _rosaryBeads = new();

        public static bool TryGetRosaryBead(string id, out RosaryBeadItemID bead) => _rosaryBeads.TryGetValue(id, out bead);

        public static IEnumerable<KeyValuePair<string, RosaryBeadItemID>> GetAllRosaryBeads() => _rosaryBeads.OrderBy(x => x.Key);

        // Prayers

        private static readonly Dictionary<string, PrayerItemID> _prayers = new();

        public static bool TryGetPrayer(string id, out PrayerItemID prayer) => _prayers.TryGetValue(id, out prayer);

        public static IEnumerable<KeyValuePair<string, PrayerItemID>> GetAllPrayers() => _prayers.OrderBy(x => x.Key);

        // Figures

        private static readonly Dictionary<string, FigureItemID> _figures = new();

        public static bool TryGetFigure(string id, out FigureItemID figure) => _figures.TryGetValue(id, out figure);

        public static IEnumerable<KeyValuePair<string, FigureItemID>> GetAllFigures() => _figures.OrderBy(x => x.Key);

        // Quest items

        private static readonly Dictionary<string, QuestItemID> _questItems = new();

        public static bool TryGetQuestItem(string id, out QuestItemID questItem) => _questItems.TryGetValue(id, out questItem);

        public static IEnumerable<KeyValuePair<string, QuestItemID>> GetAllQuestItems() => _questItems.OrderBy(x => x.Key);
    }
}
