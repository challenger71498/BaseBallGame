using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pitcher : Player
{

    public enum Pitch
    {
        FOURSEAM, TWOSEAM, CUTTER, SPLITTER, SINKER, CHANGEUP, CIRCLE_CHANGEUP, FORKBALL, CURVEBALL, KNUCKLE_CURVE, TWELVE_SIX, SLURVE, SLIDER, SCREWBALL, KNUCKLEBALL
    };

    public static string[] PitchString =
    {
        "Four-seam Fastball", "Two-seam Fastball", "Cutter", "Splitter", "Sinker", "Changeup", "Circle Changeup", "Forkball", "Curveball", "Knuckle Curve", "Twelve-six Curve", "Slurve", "Slider", "Screwball", "Knuckleball"
    };

    public static string[] PitchStringShort =
    {
        "4SM", "2SM", "CUT", "SPL", "SNK", "CHG", "CIR", "FKB", "CRV", "KNC", "TSC", "SLV", "SLI", "SCR", "NUK"
    };

    //initializers
    public Pitcher(int _index, PlayerData _playerData, PlayerStatistics _stats, Training.Train _train, Dictionary<Pitch, float> _pitches = null, float first = 0, float second = 0, float third = 0, float fourth = 0)
        : base(_index, _playerData, _stats, _train)
    {
        if (_pitches != null)
        {
            pitches = new SerializableDict<Pitch, float>();
            foreach (KeyValuePair<Pitch, float> pitch in _pitches)
            {
                pitches.d.Add(pitch.Key, pitch.Value);
            }
        }
        else
        {
            pitches = new SerializableDict<Pitch, float>()
            {
                d = {
                    { Pitch.FOURSEAM, 50 },
                    { Pitch.CURVEBALL, 50 }
                }
            };
        }

        finalStats = new SerializableDict<string, float>
        {
            d = new Dictionary<string, float>() {
                {"STR", first},
                {"CTR", second},
                {"CON", third},
                {"INT", fourth}
            }
        };

        playerData.SetData(PlayerData.PP.OVERALL, GetOverall());

        CalcFinalStats();
    }

    //member functions
    public void CalcFinalStats()
    {
        //Some functions for final stat output;
    }

    public float GetPitchAverage()
    {
        float sum = 0;

        foreach(float pitchValue in pitches.d.Values)
        {
            sum += pitchValue;
        }

        return sum / pitches.d.Count;
    }

    //data members
    public SerializableDict<Pitch, float> pitches;
}