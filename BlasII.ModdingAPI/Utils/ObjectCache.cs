using System;

namespace BlasII.ModdingAPI.Utils;

/// <summary>
/// A wrapper for a cacheable object that needs to be found first
/// </summary>
public class ObjectCache<T>(Func<T> search) where T : UnityEngine.Object
{
    private readonly Func<T> _search = search;
    private T _value = null;

    /// <summary>
    /// Retrieves the object from the cache or by using the search method
    /// </summary>
    public T Value
    {
        get
        {
            if (_value == null)
                _value = _search();

            return _value;
        }
    }

    /// <summary>
    /// Casts the object cache to the contained value
    /// </summary>
    public static implicit operator T(ObjectCache<T> cache) => cache.Value;
}
