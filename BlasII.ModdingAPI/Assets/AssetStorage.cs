using Il2CppLightbug.Kinematic2D.Implementation;
using Il2CppTGK.Framework.Localization;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.Attack.Data;
using Il2CppTGK.Game.Components.Inventory;
using Il2CppTGK.Game.Components.StatsSystem;
using Il2CppTGK.Game.Components.StatsSystem.Data;
using Il2CppTGK.Game.WeaponMemory;
using Il2CppTGK.Inventory;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Assets;

/// <summary>
/// Loads and stores a variety of useful data objects
/// </summary>
public static class AssetStorage
{
    /// <summary> Stores all RosaryBeadItemID assets (RB00) </summary>
    public static TypedStorage<RosaryBeadItemID> Beads { get; private set; }
    /// <summary> Stores all PrayerItemID assets (PR00) </summary>
    public static TypedStorage<PrayerItemID> Prayers { get; private set; }
    /// <summary> Stores all FigureItemID assets (FG00) </summary>
    public static TypedStorage<FigureItemID> Figures { get; private set; }
    /// <summary> Stores all QuestItemID assets (QI00) </summary>
    public static TypedStorage<QuestItemID> QuestItems { get; private set; }

    /// <summary> Stores all IAbilityTypeRef assets (AB00) </summary>
    public static TypedStorage<IAbilityTypeRef> Abilities { get; private set; }

    /// <summary> Stores all WeaponID assets (WE00) </summary>
    public static TypedStorage<WeaponID> Weapons { get; private set; }
    /// <summary> Stores all WeaponMemoryID assets (WM00) </summary>
    public static TypedStorage<WeaponMemoryID> WeaponMemories { get; private set; }

    /// <summary> Stores all MainStatID assets (NAME) </summary>
    public static TypedStorage<MainStatID> MainStats { get; private set; }
    /// <summary> Stores all ValueStatID assets (NAME) </summary>
    public static TypedStorage<ValueStatID> ValueStats { get; private set; }
    /// <summary> Stores all RangeStatID assets (NAME) </summary>
    public static TypedStorage<RangeStatID> RangeStats { get; private set; }
    /// <summary> Stores all BonuseableValueStatID assets (NAME) </summary>
    public static TypedStorage<BonuseableValueStatID> BonusStats { get; private set; }
    /// <summary> Stores all ModifiableStatID assets (NAME) </summary>
    public static TypedStorage<ModifiableStatID> ModifiableStats { get; private set; }

    /// <summary>
    /// Initializes and loads all tracked assets
    /// </summary>
    internal static void Initialize()
    {
        Beads = new TypedStorage<RosaryBeadItemID>(
            AssetLoader.LoadObjectsOfType<RosaryBeadItemID>());
        Prayers = new TypedStorage<PrayerItemID>(
            AssetLoader.LoadObjectsOfType<PrayerItemID>());
        Figures = new TypedStorage<FigureItemID>(
            AssetLoader.LoadObjectsOfType<FigureItemID>());
        QuestItems = new TypedStorage<QuestItemID>(
            AssetLoader.LoadObjectsOfType<QuestItemID>());

        Abilities = new TypedStorage<IAbilityTypeRef>(
            AssetLoader.LoadObjectsOfType<IAbilityTypeRef>("AB", AbilityNames));

        Weapons = new TypedStorage<WeaponID>(
            AssetLoader.LoadObjectsOfType<WeaponID>("WE", WeaponNames));
        WeaponMemories = new TypedStorage<WeaponMemoryID>(
            AssetLoader.LoadObjectsOfType<WeaponMemoryID>("WM"));

        MainStats = new TypedStorage<MainStatID>(
            AssetLoader.LoadObjectsOfType<MainStatID>());
        ValueStats = new TypedStorage<ValueStatID>(
            AssetLoader.LoadObjectsOfType<ValueStatID>());
        RangeStats = new TypedStorage<RangeStatID>(
            AssetLoader.LoadObjectsOfType<RangeStatID>());
        BonusStats = new TypedStorage<BonuseableValueStatID>(
            AssetLoader.LoadObjectsOfType<BonuseableValueStatID>());
        ModifiableStats = new TypedStorage<ModifiableStatID>(
            AssetLoader.LoadObjectsOfType<ModifiableStatID>());

        List<DoubleIdAsset<WeaponID, WeaponIds>> weapons = new List<DoubleIdAsset<WeaponID, WeaponIds>>()
        {
            new DoubleIdAsset<WeaponID, WeaponIds>("WE01", WeaponIds.BrokenSword, null),
            new DoubleIdAsset<WeaponID, WeaponIds>("WE02", WeaponIds.Censer, null),
        };
        GenericDoubleStorage<WeaponID, WeaponIds> store = new GenericDoubleStorage<WeaponID, WeaponIds>(weapons);

        foreach (var w in store)
        {
            ModLog.Error(w.Id + ": " + w.StaticId);
        }
        ModLog.Info(store["WE01"]);
        ModLog.Info(store[WeaponIds.Censer]);
    }

    enum WeaponIds
    {
        BrokenSword,
        Censer,
        MeaCulpa,
        NoWeapon,
        Rapier,
        RosaryBlade,
    }

    /// <summary>
    /// Whenever language changes, localize all item objects
    /// </summary>
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

        static void LocalizeItem(ItemID item)
        {
            CoreCache.Localization.Localize(item.Cast<ILocalizable>());
        }
    }

    private static InventoryComponent _playerInventory;
    /// <summary>
    /// A permanent reference to the player's inventory component
    /// </summary>
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
    /// <summary>
    /// A permanent reference to the player's stat component
    /// </summary>
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
