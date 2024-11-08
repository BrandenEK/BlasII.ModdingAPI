using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.ModdingAPI.Assets;

/// <summary>
/// Stores ScriptableObject assets of a generic type
/// </summary>
public class GenericSingleStorage<T> : IEnumerable<KeyValuePair<string, T>> where T : ScriptableObject
{
    private readonly Dictionary<string, T> _assets;
    private readonly AssetOrderer<T> _orderer;

    internal GenericSingleStorage(AssetFinder<T> finder, AssetOrderer<T> orderer)
    {
        _assets = finder();
        _orderer = orderer;
    }

    /// <summary>
    /// Retrieves an asset with the specified id
    /// </summary>
    public T this[string id] => _assets[id];

    /// <summary>
    /// Attempts to retrieve an asset with the specified id
    /// </summary>
    public bool TryGetAsset(string id, out T asset) => _assets.TryGetValue(id, out asset);

    /// <summary>
    /// Loops over every asset in the storage
    /// </summary>
    public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
    {
        return _orderer(_assets).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _orderer(_assets).GetEnumerator();
    }
}

public class SingleIdAsset<T> where T : ScriptableObject
{
    public string Id { get; }
    public T Value { get; }
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

public delegate Dictionary<string, T> AssetFinder<T>();
public delegate IEnumerable<KeyValuePair<string, T>> AssetOrderer<T>(Dictionary<string, T> assets);


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

    /// <summary>
    /// Attempts to retrieve an asset with the specified id
    /// </summary>
    //public bool TryGetAsset(string id, out T asset) => _assets.TryGetValue(id, out asset);

    IEnumerator<DoubleIdAsset<T, E>> IEnumerable<DoubleIdAsset<T, E>>.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _assets.GetEnumerator();
    }
}