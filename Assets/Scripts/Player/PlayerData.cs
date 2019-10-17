using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    //Static Data Member

    /// <summary>
    /// PlayerPrefs
    /// </summary>
    public enum PP
    {
        NAME, NUMBER, BIRTH_DATE, AGE, HEIGHT, WEIGHT, POSITION, IS_LEFT_HANDED,
        OVERALL,
        STRENGTH, CONTROL, CONSISTENCY, INTELLECT, POSITION_SKILLS,
        ARM_POWER, LEG_POWER, GRIP,
        FLEXIBILITY, AGILITY,
        CONCENTRATION, DURABILITY, MENTAL_STRENGTH, EFFICIENCY,
        DECISIVENESS, COMPREHENSION, CREATIVITY,
        PITCH, CONDITION, META_POSITION, FORM,
        POSITION_SKILLS_AVERAGE, CATCHER, FIRST_BASE_MAN, SECOND_BASE_MAN, THIRD_BASE_MAN, SHORT_STOP, LEFT_FIELDER, CENTER_FIELDER, RIGHT_FIELDER,
        STARTER_PITCHER, LONG_RELIEF_PITCHER, MIDDLE_RELIEF_PITCHER, SETUP_MAN, CLOSER_PITCHER,
        FOURSEAM, TWOSEAM, CUTTER, SPLITTER, SINKER, CHANGEUP, CIRCLE_CHANGEUP, FORKBALL, CURVEBALL, KNUCKLE_CURVE, TWELVE_SIX, SLURVE, SLIDER, SCREWBALL, KNUCKLEBALL
    }

    /// <summary>
    /// String value for Playerprefs.
    /// </summary>
    public static List<string> PPString = new List<string>()
    {
        "Name", "Number", "Birth Date", "Age", "Height", "Weight", "Position", "Left Handed",
        "Overall",
        "Strength", "Control", "Consistency", "Intellect", "Position Skills",
        "Arm Power", "Leg Power", "Grip",
        "Flexibility", "Agility",
        "Concentration", "Durability", "Mental Strength", "Efficiency",
        "Decisiveness", "Comprehension", "Creativity",
        "Pitch", "Condition", "Position", "Form",
        "Position Skills", "Catcher", "First Base Man", "Second Base Man", "Third Base Man", "Short Stop", "Left Fielder", "Center Fielder", "Right Fielder",
        "Starter Pitcher", "Long Relief Pitcher", "Middle Relief Pitcher", "Setup Man", "Closer Pitcher",
        "Four-seam Fastball", "Two-seam Fastball", "Cutter", "Splitter", "Sinker", "Changeup", "Circle Changeup", "Forkball", "Curveball", "Knuckle Curve", "Twelve-six Curve", "Slurve", "Slider", "Screwball", "Knuckleball"
    };

    public static List<string> PPStringShort = new List<string>()
    {
        "NME", "NUM", "DAT", "AGE", "HEI", "WEI", "POS", "LFT",
        "OVR",
        "STR", "CTR", "CON", "INT", "PSK",
        "ARM", "LEG", "GRP",
        "FLX", "AGI",
        "CCN", "DRB", "MEN", "EFF",
        "DES", "CPH", "CRT",
        "PIT", "CND", "POS", "FOM",
        "PSK", "C", "1B", "2B", "3B", "SS", "LF", "CF", "RF",
        "SP", "LRP", "MRP", "SM", "CP",
        "4FB", "2FB", "CUT", "SPL", "SIN", "CHG", "CCH", "FRK", "CUR", "KNC", "126", "SLV", "SLI", "SCR", "NUK"
    };

    /// <summary>
    /// Stat of Playerprefs.
    /// </summary>
    public static List<PP> statPrefs = new List<PP>()
    {
        PP.STRENGTH, PP.CONTROL, PP.CONSISTENCY, PP.INTELLECT, PP.POSITION_SKILLS, PP.PITCH
    };

    /// <summary>
    /// Stat for Roaster.
    /// </summary>
    public static List<PP> roasterPrefs = new List<PP>()
    {
        PP.OVERALL, PP.CONDITION
    };

    /// <summary>
    /// Stats for serializableDicts.
    /// </summary>
    public static Dictionary<PP, List<PP>> serializableDictPrefs = new Dictionary<PP, List<PP>>()
    {
        {PP.STRENGTH, new List<PP>() {
            PP.ARM_POWER, PP.LEG_POWER, PP.GRIP
        } },
        {PP.CONTROL, new List<PP>{
            PP.FLEXIBILITY, PP.AGILITY
        } },
        {PP.CONSISTENCY, new List<PP>() {
            PP.CONCENTRATION, PP.DURABILITY, PP.MENTAL_STRENGTH, PP.EFFICIENCY
        } },
        {PP.INTELLECT, new List<PP>() {
            PP.AGILITY, PP.DECISIVENESS, PP.COMPREHENSION, PP.CREATIVITY
        } },
        {PP.POSITION_SKILLS, new List<PP>() {
            PP.CATCHER, PP.FIRST_BASE_MAN, PP.SECOND_BASE_MAN, PP.THIRD_BASE_MAN, PP.SHORT_STOP, PP.LEFT_FIELDER, PP.CENTER_FIELDER, PP.RIGHT_FIELDER,
            PP.STARTER_PITCHER, PP.LONG_RELIEF_PITCHER, PP.MIDDLE_RELIEF_PITCHER, PP.SETUP_MAN, PP.CLOSER_PITCHER
        } },
        {PP.PITCH, new List<PP>() {
            PP.FOURSEAM, PP.TWOSEAM, PP.CUTTER, PP.SPLITTER, PP.SINKER, PP.CHANGEUP, PP.CIRCLE_CHANGEUP, PP.FORKBALL, PP.CURVEBALL, PP.KNUCKLE_CURVE, PP.TWELVE_SIX, PP.SLURVE, PP.SLIDER, PP.SCREWBALL, PP.KNUCKLEBALL
        } }
    };

    //Static Member Function

    /// <summary>
    /// Find meta pref of a pref.
    /// </summary>
    /// <param name="pref"></param>
    /// <returns></returns>
    public static PP FindSerializablePP(PP pref)
    {
        foreach (KeyValuePair<PP, List<PP>> pair in serializableDictPrefs)
        {
            if (pair.Value.Contains(pref))
            {
                return pair.Key;
            }
        }
        throw new NullReferenceException("There is no such value named as " + pref.ToString() + " in serializableDictPrefs dictionary.");
    }

    //Member Function

    /// <summary>
    /// Dataset of player.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="number"></param>
    /// <param name="height"></param>
    /// <param name="weight"></param>
    /// <param name="metaPosition">A wider position value. One metaposition can have multiple positions.</param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    public PlayerData(string name = "", int number = 0, int height = 0, int weight = 0, Player.MetaPosition metaPosition = Player.MetaPosition.STARTER_PITCHER, int year = 1994, int month = 4, int day = 17)
    {
        data = new SerializableDict<PP, object>();

        SerializableDict<PP, float> SetSerializableDictPP(PP pref)
        {
            SerializableDict<PP, float> sdpp = (SerializableDict<PP, float>)data.d[pref];
            foreach (PP p in serializableDictPrefs[pref])
            {
                sdpp.d.Add(
                    (PP)PPString.FindIndex(delegate (string cmp)
                    {
                        return cmp == PPString[(int)p];
                    }), 50);
            }

            return sdpp;
        }

        SerializableDict<PP, float> SetSerializableDictPP_Position(Player.MetaPosition meta)
        {
            SerializableDict<PP, float> sdpp = (SerializableDict<PP, float>)data.d[PP.POSITION_SKILLS];
            foreach (Player.Position p in Player.metaPosition[meta])
            {
                if (p == Player.Position.DESIGNATED_HITTER) continue;

                sdpp.d.Add(
                    (PP)PPString.FindIndex(delegate (string cmp)
                    {
                        return cmp == Player.positionString[(int)p];
                    }), 50);
            }

            return sdpp;
        }

        data.d.Add(PP.NAME, name);
        data.d.Add(PP.NUMBER, number);
        data.d.Add(PP.BIRTH_DATE, new DateTime(year, month, day));
        data.d.Add(PP.HEIGHT, height);
        data.d.Add(PP.WEIGHT, weight);
        data.d.Add(PP.META_POSITION, metaPosition);
        data.d.Add(PP.IS_LEFT_HANDED, true);
        data.d.Add(PP.CONDITION, 0f);
        data.d.Add(PP.STRENGTH, new SerializableDictPP());
        data.d.Add(PP.CONTROL, new SerializableDictPP());
        data.d.Add(PP.CONSISTENCY, new SerializableDictPP());
        data.d.Add(PP.INTELLECT, new SerializableDictPP());
        data.d.Add(PP.POSITION_SKILLS, new SerializableDictPP());

        SetSerializableDictPP(PP.STRENGTH);
        SetSerializableDictPP(PP.CONTROL);
        SetSerializableDictPP(PP.CONSISTENCY);
        SetSerializableDictPP(PP.INTELLECT);
        SetSerializableDictPP_Position(metaPosition);

        SetPositionMyMetaPosition(metaPosition);
    }

    /// <summary>
    /// Returns Playerpref data.
    /// </summary>
    /// <param name="pref"></param>
    /// <returns></returns>
    public dynamic GetData(PP pref)
    {
        if(serializableDictPrefs.ContainsKey(pref))
        {
            return ((SerializableDictPP)data.d[pref]).GetAverage();
        }
        else
        {
            try
            {
                return GetDictData(pref);
            } catch (Exception)
            {
                return Convert.ChangeType(data.d[pref], data.d[pref].GetType());
            }
        }
    }

    public float GetDictData(PP p)
    {
        foreach(KeyValuePair<PP, List<PP>> prefPair in serializableDictPrefs)
        {
            if(prefPair.Value.Contains(p))
            {
                return ((SerializableDictPP)data.d[prefPair.Key]).d[p];
            }
        }

        throw new ArgumentNullException();
    }

    /// <summary>
    /// Sets Playerpref data.
    /// </summary>
    /// <param name="pref"></param>
    /// <param name="obj"></param>
    public void SetData(PP pref, object obj)
    {
        data.d[pref] = obj;
    }

    public void SetDictData(PP pref, float f, PP p)
    {
        if(data.d.ContainsKey(pref))
        {
            if (((SerializableDictPP)data.d[pref]).d.ContainsKey(p))
            {
                ((SerializableDictPP)data.d[pref]).d[p] = f;
            }
            else
            {
                ((SerializableDictPP)data.d[pref]).d.Add(p, f);
            }
        }
        else
        {
            data.d.Add(pref, new SerializableDictPP());
            ((SerializableDictPP)data.d[pref]).d.Add(p, f);
        }
    }
    
    /// <summary>
    /// Sets position same as metaposition.
    /// </summary>
    /// <param name="metaPosition"></param>
    public void SetPositionMyMetaPosition(Player.MetaPosition metaPosition)
    {
        if (!data.d.ContainsKey(PP.POSITION))
        {
            data.d.Add(PP.POSITION, Player.Position.CATCHER);
        }

        if (metaPosition == Player.MetaPosition.CATCHER)
        {
            SetData(PP.POSITION, Player.Position.SUB_CATCHER);
        }
        else if (metaPosition == Player.MetaPosition.IN_FIELD_PLAYER)
        {
            SetData(PP.POSITION, Player.Position.SUB_IN_FIELD);
        }
        else if (metaPosition == Player.MetaPosition.OUT_FIELD_PLAYER)
        {
            SetData(PP.POSITION, Player.Position.SUB_OUT_FIELD);
        }
        else if (metaPosition == Player.MetaPosition.STARTER_PITCHER)
        {
            SetData(PP.POSITION, Player.Position.SUB_STARTER_PITCHER);
        }
        else if (metaPosition == Player.MetaPosition.RELIEF_PITCHER)
        {
            SetData(PP.POSITION, Player.Position.SUB_RELIEF_PITCHER);
        }
    }

    //Data Member

    /// <summary>
    /// Player data.
    /// </summary>
    public SerializableDict<PP, object> data;
}
