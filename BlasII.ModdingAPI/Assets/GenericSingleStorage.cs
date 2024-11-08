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

    IEnumerator<SingleIdAsset<T>> IEnumerable<SingleIdAsset<T>>.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }
}

public class SingleIdAsset<T> where T : ScriptableObject
{
    public string Id { get; }
    public T Value { get; }

    public SingleIdAsset(string id, T value)
    {
        Id = id;
        Value = value;
    }
}
