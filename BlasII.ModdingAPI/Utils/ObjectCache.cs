using System;
using UnityEngine;

namespace BlasII.ModdingAPI.Utils
{
    /// <summary>
    /// A wrapper for a cacheable object that needs to be found first
    /// </summary>
    public class ObjectCache<T> where T : MonoBehaviour
    {
        private readonly Func<T> _search;
        private T _value = null;

        /// <summary>
        /// Initializes the search method for caching the object
        /// </summary>
        public ObjectCache(Func<T> search) => _search = search;

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
    }

    /// <summary>
    /// A wrapper for a cacheable object that needs to be found first
    /// </summary>
    public class ObjectCache
    {
        private readonly Func<GameObject> _search;
        private GameObject _value = null;

        /// <summary>
        /// Initializes the search method for caching the object
        /// </summary>
        public ObjectCache(Func<GameObject> search) => _search = search;

        /// <summary>
        /// Retrieves the object from the cache or by using the search method
        /// </summary>
        public GameObject Value
        {
            get
            {
                if (_value == null)
                    _value = _search();

                return _value;
            }
        }
    }
}
