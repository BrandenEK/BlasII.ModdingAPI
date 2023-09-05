using Il2CppLightbug.Kinematic2D.Implementation;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Storage
{
    public static class AbilityStorage
    {
        internal static void Initialize()
        {
            AssetLoader.LoadObjectsOfType(_abilities, "AB");
        }

        private static readonly Dictionary<string, IAbilityTypeRef> _abilities = new();

        public static bool TryGetAbility(string id, out IAbilityTypeRef ability) => _abilities.TryGetValue(id, out ability);

        public static IEnumerable<KeyValuePair<string, IAbilityTypeRef>> GetAllAbilities() => _abilities.OrderBy(x => x.Key);
    }
}
