using System;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a serializable bracket for dictionary.
[Serializable]
public class SerializableDict<T, U>
{
    public U this[T key]
    {
        get
        {
            return d[key];
        }
        set
        {
            d[key] = value;
        }
    }

    public SerializableDict()
    {
        d = new Dictionary<T, U>();
    }

    public Dictionary<T, U> d;
}
