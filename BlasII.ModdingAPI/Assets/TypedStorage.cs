using System.Collections.Generic;
using System.Linq;

namespace BlasII.ModdingAPI.Assets;

/// <summary>
/// Stores ScriptableObject assets of a generic type
/// </summary>
public class TypedStorage<T>
{
    private readonly Dictionary<string, T> _assets = new();

    internal TypedStorage(Dictionary<string, T> assets) => _assets = assets;

    /// <summary>
    /// Retrieves an asset with the specified id
    /// </summary>
    public T this[string id] => _assets[id];

    /// <summary>
    /// Attempts to retrieve an asset with the specified id
    /// </summary>
    public bool TryGetAsset(string id, out T asset) => _assets.TryGetValue(id, out asset);

    /// <summary>
    /// Retrieves all assets in this storage
    /// </summary>
    public IEnumerable<KeyValuePair<string, T>> AllAssets => _assets.OrderBy(x => x.Key);
}
