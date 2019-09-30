using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    //Public static variables
    public enum Form
    {
        TYPEA, TYPEB, TYPEC, TYPED,
        typea, typeb, typec, typed
    }

    //position
    /// <summary>
    /// Metaposition enum values.
    /// </summary>
    public enum MetaPosition
    {
        CATCHER, IN_FIELD_PLAYER, OUT_FIELD_PLAYER,
        STARTER_PITCHER, RELIEF_PITCHER
    }

    /// <summary>
    /// A group of positions handling by metaPosition.
    /// </summary>
    public static Dictionary<MetaPosition, List<Position>> metaPosition = new Dictionary<MetaPosition, List<Position>>()
    {
        { MetaPosition.CATCHER, new List<Position>() {Position.CATCHER} },
        { MetaPosition.IN_FIELD_PLAYER, new List<Position>() {Position.FIRST_BASE_MAN, Position.SECOND_BASE_MAN, Position.THIRD_BASE_MAN} },
        { MetaPosition.OUT_FIELD_PLAYER, new List<Position>() {Position.LEFT_FIELDER, Position.CENTER_FIELDER, Position.RIGHT_FIELDER} },
        { MetaPosition.STARTER_PITCHER, new List<Position>() {Position.STARTER_PITCHER, Position.LONG_RELIEF_PITCHER} },
        { MetaPosition.RELIEF_PITCHER, new List<Position>() {Position.MIDDLE_RELIEF_PITCHER, Position.SETUP_MAN, Position.CLOSER_PITCHER} }
    };

    public enum Position
    {
        CATCHER, FIRST_BASE_MAN, SECOND_BASE_MAN, THIRD_BASE_MAN, SHORT_STOP, LEFT_FIELDER, CENTER_FIELDER, RIGHT_FIELDER, DESIGNATED_HITTER,
        STARTER_PITCHER, LONG_RELIEF_PITCHER, MIDDLE_RELIEF_PITCHER, SETUP_MAN, CLOSER_PITCHER,
        SUB_CATCHER, SUB_IN_FIELD, SUB_OUT_FIELD, SUB_STARTER_PITCHER, SUB_RELIEF_PITCHER
    }

    /// <summary>
    /// Color sets for position.
    /// </summary>
    public static Color[] positionColor = {
        Colors.yellowDark, Colors.redDark, Colors.redDark, Colors.redDark, Colors.redDark, Colors.greenDark, Colors.greenDark, Colors.greenDark, Colors.purpleDark,
        Colors.skyblueDark, Colors.pinkDark, Colors.pinkDark, Colors.purpleDark, Colors.blueDark,
        Colors.yellowDarker, Colors.redDarker, Colors.greenDarker, Colors.skyblueDarker, Colors.pinkDarker
    };

    /// <summary>
    /// Short strings for position.
    /// </summary>
    public static List<string> positionStringShort = new List<string>() {
        "C", "1B", "2B", "3B", "SS", "LF", "CF", "RF", "DH",
        "SP", "LR", "MR", "SM", "CP",
        "C", "I", "O", "S", "R"
    };

    /// <summary>
    /// Strings for position.
    /// </summary>
    public static List<string> positionString = new List<string>() {
        "Catcher", "First Base Man", "Second Base Man", "Third Base Man", "Shortstop", "Left Fielder", "Center Fielder", "Right Fielder", "Designated Hitter",
        "Starter Pitcher", "Long Relief Pitcher", "Middle Relief Pitcher", "Setup Man", "Closer Pitcher",
        "Substitute Catcher", "Substitute In-Fielder", "Substitute Out-Fielder", "Substitute Starter", "Substitute Reliever"
    };

    /// <summary>
    /// Stat enum values.
    /// </summary>
    public enum StatGrade
    {
        POOR, NORMAL, GOOD, EXCELLENT, MASTER
    }

    /// <summary>
    /// Cutline sets for stat.
    /// </summary>
    public static List<int> statRange = new List<int>()
    {
        40, 70, 80, 90, 100
    };

    /// <summary>
    /// Color set for position.
    /// </summary>
    public static List<Color> statColor = new List<Color>()
    {
        Colors.red, Color.white, Colors.green, Colors.blue, Colors.pink
    };

    /// <summary>
    /// Alpha values for position.
    /// </summary>
    public static List<float> statAlpha = new List<float>()
    {
        0.3f, 0.75f, 0.7f, 0.9f, 1f
    };

    public static Color ColorPicker(float value, bool[] colorPicker = null)
    {
        for (int i = 0; i < statRange.Count; ++i)
        {
            if (Mathf.FloorToInt(value) < statRange[i])
            {
                if (colorPicker == null || (colorPicker != null && colorPicker[i]))
                {
                    return statColor[i];
                }
            }
        }
        return Color.white;
    }

    public static float AlphaPicker(float value, float customValue = -1f, bool[] colorPicker = null)
    {
        for (int i = 0; i < statRange.Count; ++i)
        {
            if (Mathf.FloorToInt(value) < statRange[i])
            {
                if (colorPicker == null || (colorPicker != null && colorPicker[i]))
                {
                    return customValue == -1f ? statAlpha[i] : customValue;
                }
            }
        }
        return 1f;
    }

    /// <summary>
    /// Total number of player created that year.
    /// </summary>
    public static Dictionary<int, int> playerCreated = new Dictionary<int, int>();

    /// <summary>
    /// Getter for PlayerCreated. Default year value is current year.
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    public static int GetPlayerCreated(int year = -1)
    {
        if (year == -1)
        {
            year = Values.date.Year;
        }

        if (playerCreated.ContainsKey(year))
        {
            return playerCreated[year];
        }
        else
        {
            throw new NullReferenceException("There is no such data matching year " + year.ToString() + ".");
        }
    }

    /// <summary>
    /// Adder for PlayerCreated. Default year value is current year.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="makeNewIfNone"></param>
    public static void AddPlayerCreated(int year = -1, bool makeNewIfNone = true)
    {
        if (year == -1)
        {
            year = Values.date.Year;
        }

        if (playerCreated.ContainsKey(year))
        {
            ++playerCreated[year];
        }
        else
        {
            if (makeNewIfNone)
            {
                playerCreated.Add(year, 1);
            }
            else
            {
                throw new NullReferenceException("There is no such data matching year " + year.ToString() + ".");
            }
        }
    }

    /// <summary>
    /// Subtracter for PlayerCreated. Default year value is current year.
    /// </summary>
    /// <param name="year"></param>
    public static void SubtractPlayerCreated(int year = -1)
    {
        if (year == -1)
        {
            year = Values.date.Year;
        }

        if (playerCreated.ContainsKey(year))
        {
            --playerCreated[year];
        }
        else
        {
            throw new NullReferenceException("There is no such data matching year " + year.ToString() + ".");
        }
    }


    //initializers
    public Player()
    {
        ;
    }

    public Player(int _index, PlayerData _playerData, PlayerStatistics _stats, Training.Train _train)
    {
        index = _index;

        playerData = new PlayerData(_playerData.GetData(PlayerData.PP.NAME));

        foreach (KeyValuePair<PlayerData.PP, object> pd in _playerData.data.d)
        {
            playerData.data.d[pd.Key] = pd.Value;
        }

        playerData.SetData(PlayerData.PP.AGE, GetAge());

        stats = new PlayerStatistics(1);

        foreach (KeyValuePair<DateTime, SerializableDict<PlayerStatistics.PS, float>> ps in _stats.statistics.d)
        {
            foreach (KeyValuePair<PlayerStatistics.PS, float> data in ps.Value.d)
            {
                stats.SetStat(data.Value, data.Key, ps.Key);
            }
        }

        train = _train;
        order = -1;

        //Debug.Log("STAT:   " + stats.statistics.d.Count);
        //Debug.Log("SEASON: " + stats.seasonStats.d.Count);

        //foreach (KeyValuePair<int, SerializableDict<PlayerStatistics.PS, float>> ps in stats.seasonStats.d)
        //{
        //    Debug.Log(ps.Key + " G: " + ps.Value[PlayerStatistics.PS.G]);
        //}

        //Debug.Log(stats.seasonStats[Values.date.Year][PlayerStatistics.PS.G]);

        //Debug.Log(stats.GetStat(PlayerStatistics.PS.G, new DateTime(2019, 1, 1)));

        CalcStats();
    }

    //member fucntions
    /// <summary>
    /// Calculates stats.
    /// </summary>
    public void CalcStats()
    {
        //Some calculations for adjusting stats by body stats.
    }

    /// <summary>
    /// Returns age.
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetAge()
    {
        return Values.date - playerData.GetData(PlayerData.PP.BIRTH_DATE);
    }

    /// <summary>
    /// Returns overall.
    /// </summary>
    /// <returns></returns>
    public float GetOverall()
    {
        float sum = 0;
        foreach (float value in finalStats.d.Values)
        {
            sum += value;
        }
        return sum / finalStats.d.Count;
    }

    /// <summary>
    /// Get Training data modified by player stats.
    /// </summary>
    /// <param name="train"></param>
    /// <returns></returns>
    public Training GetTraining(Training.Train train)
    {
        Training training = Trainings.trainings[train].DeepCopy();
        foreach(PlayerData.PP pref in training.modifier.Keys.ToList())
        {
            training.modifier[pref] += 0.003f * playerData.GetDictData(PlayerData.PP.COMPREHENSION) / 100;
        }

        return training;
    }

    /// <summary>
    /// Calculates every training modifiers.
    /// </summary>
    public void CalcTraining()
    {
        foreach(KeyValuePair<PlayerData.PP, float> pair in GetTraining(train).modifier)
        {
            float random = UnityEngine.Random.Range(0f, 1f);
            if(random < pair.Value)
            {
                float oldValue = playerData.GetDictData(PlayerData.FindSerializablePP(pair.Key));
                playerData.SetDictData(PlayerData.FindSerializablePP(pair.Key), oldValue + 1, pair.Key);
            }
        }
    }

    //data members
    public int index;
    public bool isStartingMember;
    public bool isSubstitute;
    public PlayerData playerData;
    public SerializableDict<string, float> finalStats;
    public PlayerStatistics stats;
    public Training.Train train;
    public int order;

    public Form form = Form.typea;
}