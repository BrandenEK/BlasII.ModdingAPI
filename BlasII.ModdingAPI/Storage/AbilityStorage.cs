using Il2CppLightbug.Kinematic2D.Implementation;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Storage
{
    public static class AbilityStorage
    {
        internal static void Initialize()
        {
            AssetLoader.LoadObjectsOfType(_abilities, "AB", AllAbilityNames);
        }

        private static readonly Dictionary<string, IAbilityTypeRef> _abilities = new();

        public static bool TryGetAbility(string id, out IAbilityTypeRef ability) => _abilities.TryGetValue(id, out ability);

        public static IEnumerable<KeyValuePair<string, IAbilityTypeRef>> GetAllAbilities() => _abilities.OrderBy(x => x.Key);

        private static string[] AllAbilityNames => new string[]
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
    }
}
