using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictPP : SerializableDict<PlayerData.PP, float>
{
    public SerializableDictPP()
    : base() {
        ;
    }

    public float GetAverage()
    {
        float sum = 0;

        foreach(float value in d.Values)
        {
            sum += value;
        }

        return sum / d.Values.Count;
    }
}
