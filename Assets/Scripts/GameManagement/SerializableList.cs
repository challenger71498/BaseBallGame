using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableList<T>
{
    public T this[int index]
    {
        get
        {
            return d[index];
        }
        set
        {
            d[index] = value;
        }
    }

    public SerializableList()
    {
        d = new List<T>();
    }

    public List<T> d;
}
