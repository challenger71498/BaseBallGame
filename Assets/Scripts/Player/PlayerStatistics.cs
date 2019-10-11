using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatistics
{
    //public static vairables
    /// <summary>
    /// Player Statistics.
    /// </summary>
    public enum PS
    {
        G, RAT,

        W, L, WLP,
        ERA, GS_PIT, GF, CG, SHO, HLD, SVO, SV, BS, IP,
        H_PIT, RA, ER, HR_PIT, BB_PIT, IBB_PIT, HB, K_PIT, BK, BF, WHIP, GB_PIT, FB_PIT,
        BB_9, K_9, K_BB, H_9, HR_9, IP_GS, GO_AO_PIT,
        GIDP, GIDPO, GIR, IR, IRA, PIT, QS, WPS, WP,
        CERA, DICE, ERAP, FIP, LOBP, OBA, PC_ST, PFR, PNERD, QOP, SIERA,

        AB, H_BAT, AVG, OBP, SLG, OPS, SIN, DBL, TRP, HR_BAT, GS_BAT, ITPHR, RBI,
        PA, BB_BAT, HBP, IBB_BAT, K_BAT, BB_K, GB_BAT, FB_BAT,
        SBA, SB, CS, SBP, FC, DI,
        R, GDP, LOB, SF, SH, TB, TOB, XBH,
        AB_HR, BABIP, EQA, GO_AO_BAT, GPA, HR_H, ISO, PA_SO, RC, RP, RISP, TA, UBR
    }

    public static List<PS> lowerBetter = new List<PS>()
    {
        PS.L,
        PS.ERA, PS.BS,
        PS.H_PIT, PS.RA, PS.ER, PS.HR_PIT, PS.BB_PIT, PS.IBB_PIT, PS.HB, PS.BK, PS.WHIP, PS.GB_PIT, PS.FB_PIT,
        PS.BB_9, PS.H_9, PS.HR_9, PS.IP_GS,
        PS.IRA, PS.PIT, PS.WP,
        PS.CERA, PS.DICE, PS.ERAP, PS.FIP, PS.OBA, PS.PC_ST, PS.SIERA,

        PS.BB_BAT, PS.IBB_BAT, PS.K_BAT, PS.GB_BAT, PS.FB_BAT,
        PS.CS,
        PS.GDP, PS.LOB,
        PS.AB_HR, PS.RISP
    };

    public static List<PS> generalPS = new List<PS>()
    {
        PS.G, PS.RAT
    };

    public static List<PS> pitcherPS = new List<PS>()
    {
        PS.W, PS.L, PS.WLP,
        PS.ERA, PS.GS_PIT, PS.GF, PS.CG, PS.SHO, PS.HLD, PS.SVO, PS.SV, PS.BS, PS.IP,
        PS.H_PIT, PS.RA, PS.ER, PS.HR_PIT, PS.BB_PIT, PS.IBB_PIT, PS.HB, PS.K_PIT, PS.BK, PS.BF, PS.WHIP, PS.GB_PIT, PS.FB_PIT,
        PS.BB_9, PS.K_9, PS.K_BB, PS.H_9, PS.HR_9, PS.IP_GS, PS.GO_AO_PIT,
        PS.GIDP, PS.GIDPO, PS.GIR, PS.IR, PS.IRA, PS.PIT, PS.QS, PS.WPS, PS.WP,
        PS.CERA, PS.DICE, PS.ERAP, PS.FIP, PS.LOBP, PS.OBA, PS.PC_ST, PS.PFR, PS.PNERD, PS.QOP, PS.SIERA
    };

    public static List<PS> batterPS = new List<PS>()
    {
        PS.AB, PS.H_BAT, PS.AVG, PS.OBP, PS.SLG, PS.OPS, PS.SIN, PS.DBL, PS.TRP, PS.HR_BAT, PS.GS_BAT, PS.ITPHR, PS.RBI,
        PS.PA, PS.BB_BAT, PS.HBP, PS.IBB_BAT, PS.K_BAT, PS.BB_K, PS.GB_BAT, PS.FB_BAT,
        PS.SBA, PS.SB, PS.CS, PS.SBP, PS.FC, PS.DI,
        PS.R, PS.GDP, PS.LOB, PS.SF, PS.SH, PS.TB, PS.TOB, PS.XBH,
        PS.AB_HR, PS.BABIP, PS.EQA, PS.GO_AO_BAT, PS.GPA, PS.HR_H, PS.ISO, PS.PA_SO, PS.RC, PS.RP, PS.RISP, PS.TA, PS.UBR
    };

    public static List<PS> accumulativePS = new List<PS>()
    {
        PS.G,

        PS.W, PS.L, PS.WLP,
        PS.GS_PIT, PS.GF, PS.CG, PS.SHO, PS.HLD, PS.SVO, PS.SV, PS.BS, PS.IP,
        PS.H_PIT, PS.RA, PS.ER, PS.HR_PIT, PS.BB_PIT, PS.IBB_PIT, PS.HB, PS.K_PIT, PS.BK, PS.BF, PS.GB_PIT, PS.FB_PIT,
        PS.GIDP, PS.GIDPO, PS.GIR, PS.IR, PS.IRA, PS.PIT, PS.QS, PS.WPS, PS.WP,

        PS.AB, PS.H_BAT, PS.SIN, PS.DBL, PS.TRP, PS.HR_BAT, PS.GS_BAT, PS.ITPHR, PS.RBI,
        PS.PA, PS.BB_BAT, PS.HBP, PS.IBB_BAT, PS.K_BAT, PS.GB_BAT, PS.FB_BAT,
        PS.SBA, PS.SB, PS.CS, PS.FC, PS.DI,
        PS.R, PS.GDP, PS.LOB, PS.SF, PS.SH, PS.TB, PS.TOB, PS.XBH
    };

    public static List<PS> averagePS = new List<PS>()
    {
        PS.RAT,

        PS.ERA, PS.WHIP, PS.BB_9, PS.K_9, PS.K_BB, PS.H_9, PS.HR_9, PS.IP_GS, PS.GO_AO_PIT,
        PS.CERA, PS.DICE, PS.ERAP, PS.FIP, PS.LOBP, PS.OBA, PS.PC_ST, PS.PFR, PS.PNERD, PS.QOP, PS.SIERA,

        PS.AVG, PS.OBP, PS.SLG, PS.OPS, PS.BB_K, PS.SBP,
        PS.AB_HR, PS.BABIP, PS.EQA, PS.GO_AO_BAT, PS.GPA, PS.HR_H, PS.ISO, PS.PA_SO, PS.RC, PS.RP, PS.RISP, PS.TA, PS.UBR
    };

    /// <summary>
    /// String values for Player Statistics.
    /// </summary>
    public static List<string> PSString = new List<string>()
    {
        "Game", "Rating",

        "Win", "Loss", "Win-Loss Rate",
        "Earned Run Average", "Starts", "Games Finished", "Complete Game", "Shutout", "Hold", "Save Oppertunity", "Save", "Blown Save", "Innings Pitched",
        "Hits Allowed", "Run Average", "Earned Run", "Home Runs Allowed", "Walks Allowed", "Intentional Walks Allowed", "Hit Batsman", "Strikeout", "Balk", "Total Batters Faced", "Walks and Hits per Inning Pitched", "Ground Ball", "Fly Ball",
        "Double Plays Induced", "Double Play Oppertunities", "Games in Relief", "Inherited Runners", "Inherited Runners Allowed", "Pitches Thrown", "Quality Start", "W+S", "Wild Pitches",
        "Component ERA", "Defense-Independent ERA", "Adjusted ERA+", "Fielding Independent Pitching", "Left-On-Base Percentage", "Opponents Batting Average", "Pitch Count per Strike", "Power Finesse Ratio", "Pitcher's NERD", "Quality of Pitch", "Skill-Interactive Earned Run Average",

        "At Bat", "Hit", "Batting Average", "On-Base Percentage", "Slugging Average", "On-Base Plus Slugging", "Single", "Double", "Triple", "Home Run", "Grand Slam", "Inside-The-Park Home Run", "Run Batted in",
        "Plate Appearance", "Walk", "Hit by Pitch", "Intentional Walk", "Strikeout", "Walk-To-Strikeout Ratio", "Ground Ball", "Fly Ball",
        "Stolen Base Attempts", "Stolen Bases", "Caught Stealing", "Stolen Base Percentage", "Fielder's Choice", "Defensive Indifference",
        "Runs Scored", "Ground into Double Play", "Left on Base", "Sacrifice Fly", "Sacrifice Hit", "Total Bases", "Times on Base", "Extra Base Hits",
        "At Bats per Home Run", "Batting Average on Balls in Play", "Equivalent Average", "Ground Ball Fly Ball Ratio", "Gross Production Average", "Home Runs per Hit", "Isolated Power", "Plate Appearances per Strikeout", "Runs Created", "Runs Produced", "Runner in Scoring Position", "Total Average", "Ultimate Base Running"
    };

    /// <summary>
    /// Short string values for Player Statistics.
    /// </summary>
    public static List<string> PSStringShort = new List<string>()
    {
        "G", "RAT.",

        "W", "L", "WLP",
        "ERA", "GS", "GF", "CG", "SHO", "HLD", "SVO", "SV", "BS", "IP",
        "H", "RA", "ER", "HR", "BB", "IBB", "HB", "K", "BK", "BF", "WHIP", "GB", "FB",
        "BB/9", "K/9", "K/BB", "H/9", "HR/9", "IP/GS", "GO/AO",
        "GIDP", "GIDPO", "GIR", "IR", "IRA", "PIT", "QS", "WPS", "WP",
        "CERA", "DICE", "ERAP", "FIP", "LOBP", "OBA", "PC-ST", "PFR", "PNERD", "QOP", "SIERA",

        "AB", "H", "AVG", "OBP", "SLG", "OPS", "SIN", "DBL", "TRP", "HR", "GS", "ITPHR", "RBI",
        "PA", "BB", "HBP", "IBB", "K", "BB/K", "GB", "FB",
        "SBA", "SB", "CS", "SBP", "FC", "DI",
        "R", "GDP", "LOB", "SF", "SH", "TB", "TOB", "XBH",
        "AB/HR", "BABIP", "EQA", "GO/AO", "GPA", "HR/H", "ISO", "PA/SO", "RC", "RP", "RISP", "TA", "UBR"
    };

    public static SerializableDict<int, SerializableDict<PS, int>> statisticCreated = new SerializableDict<int, SerializableDict<PS, int>>();

    public static void AddStatNumber(PS stat, int year = -1)
    {
        if (year == -1)
        {
            year = Values.date.Year;
        }

        if(statisticCreated.d.ContainsKey(year))
        {
            if (statisticCreated[year].d.ContainsKey(stat))
            {
                ++statisticCreated[year][stat];
            }
            else
            {
                statisticCreated[year].d.Add(stat, 1);
            }
        }
        else
        {
            statisticCreated.d.Add(year, new SerializableDict<PS, int>());
            statisticCreated[year].d.Add(stat, 1);
        }
    }

    /// <summary>
    /// A dictionary for season average data.
    /// </summary>
    public static SerializableDict<int, SerializableDict<PS, float>> statisticSum = new SerializableDict<int, SerializableDict<PS, float>>();

    //public static functions
    /// <summary>
    /// Returns an average value of a stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    public static float StatAverage(PS stat, int year, int rounding = 3)
    {
        if (statisticSum.d.ContainsKey(year))
        {
            if (statisticSum[year].d.ContainsKey(stat))
            {
                if(statisticCreated.d.ContainsKey(year))
                {
                    if(statisticCreated[year].d.ContainsKey(stat))
                    {
                        if(accumulativePS.Contains(stat))
                        {
                            return (int)(statisticSum[year][stat] / statisticCreated[year][stat] * Mathf.Pow(10, rounding)) / Mathf.Pow(10, rounding);
                        }
                        else
                        {
                            return (int)(statisticSum[year][stat] * Mathf.Pow(10, rounding)) / Mathf.Pow(10, rounding);
                        }
                    }
                    else
                    {
                        throw new NullReferenceException("There is no such value name as " + stat.ToString() + " in statisticCreated dictionary.");
                    }
                }
                else
                {
                    throw new NullReferenceException("There is no such value maching year " + year.ToString() + " in statisticCreated dictionary.");
                }
                
            }

            else
            {
                throw new NullReferenceException("There is no such value name as " + stat.ToString() + ".");
            }
        }
        else
        {
            throw new NullReferenceException("There is no such value matching year " + year.ToString() + ".");
        }
    }

    //constructor
    public PlayerStatistics()
    {
        ;
    }

    public PlayerStatistics(int id)
    {
        seasonStats = new SerializableDict<int, SerializableDict<PS, float>>();
        statistics = new SerializableDict<DateTime, SerializableDict<PS, float>>();
    }

    //member functions
    public float GetSeason(PS stat, int year = -1, int rounding = 3)
    {
        if (year == -1)
        {
            year = Values.date.Year;
        }

        if(seasonStats.d.ContainsKey(year))
        {
            if(seasonStats[year].d.ContainsKey(stat))
            {
                return (int)(seasonStats[year][stat] * Mathf.Pow(10, rounding)) / Mathf.Pow(10, rounding);
            }
            else
            {
                throw new NullReferenceException("There is no such data named as " + stat.ToString() + ".");
            }
        }
        else {
            throw new NullReferenceException("There is no such data maching year " + year.ToString() + ".");
        }
    }

    public bool FIndStat(PS stat, DateTime date = default)
    {
        if (date == default)
        {
            date = Values.date;
        }

        if (statistics.d.ContainsKey(date))
        {
            if (statistics[date].d.ContainsKey(stat))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public float GetStat(PS stat, DateTime date = default, int rounding = 3)
    {
        if (date == default)
        {
            date = Values.date;
        }

        if (statistics.d.ContainsKey(date))
        {
            if (statistics[date].d.ContainsKey(stat))
            {
                return (int)(statistics[date][stat] * Mathf.Pow(10, rounding)) / Mathf.Pow(10, rounding);
            }
            else
            {
                throw new NullReferenceException("Exception throwed while trying to set a value: There is no such data named as " + stat.ToString() + ".");
            }
        }
        else
        {
            throw new NullReferenceException("Exception throwed while trying to set a value: There is no such matching with date " + date.ToString() + ".");
        }
    }

    public void SetStat(float value, PS stat, DateTime date = default, bool makeNewIfNull = true)
    {
        void AddAtSeason()
        {
            if (accumulativePS.Contains(stat))
            {
                if (seasonStats.d.ContainsKey(date.Year))
                {
                    if (seasonStats[date.Year].d.ContainsKey(stat))
                    {
                        seasonStats[date.Year][stat] += value;
                    }
                    else
                    {
                        AddStatNumber(stat, date.Year);
                        seasonStats[date.Year].d.Add(stat, value);
                    }
                }
                else
                {
                    seasonStats.d.Add(date.Year, new SerializableDict<PS, float>());
                    AddStatNumber(stat, date.Year);
                    seasonStats[date.Year].d.Add(stat, value);
                }
            }
            else if (averagePS.Contains(stat))
            {
                if (seasonStats.d.ContainsKey(date.Year))
                {
                    if (seasonStats[date.Year].d.ContainsKey(stat))
                    {
                        seasonStats[date.Year][stat] = (seasonStats[date.Year][stat] * (seasonStats[date.Year][PS.G] - 1) + value) / seasonStats[date.Year][PS.G];
                    }
                    else
                    {
                        AddStatNumber(stat, date.Year);
                        seasonStats[date.Year].d.Add(stat, value);
                    }
                }
                else
                {
                    seasonStats.d.Add(date.Year, new SerializableDict<PS, float>());
                    AddStatNumber(stat, date.Year);
                    seasonStats[date.Year].d.Add(stat, value);
                }
            }
        }

        void AddAtSum()
        {
            if(statisticSum.d.ContainsKey(date.Year))
            {
                if(statisticSum[date.Year].d.ContainsKey(stat))
                {
                    if (accumulativePS.Contains(stat))
                    {
                        statisticSum[date.Year][stat] += value;
                    }
                    else if (averagePS.Contains(stat))
                    {
                        statisticSum[date.Year][stat] = (statisticSum[date.Year][stat] * (statisticSum[date.Year][PS.G] - 1) + value) / statisticSum[date.Year][PS.G];
                    }
                }

                else
                {
                    statisticSum[date.Year].d.Add(stat, value);
                }
            }
            else
            {
                statisticSum.d.Add(date.Year, new SerializableDict<PS, float>());
                statisticSum[date.Year].d.Add(stat, value);
            }
        }

        if (date == default)
        {
            date = Values.date;
        }

        if (statistics.d.ContainsKey(date))
        {
            if (statistics[date].d.ContainsKey(stat))
            {
                if(accumulativePS.Contains(stat))
                {
                    statistics[date][stat] += value;
                }
                else if(averagePS.Contains(stat))
                {
                    statistics[date][stat] = value;
                }
                AddAtSeason();
                AddAtSum();
            }
            else
            {
                if (makeNewIfNull)
                {
                    statistics[date].d.Add(stat, value);
                    AddAtSeason();
                    AddAtSum();
                }
                else
                {
                    throw new NullReferenceException("Exception throwed while trying to set a value: There is no such data named as " + stat.ToString() + ".");
                }
            }
        }
        else
        {
            if (makeNewIfNull)
            {
                statistics.d.Add(date, new SerializableDict<PS, float>());
                statistics[date].d.Add(stat, value);
                AddAtSeason();
                AddAtSum();
            }
            else
            {
                throw new NullReferenceException("Exception throwed while trying to set a value: There is no such matching with date " + date.ToString() + ".");
            }
        }
    }

    //data members
    public SerializableDict<int, SerializableDict<PS, float>> seasonStats;
    public SerializableDict<DateTime, SerializableDict<PS, float>> statistics;
}