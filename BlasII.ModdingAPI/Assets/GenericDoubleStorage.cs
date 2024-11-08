using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.ModdingAPI.Assets;

/// <summary>
/// Stores ScriptableObject assets of a generic type
/// </summary>
public class GenericDoubleStorage<T, E> : IEnumerable<DoubleIdAsset<T, E>> where T : ScriptableObject where E : Enum
{
    private readonly IEnumerable<DoubleIdAsset<T, E>> _assets;

    internal GenericDoubleStorage(IEnumerable<DoubleIdAsset<T, E>> assets)
    {
        _assets = assets;
    }

    /// <summary>
    /// Retrieves an asset with the specified id
    /// </summary>
    public T this[string id] => _assets.FirstOrDefault(x => x.Id == id)?.Value;

    /// <summary>
    /// Retrieves an asset with the specified id
    /// </summary>
    public T this[E id] => _assets.FirstOrDefault(x => x.StaticId.Equals(id))?.Value;

    IEnumerator<DoubleIdAsset<T, E>> IEnumerable<DoubleIdAsset<T, E>>.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }
}

public class DoubleIdAsset<T, E> where T : ScriptableObject where E : Enum
{
    public string Id { get; }
    public E StaticId { get; }
    public T Value { get; }

    public DoubleIdAsset(string id, E staticId, T value)
    {
        Id = id;
        StaticId = staticId;
        Value = value;
    }
}
