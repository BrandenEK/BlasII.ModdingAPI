using Il2CppLightbug.Kinematic2D.Implementation;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Storage
{
    public static class AbilityStorage
    {
        internal static void Initialize()
        {
            AssetLoader.LoadObjectsOfType(_abilities, _abilityLookup);
        }

        private static readonly Dictionary<string, IAbilityTypeRef> _abilities = new();

        public static bool TryGetAbility(string id, out IAbilityTypeRef ability) => _abilities.TryGetValue(id, out ability);

        public static IEnumerable<IAbilityTypeRef> GetAllAbilities() => _abilities.OrderBy(x => x.Key).Select(x => x.Value);

        private static readonly Dictionary<string, string> _abilityLookup = new()
        {
            { "Wall Climb Ability Type Ref", "AB01" },
            { "Air Jump Type Ref", "AB02" },
            { "Air Dash Type Ref", "AB03" },
            { "Magic Ring Climb Type Ref", "AB04" },
        };
    }
}
