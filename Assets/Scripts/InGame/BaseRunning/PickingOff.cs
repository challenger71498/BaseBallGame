using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PickingOff
{
    public static Batter[] runnerInBases = InGameManager.runnerInBases;

    /// <summary>
    /// Determines whether pickoff or not.
    /// </summary>
    /// <returns></returns>
    public static bool PickOffDetermine(out int whichBase, bool isRandom = false)
    {
        if (isRandom)
        {
            for (int i = 1; i <= 3; ++i)
            {
                if (runnerInBases[i] != null)
                {
                    whichBase = i;
                    return false;
                }
            }
            whichBase = -1;
            return false;
        }
        else
        {
            whichBase = -1;
            return false;
        }
    }

    /// <summary>
    /// Throws pickoff ball.
    /// </summary>
    public static void PickOff(int whichBase, bool isRandom = false)
    {
        if (isRandom)
        {

        }
    }
}
