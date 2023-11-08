using BlasII.ModdingAPI.Assets;
using Il2CppLightbug.Kinematic2D.Implementation;
using System.Collections.Generic;

namespace BlasII.ModdingAPI.Storage
{
    public static class AbilityStorage
    {
        [System.Obsolete("Use new AssetStorage instead")]
        public static bool TryGetAbility(string id, out IAbilityTypeRef ability) => AssetStorage.Abilities.TryGetAsset(id, out ability);
        [System.Obsolete("Use new AssetStorage instead")]
        public static IEnumerable<KeyValuePair<string, IAbilityTypeRef>> GetAllAbilities() => AssetStorage.Abilities.AllAssets;
    }
}