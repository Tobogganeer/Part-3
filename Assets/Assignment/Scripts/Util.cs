using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

    Collection of various utilities (classes, extension methods, etc)

*/


/// <summary>
/// A dictionary that can be edited in the Unity inspector
/// </summary>
public class SerializeableDictionary<TKey, TValue>
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
    public Dictionary<TKey, TValue> dict
    {
        get
        {
            if (_dict == null)
            {
                // Add the values from the inspector array to the dict
                _dict = new Dictionary<TKey, TValue>();
                foreach (SerializedPair pair in _values)
                {
                    _dict.Add(pair.key, pair.value);
                }
            }

            return _dict;
        }
    }
}
