using Il2CppLightbug.Kinematic2D.Implementation;
using Il2CppTGK.Framework.Localization;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.Attack.Data;
using Il2CppTGK.Game.Components.Inventory;
using Il2CppTGK.Game.Components.StatsSystem;
using Il2CppTGK.Game.Components.StatsSystem.Data;
using Il2CppTGK.Game.WeaponMemory;
using Il2CppTGK.Inventory;

namespace BlasII.ModdingAPI.Assets;

/// <summary>
/// Loads and stores a variety of useful data objects
/// </summary>
public static class AssetStorage
{
    

    /// <summary> Stores all WeaponID assets (WE00) </summary>
    public static GenericDoubleStorage<WeaponID, WEAPON_IDS> Weapons { get; private set; }
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

    /// <summary> Stores all RosaryBeadItemID assets (RB00) </summary>
    public static GenericSingleStorage<RosaryBeadItemID> Beads { get; private set; }
    /// <summary> Stores all PrayerItemID assets (PR00) </summary>
    public static GenericSingleStorage<PrayerItemID> Prayers { get; private set; }
    /// <summary> Stores all FigureItemID assets (FG00) </summary>
    public static GenericSingleStorage<FigureItemID> Figures { get; private set; }
    /// <summary> Stores all QuestItemID assets (QI00) </summary>
    public static GenericSingleStorage<QuestItemID> QuestItems { get; private set; }

    /// <summary> Stores all IAbilityTypeRef assets (AB00) </summary>
    public static GenericDoubleStorage<IAbilityTypeRef, ABILITY_IDS> Abilities { get; private set; }

    /// <summary>
    /// Initializes and loads all tracked assets
    /// </summary>
    internal static void Initialize()
    {

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

        Beads = new GenericSingleStorage<RosaryBeadItemID>(
            AssetLoader.LoadSingleItem<RosaryBeadItemID>());
        Prayers = new GenericSingleStorage<PrayerItemID>(
            AssetLoader.LoadSingleItem<PrayerItemID>());
        Figures = new GenericSingleStorage<FigureItemID>(
            AssetLoader.LoadSingleItem<FigureItemID>());
        QuestItems = new GenericSingleStorage<QuestItemID>(
            AssetLoader.LoadSingleItem<QuestItemID>());

        Abilities = new GenericDoubleStorage<IAbilityTypeRef, ABILITY_IDS>(
            AssetLoader.LoadDoubleStandard<IAbilityTypeRef, ABILITY_IDS>("AB"));
        Weapons = new GenericDoubleStorage<WeaponID, WEAPON_IDS>(
            AssetLoader.LoadDoubleStandard<WeaponID, WEAPON_IDS>("WE"));

        foreach (var w in Weapons)
        {
            ModLog.Warn(w.Id + ": " + w.StaticId);
        }
        ModLog.Error(Weapons[WEAPON_IDS.RosaryBlade].name);
        ModLog.Error(Weapons[WEAPON_IDS.Rapier].name);
        ModLog.Error(Weapons["WE06"].name);

        foreach (var item in Beads)
        {
            ModLog.Info(item.Id + ": " + item.Value.caption);
        }
        ModLog.Error(Beads["RB02"].description);
    }

    /// <summary>
    /// Whenever language changes, localize all item objects
    /// </summary>
    internal static void LocalizeAllItems()
    {
        foreach (var bead in Beads)
            LocalizeItem(bead.Value);
        foreach (var prayer in Prayers)
            LocalizeItem(prayer.Value);
        foreach (var figure in Figures)
            LocalizeItem(figure.Value);
        foreach (var questItem in QuestItems)
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
