using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.ModdingAPI.Assets;

/// <summary>
/// Stores ScriptableObject assets of a generic type
/// </summary>
public class GenericDoubleStorage<T, E> : IEnumerable<DoubleIdAsset<T, E>> where T : ScriptableObject where E : struct, Enum
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
    /// Attempts to retrieve an asset with the specified id
    /// </summary>
    public bool TryGetValue(string id, out T value)
    {
        return (value = _assets.FirstOrDefault(x => x.Id == id)?.Value) != null;
    }

    /// <summary>
    /// Retrieves an asset with the specified id
    /// </summary>
    public T this[E id] => _assets.FirstOrDefault(x => x.StaticId.Equals(id))?.Value;

    /// <summary>
    /// Attempts to retrieve an asset with the specified id
    /// </summary>
    public bool TryGetValue(E id, out T value)
    {
        return (value = _assets.FirstOrDefault(x => x.StaticId.Equals(id))?.Value) != null;
    }

    IEnumerator<DoubleIdAsset<T, E>> IEnumerable<DoubleIdAsset<T, E>>.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }
}

/// <summary>
/// Represents an asset with two ids
/// </summary>
public class DoubleIdAsset<T, E> where T : ScriptableObject where E : struct, Enum
{
    /// <summary>
    /// The id of the asset
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The unchanging id of the asset
    /// </summary>
    public E StaticId { get; }

    /// <summary>
    /// The actual asset in memory
    /// </summary>
    public T Value { get; }

    internal DoubleIdAsset(string id, E staticId, T value)
    {
        Id = id;
        StaticId = staticId;
        Value = value;
    }
}
