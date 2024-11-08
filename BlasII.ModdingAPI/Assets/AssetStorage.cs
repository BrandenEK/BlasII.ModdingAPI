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
    /// <summary> Stores all WeaponID assets (WE00) </summary>
    public static GenericDoubleStorage<WeaponID, WEAPON_IDS> Weapons { get; private set; }
    /// <summary> Stores all WeaponMemoryID assets (WM00) </summary>
    public static GenericDoubleStorage<WeaponMemoryID, WEAPON_MEMORY_IDS> WeaponMemories { get; private set; }

    /// <summary> Stores all MainStatID assets (NAME) </summary>
    public static GenericSingleStorage<MainStatID> MainStats { get; private set; }
    /// <summary> Stores all ValueStatID assets (NAME) </summary>
    public static GenericSingleStorage<ValueStatID> ValueStats { get; private set; }
    /// <summary> Stores all RangeStatID assets (NAME) </summary>
    public static GenericSingleStorage<RangeStatID> RangeStats { get; private set; }
    /// <summary> Stores all BonuseableValueStatID assets (NAME) </summary>
    public static GenericSingleStorage<BonuseableValueStatID> BonusStats { get; private set; }
    /// <summary> Stores all ModifiableStatID assets (NAME) </summary>
    public static GenericSingleStorage<ModifiableStatID> ModifiableStats { get; private set; }

    /// <summary>
    /// Initializes and loads all tracked assets
    /// </summary>
    internal static void Initialize()
    {
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
        WeaponMemories = new GenericDoubleStorage<WeaponMemoryID, WEAPON_MEMORY_IDS>(
            AssetLoader.LoadDoubleStandard<WeaponMemoryID, WEAPON_MEMORY_IDS>("WM"));

        MainStats = new GenericSingleStorage<MainStatID>(
            AssetLoader.LoadSingleStandard<MainStatID>());
        ValueStats = new GenericSingleStorage<ValueStatID>(
            AssetLoader.LoadSingleStandard<ValueStatID>());
        RangeStats = new GenericSingleStorage<RangeStatID>(
            AssetLoader.LoadSingleStandard<RangeStatID>());
        BonusStats = new GenericSingleStorage<BonuseableValueStatID>(
            AssetLoader.LoadSingleStandard<BonuseableValueStatID>());
        ModifiableStats = new GenericSingleStorage<ModifiableStatID>(
            AssetLoader.LoadSingleStandard<ModifiableStatID>());

        foreach (var w in Weapons)
        {
            ModLog.Warn(w.Id + ": " + w.StaticId);
        }
        ModLog.Error(Weapons[WEAPON_IDS.RosaryBlade].name);
        ModLog.Error(Weapons[WEAPON_IDS.Rapier].name);
        ModLog.Error(Weapons["WE06"].name);

        foreach (var w in WeaponMemories)
        {
            ModLog.Warn(w.Id + ": " + w.StaticId);
        }

        foreach (var w in Abilities)
        {
            ModLog.Warn(w.Id + ": " + w.StaticId);
        }
        ModLog.Error((Abilities.TryGetValue(ABILITY_IDS.AirDash, out var value) ? value : null).name);

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
