using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TeamStatistics
{
    /// <summary>
    /// TeamPrefs
    /// </summary>
    public enum TS
    {
        WIN, LOSS,
        WIN_RATE, GAMES_BEHIND,
        RUNS_SCORED, RUNS_ALLOWED, RUN_DIFF,
        HOME_WIN, HOME_LOSS, HOME_RATE,
        AWAY_WIN, AWAY_LOSS, AWAY_RATE,
        LAST_WIN, LAST_LOSS, LAST_RATE, STREAK
    }

    public static List<string> TSString = new List<string>()
    {
        "Win", "Loss",
        "Win-Loss Rate", "Games Behind",
        "Runs Scored", "Runs Allowed", "Runs Differential",
        "Home Win", "Home Loss", "Home Win-Loss Rate",
        "Away Win", "Away Loss", "Away Win-Loss Rate",
        "Last 10 Games Win", "Last 10 Games Loss", "Last 10 Games Win-Loss Rate", "Streak"
    };

    public static List<string> TSStringShort = new List<string>()
    {
        "W", "L",
        "R", "GB",
        "RS", "RA", "RD",
        "HW", "HL", "HR",
        "AW", "AL", "AR",
        "LW", "LL", "LR", "ST"
    };

    public static List<TS> TSAverage = new List<TS>()
    {
        TS.WIN_RATE, TS.HOME_RATE, TS.AWAY_RATE, TS.LAST_RATE
    };

    public static List<TS> TSImmidiate = new List<TS>()
    {
        TS.GAMES_BEHIND, TS.RUN_DIFF, TS.STREAK
    };

    public TeamStatistics()
    {
        seasonStats = new SerializableDict<int, SerializableDict<TS, float>>();
        size = new SerializableDict<int, SerializableDict<TS, float>>();
        SetData(TS.WIN, 0);
        SetData(TS.LOSS, 0);
    }

    public float GetData(TS stat, int year = -1)
    {
        if (year == -1)
        {
            year = Values.date.Year;
        }

        if (seasonStats.d.ContainsKey(year))
        {
            if (seasonStats[year].d.ContainsKey(stat))
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

    public void SetData(TS stat, float value, int year = -1)
    {
        if (year == -1)
        {
            year = Values.date.Year;
        }

        if (seasonStats.d.ContainsKey(year))
        {
            if (seasonStats[year].d.ContainsKey(stat))
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
            seasonStats.d.Add(year, new SerializableDict<TS, float>());
            seasonStats[year].d.Add(stat, value);
            size.d.Add(year, new SerializableDict<TS, float>());
            size[year].d.Add(stat, 1);
        }
    }

    //Member functions
    public SerializableDict<int, SerializableDict<TS, float>> seasonStats;
    public SerializableDict<int, SerializableDict<TS, float>> size;
}
