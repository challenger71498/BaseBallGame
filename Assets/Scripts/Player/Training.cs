using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Training
{
    /// <summary>
    /// Training enum values.
    /// </summary>
    public enum Train
    {
        WEIGHT,
        STRETCHING,
        RUNNING,
        FUNGO
    }

    public static List<string> trainString = new List<string>()
    {
        "Weight",
        "Stretching",
        "Running",
        "Fungo"
    };

    public static List<string> trainDescription = new List<string>() {
        "Weight training increases player\'s total strength, such as arm power, leg power, and grip.",
        "Stretching increases player\'s flexibility of body. And also a little bit of durability, too.",
        "Running increases player\'s consistency, especially durability. It also slightly increases leg power.",
        "Fungo increases player\'s intelligence of baseball tactics. Agility and a bit of comprehension, and creativity will be affected."
    };

    public static List<Train> trainPitcher = new List<Train>()
    {

    };

    public static List<Train> trainBatter = new List<Train>()
    {

    };
    
    public static List<Train> trainAll = new List<Train>()
    {
        Train.WEIGHT, Train.STRETCHING, Train.RUNNING, Train.FUNGO
    };
    

    /// <summary>
    /// Constructor
    /// </summary>
    public Training(Train _train, bool selected = false)
    {
        train = _train;
        currentlySelected = selected;

        modifier = new Dictionary<PlayerData.PP, float>();
    }

    public Train GetTrain()
    {
        return train;
    }

    public float GetModifier(PlayerData.PP pref)
    {
        if(modifier.ContainsKey(pref))
        {
            return modifier[pref];
        }
        else
        {
            throw new NullReferenceException("There is no such value named as " + pref.ToString() + ".");
        }
    }

    public Training SetModifier(PlayerData.PP pref, float value, bool makeNewIfNone = true)
    {
        if(value < 0 || 1 < value)
        {
            throw new ArgumentOutOfRangeException("Value should be between 0 to 1.");
        }

        if(modifier.ContainsKey(pref))
        {
            modifier[pref] = value;
            return this;
        }
        else
        {
            if(makeNewIfNone)
            {
                modifier.Add(pref, value);
                return this;
            }
            else
            {
                throw new NullReferenceException("There is no such value named as " + pref.ToString() + ".");
            }
        }
    }

    public Training DeepCopy()
    {
        Training training = new Training(train);
        foreach(KeyValuePair<PlayerData.PP, float> pair in modifier)
        {
            training.modifier.Add(pair.Key, pair.Value);
        }

        return training;
    }

    public Train train;
    public Dictionary<PlayerData.PP, float> modifier;
    public bool currentlySelected;
}

public class Trainings
{
    public static Dictionary<Training.Train, Training> trainings = new Dictionary<Training.Train, Training>()
    {
        {
            Training.Train.WEIGHT, new Training(Training.Train.WEIGHT)
            .SetModifier(PlayerData.PP.ARM_POWER, 0.0053f)
            .SetModifier(PlayerData.PP.LEG_POWER, 0.0048f)
            .SetModifier(PlayerData.PP.GRIP, 0.0036f)
        },
        {
            Training.Train.FUNGO, new Training(Training.Train.FUNGO)
            .SetModifier(PlayerData.PP.AGILITY, 0.0068f)
            .SetModifier(PlayerData.PP.COMPREHENSION, 0.0027f)
            .SetModifier(PlayerData.PP.CREATIVITY, 0.0013f)
        },
        {
            Training.Train.RUNNING, new Training(Training.Train.RUNNING)
            .SetModifier(PlayerData.PP.LEG_POWER, 0.0036f)
            .SetModifier(PlayerData.PP.DURABILITY, 0.0057f)
        },
        {
            Training.Train.STRETCHING, new Training(Training.Train.STRETCHING)
            .SetModifier(PlayerData.PP.FLEXIBILITY, 0.0043f)
            .SetModifier(PlayerData.PP.DURABILITY, 0.0018f)
        }
    };
}