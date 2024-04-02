using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

    Collection of various utilities (classes, extension methods, etc)

*/


/// <summary>
/// A dictionary that can be edited in the Unity inspector
/// </summary>
[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [System.Serializable]
    class SerializedPair
    {
        public TKey key;
        public TValue value;
    }

    // This can show up in the inspector
    [SerializeField] SerializedPair[] _values;

    Dictionary<TKey, TValue> _dict;
    /// <summary>
    /// A copy of the inspector's values.
    /// </summary>
    /// <remarks>Only use if the values will not change. If in Gizmos or similar, use <seealso cref="CreateDict"/></remarks>
    public Dictionary<TKey, TValue> dict
    {
        get
        {
            if (_dict == null || _dict.Count != _values.Length)
            {
                // Add the values from the inspector array to the dict
                _dict = CreateDict();
            }

            return _dict;
        }
    }

    /// <summary>
    /// Creates a fresh copy of the inspector's values.
    /// </summary>
    /// <returns></returns>
    public Dictionary<TKey, TValue> CreateDict()
    {
        Dictionary<TKey, TValue> newDict = new Dictionary<TKey, TValue>();
        foreach (SerializedPair pair in _values)
            newDict[pair.key] = pair.value;

        return newDict;
    }

    public TValue this[TKey key] => dict[key];
}
