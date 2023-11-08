using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Assets
{
    public class AssetStore<T>
    {
        private readonly Dictionary<string, T> _assets = new();

        internal AssetStore(Dictionary<string, T> assets) => _assets = assets;

        public T this[string id] => _assets[id];

        public bool TryGetAsset(string id, out T asset) => _assets.TryGetValue(id, out asset);

        public IEnumerable<KeyValuePair<string, T>> AllAssets => _assets.OrderBy(x => x.Key);
    }
}
