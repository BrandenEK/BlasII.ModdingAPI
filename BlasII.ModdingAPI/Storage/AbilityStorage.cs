using Il2CppLightbug.Kinematic2D.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Storage
{
    public static class AbilityStorage
    {
        internal static void Initialize()
        {
            AssetLoader.LoadObjectsOfType(_abilities, new string[] { "Ability", "Type", "Ref" });
        }

        private static readonly Dictionary<AbilityType, IAbilityTypeRef> _abilities = new();

        public static bool TryGetAbility(AbilityType id, out IAbilityTypeRef ability) => _abilities.TryGetValue(id, out ability);

        public static IEnumerable<KeyValuePair<AbilityType, IAbilityTypeRef>> GetAllAbilities() => _abilities.OrderBy(x => x.Key);

        private static string[] AllAbilityNames => new string[]
        {

        };
    }

    public enum AbilityType
    {
        // Movement
        HorizontalMovement = 10,
        HorizontalMovementOnAir = 11,
        // Facing
        FacingDirection = 20,
        CharacterFacingDirection = 21,
        CharacterFacingDirectionOnAir = 22,
        // Throwing
        ThrowObject = 30,
        ThrowObjectDirection = 31,
        ThrowObjectToTarget = 32,
        // Climbing
        LadderClimb = 40,
        LedgeClimb = 41,
        LedgeWallClimb = 42,
        // Recovering
        GroundRecovery = 50,
        AirRecovery = 51,
        // Actions
        Attack = 60,
        Crouch = 61,
        Dash = 62,
        FlaskUse = 63,
        JumpDown = 64,
        Jump = 65,
        InteractableUse = 66,
        EtherealLeversUse = 67,
        Execution = 68,
        ChangeWeapon = 69,
        AirDash = 70,
        AirJump = 71,
        // Traits
        Instadeath = 80,
        Stagger = 81,
        SuperArmor = 82,
        HardLanding = 83,
        WallClimb = 84,
        WallJump = 85,
        MagicRingClimb = 86,
        // Censer
        CenserChargedAttack = 100,
        CenserIgniter = 101,
        // Blade
        BladeBerserkModeActivator = 110,
        BladeParry = 111,
        VerticalImpulse = 112,
        // Rapier
        RapierParry = 120,
        RapierTeleport = 121,
        HorizontalImpulse = 122,
        ZipLineSlide = 123,
        // Prayers
        FastPrayer = 130,
        FullPrayer = 131,
        // Unknown
        Drag = 200,
        GhostClimb = 201,
        Gravity = 202,
        ApplyDirectionalImpulse = 203,
    }
}
