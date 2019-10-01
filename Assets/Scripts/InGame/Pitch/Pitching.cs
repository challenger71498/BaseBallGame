using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pitching
{
    /// <summary>
    /// Throws pitch. Returns whether the pitch is wild or not.
    /// </summary>
    /// <returns></returns>
    public static void Pitch(out bool wildPitch, out bool hitByPitch, bool isRandom = false)
    {
        if (isRandom)
        {
            float randomFloat = UnityEngine.Random.Range(0f, 1f);
            if (randomFloat < 0.03f)
            {
                wildPitch = true;
                hitByPitch = false;
            }
            else if (0.03f <= randomFloat && randomFloat < 0.08f)
            {
                wildPitch = false;
                hitByPitch = true;
            }
            else
            {
                wildPitch = false;
                hitByPitch = false;
            }
        }
        else
        {
            wildPitch = false;
            hitByPitch = false;
        }
    }

    /// <summary>
    /// Determines wheter a ball is in strike zone or not.
    /// </summary>
    /// <returns></returns>
    public static bool InStrikeZoneDetermine(bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0f, 1f) < 0.6f;
        }
        else
        {
            return true;
        }
    }
}
