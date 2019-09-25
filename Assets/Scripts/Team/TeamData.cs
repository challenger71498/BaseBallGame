using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TeamData
{
    public enum TP
    {
        NAME, CITY_NAME, TEAM_NAME, SHORT_NAME,
        BIRTH_DATE, EMBLEM
    }

    public static List<string> TPString = new List<string>()
    {
        "Name", "City Name", "Team Name",
        "Established Year", "Emblem"
    };

    public TeamData(string _cityName, string _teamName, string _shortName, int _year = 1990, int _month = 1, int _day = 1)
    {
        data = new SerializableDict<TP, object>();

        data.d.Add(TP.NAME, _cityName + " " + _teamName);
        data.d.Add(TP.CITY_NAME, _cityName);
        data.d.Add(TP.TEAM_NAME, _teamName);
        data.d.Add(TP.SHORT_NAME, _shortName);
        data.d.Add(TP.BIRTH_DATE, new DateTime(_year, _month, _day));
    }

    public dynamic GetData(TP pref)
    {
        if(data.d.ContainsKey(pref))
        {
            return Convert.ChangeType(data[pref], data[pref].GetType());
        }
        else
        {
            throw new NullReferenceException("There is no such value named as " + pref.ToString() + " in data.");
        }
    }

    public void SetData(TP pref, object obj)
    {
        if(data.d.ContainsKey(pref))
        {
            data[pref] = obj;
        }
        else
        {
            data.d.Add(pref, obj);
        }
    }

    public SerializableDict<TP, object> data;
}
