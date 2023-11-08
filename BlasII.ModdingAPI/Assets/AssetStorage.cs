using Il2CppLightbug.Kinematic2D.Implementation;
using Il2CppTGK.Framework.Localization;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.Attack.Data;
using Il2CppTGK.Game.Components.Inventory;
using Il2CppTGK.Game.Components.StatsSystem;
using Il2CppTGK.Game.Components.StatsSystem.Data;
using Il2CppTGK.Game.WeaponMemory;
using Il2CppTGK.Inventory;

namespace BlasII.ModdingAPI.Assets
{
    public static class AssetStorage
    {
        public static AssetStore<RosaryBeadItemID> Beads { get; private set; }
        public static AssetStore<PrayerItemID> Prayers { get; private set; }
        public static AssetStore<FigureItemID> Figures { get; private set; }
        public static AssetStore<QuestItemID> QuestItems { get; private set; }

        public static AssetStore<IAbilityTypeRef> Abilities { get; private set; }

        public static AssetStore<WeaponID> Weapons { get; private set; }
        public static AssetStore<WeaponMemoryID> WeaponMemories { get; private set; }

        public static AssetStore<MainStatID> MainStats { get; private set; }
        public static AssetStore<ValueStatID> ValueStats { get; private set; }
        public static AssetStore<RangeStatID> RangeStats { get; private set; }
        public static AssetStore<BonuseableValueStatID> BonusStats { get; private set; }
        public static AssetStore<ModifiableStatID> ModifiableStats { get; private set; }

        internal static void Initialize()
        {
            Beads = new AssetStore<RosaryBeadItemID>(
                AssetLoader.LoadObjectsOfType<RosaryBeadItemID>());
            Prayers = new AssetStore<PrayerItemID>(
                AssetLoader.LoadObjectsOfType<PrayerItemID>());
            Figures = new AssetStore<FigureItemID>(
                AssetLoader.LoadObjectsOfType<FigureItemID>());
            QuestItems = new AssetStore<QuestItemID>(
                AssetLoader.LoadObjectsOfType<QuestItemID>());

            Abilities = new AssetStore<IAbilityTypeRef>(
                AssetLoader.LoadObjectsOfType<IAbilityTypeRef>("AB", AbilityNames));

            Weapons = new AssetStore<WeaponID>(
                AssetLoader.LoadObjectsOfType<WeaponID>("WE", WeaponNames));
            WeaponMemories = new AssetStore<WeaponMemoryID>(
                AssetLoader.LoadObjectsOfType<WeaponMemoryID>("WM"));

            MainStats = new AssetStore<MainStatID>(
                AssetLoader.LoadObjectsOfType<MainStatID>());
            ValueStats = new AssetStore<ValueStatID>(
                AssetLoader.LoadObjectsOfType<ValueStatID>());
            RangeStats = new AssetStore<RangeStatID>(
                AssetLoader.LoadObjectsOfType<RangeStatID>());
            BonusStats = new AssetStore<BonuseableValueStatID>(
                AssetLoader.LoadObjectsOfType<BonuseableValueStatID>());
            ModifiableStats = new AssetStore<ModifiableStatID>(
                AssetLoader.LoadObjectsOfType<ModifiableStatID>());
        }

        internal static void LocalizeAllItems()
        {
            foreach (var bead in Beads.AllAssets)
                LocalizeItem(bead.Value);
            foreach (var prayer in Prayers.AllAssets)
                LocalizeItem(prayer.Value);
            foreach (var figure in Figures.AllAssets)
                LocalizeItem(figure.Value);
            foreach (var questItem in QuestItems.AllAssets)
                LocalizeItem(questItem.Value);
        }

        private static void LocalizeItem(ItemID item)
        {
            CoreCache.Localization.Localize(item.Cast<ILocalizable>());
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

        private static string[] AbilityNames => new string[]
        {
            "AirDashTypeRef",
            "AirJumpTypeRef",
            "AirRecoveryTypeRef",
            "ApplyDirectionalImpulseAbilityTypeRef",
            "AttackAbilityTypeRef",
            "BladeBerserkModeActivatorTypeRef",
            "BladeParryTypeRef",
            "CenserChargedAttackAbilityTypeRef",
            "CenserIgniterTypeRef",
            "ChangeWeaponTypeRef",
            "CharacterFacingDirectionOnAirTypeRef",
            "CharacterFacingDirectionRef",
            "CrouchTypeRef",
            "DashTypeRef",
            "DragAbilityTypeRef",
            "EtherealLeversUseTypeRef",
            "ExecutionAbilityTypeRef",
            "FacingDirectionTypeRef",
            "FastPrayerTypeRef",
            "FlaskUseTypeRef",
            "FullPrayerTypeRef",
            "GhostClimbAbilityTypeRef",
            "GravityTypeRef",
            "HardLandingTypeRef",
            "HorizontalImpulseAbilityTypeRef",
            "HorizontalMovementAbilityTypeRef",
            "HorizontalMovementOnAirTypeRef",
            "InstadeathAbilityTypeRef",
            "InteractableUseTypeRef",
            "JumpDownTypeRef",
            "JumpTypeRef",
            "LadderClimbTypeRef",
            "LedgeClimbTypeRef",
            "LedgeWallClimbTypeRef",
            "MagicRingClimbTypeRef",
            "RapierParryTypeRef",
            "RapierTeleportTypeRef",
            "StaggerTypeRef",
            "SuperArmorAbilityRefType",
            "ThrowObjectAbilityTypeRef",
            "ThrowObjectDirectionAbilityRef",
            "ThrowObjectToTargetAbilityRef",
            "VerticalImpulseAbilityTypeRef",
            "WallClimbAbilityTypeRef",
            "WallJumpTypeRef",
            "ZipLineSlideTypeRef",
            "GroundRecoveryTypeRef",
        };

        private static string[] WeaponNames => new string[]
        {
            "Censer",
            "RosaryBlade",
            "Rapier",
        };
    }
}
