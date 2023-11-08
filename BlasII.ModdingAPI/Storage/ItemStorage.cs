using BlasII.ModdingAPI.Assets;
using Il2CppTGK.Game.Components.Inventory;
using Il2CppTGK.Inventory;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Storage
{
    public static class ItemStorage
    {
        public static InventoryComponent PlayerInventory => AssetStorage.PlayerInventory;

        // Rosary beads

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetRosaryBead(string id, out RosaryBeadItemID bead) => AssetStorage.Beads.TryGetAsset(id, out bead);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, RosaryBeadItemID>> GetAllRosaryBeads() => AssetStorage.Beads.AllAssets;

        // Prayers

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetPrayer(string id, out PrayerItemID prayer) => AssetStorage.Prayers.TryGetAsset(id, out prayer);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, PrayerItemID>> GetAllPrayers() => AssetStorage.Prayers.AllAssets;

        // Figures

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetFigure(string id, out FigureItemID figure) => AssetStorage.Figures.TryGetAsset(id, out figure);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, FigureItemID>> GetAllFigures() => AssetStorage.Figures.AllAssets;

        // Quest items

        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetQuestItem(string id, out QuestItemID questItem) => AssetStorage.QuestItems.TryGetAsset(id, out questItem);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, QuestItemID>> GetAllQuestItems() => AssetStorage.QuestItems.AllAssets;
    }
}
