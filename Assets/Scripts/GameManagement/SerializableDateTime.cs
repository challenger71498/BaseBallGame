using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDateTime
{
    public SerializableDateTime()
    {
        date = new DateTime();
    }

    public SerializableDateTime(DateTime _date)
    {
        date = _date;
    }

    public DateTime date;
}
