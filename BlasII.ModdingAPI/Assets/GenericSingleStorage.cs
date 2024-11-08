using System.Collections;
using System.Collections.Generic;
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

public delegate Dictionary<string, T> AssetFinder<T>();
public delegate IEnumerable<KeyValuePair<string, T>> AssetOrderer<T>(Dictionary<string, T> assets);
