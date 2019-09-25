using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeagueStatistics
{
    public enum LS
    {
        NUMBER
    }

    public static List<LS> TSAverage = new List<LS>()
    {

    };

    public static List<LS> TSImmidiate = new List<LS>()
    {

    };

    public List<string> LSString = new List<string>()
    {
        "Number"
    };

    public LeagueStatistics()
    {
        seasonStats = new SerializableDict<int, SerializableDict<LS, float>>();
    }

    public float GetData(LS stat, int year = -1)
    {
        if(year == -1)
        {
            year = Values.date.Year;
        }

        if (seasonStats.d.ContainsKey(year))
        {
            if(seasonStats[year].d.ContainsKey(stat))
            {
                return seasonStats[year][stat];
            }
            else
            {
                throw new NullReferenceException("There is no such value named as " + stat.ToString() + ".");
            }
        }
        else
        {
            throw new NullReferenceException("There is no such value in year " + year.ToString() + ".");
        }
    }

    public void SetData(LS stat, float value, int year = -1)
    {
        if(year == -1)
        {
            year = Values.date.Year;
        }

        if (seasonStats.d.ContainsKey(year))
        {
            if(seasonStats[year].d.ContainsKey(stat))
            {
                if (TSAverage.Contains(stat))
                {
                    seasonStats[year][stat] = seasonStats[year][stat] * size[year][stat] + value / (size[year][stat] + 1);
                }
                else if (TSImmidiate.Contains(stat))
                {
                    seasonStats[year][stat] = value;
                }
                else
                {
                    seasonStats[year][stat] += value;
                }
                size[year][stat] += 1;
            }
            else
            {
                seasonStats[year].d.Add(stat, value);
                size[year].d.Add(stat, 1);
            }
        }
        else
        {
            seasonStats.d.Add(year, new SerializableDict<LS, float>());
            seasonStats[year].d.Add(stat, value);
            size.d.Add(year, new SerializableDict<LS, float>());
            size[year].d.Add(stat, 1);
        }
    }

    public SerializableDict<int, SerializableDict<LS, float>> seasonStats;
    public SerializableDict<int, SerializableDict<LS, float>> size;
}
