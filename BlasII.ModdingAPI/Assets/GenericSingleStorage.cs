using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.ModdingAPI.Assets;

/// <summary>
/// Stores ScriptableObject assets of a generic type
/// </summary>
public class GenericSingleStorage<T> : IEnumerable<SingleIdAsset<T>> where T : ScriptableObject
{
    private readonly IEnumerable<SingleIdAsset<T>> _assets;

    internal GenericSingleStorage(IEnumerable<SingleIdAsset<T>> assets)
    {
        _assets = assets;
    }

    /// <summary>
    /// Retrieves an asset with the specified id
    /// </summary>
    public T this[string id] => _assets.FirstOrDefault(x => x.Id == id)?.Value;

    /// <summary>
    /// Attempts to retrieve an asset with the specified id
    /// </summary>
    public bool TryGetValue(string id, out T value)
    {
        return (value = _assets.FirstOrDefault(x => x.Id == id)?.Value) != null;
    }

    IEnumerator<SingleIdAsset<T>> IEnumerable<SingleIdAsset<T>>.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }
}

/// <summary>
/// Represents an asset with one id
/// </summary>
public class SingleIdAsset<T> where T : ScriptableObject
{
    /// <summary>
    /// The id of the asset
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The actual asset in memory
    /// </summary>
    public T Value { get; }

    internal SingleIdAsset(string id, T value)
    {
        Id = id;
        Value = value;
    }
}
