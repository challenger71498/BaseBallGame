using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeagueData
{
    public enum LP
    {
        NAME, SHORT_NAME,
        BIRTH_DATE, EMBLEM
    }

    public List<string> LPString = new List<string>()
    {
        "Name", "Short Name",
        "Established Year", "Emblem"
    };

    public LeagueData(string _name, string _shortName, int year, int month, int day)
    {
        data = new SerializableDict<LP, object>();
        data.d.Add(LP.NAME, _name);
        data.d.Add(LP.SHORT_NAME, _shortName);
        data.d.Add(LP.BIRTH_DATE, new DateTime(year, month, day));
    }

    public dynamic GetData(LP pref)
    {
        if (data.d.ContainsKey(pref))
        {
            return Convert.ChangeType(data[pref], data[pref].GetType());
        }
        else
        {
            throw new NullReferenceException("There is no such value named as " + pref.ToString() + " in data.");
        }
    }

    public void SetData(LP pref, object obj)
    {
        if (data.d.ContainsKey(pref))
        {
            data[pref] = obj;
        }
        else
        {
            data.d.Add(pref, obj);
        }
    }

    public SerializableDict<LP, object> data;
}
