﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Hitting
{
    public static Batter currentBatter = InGameManager.currentBatter;

    public enum Hit
    {
        SINGLE, DOUBLE, TRIPLE, HOME_RUN
    }

    /// <summary>
    /// Conduct behaviours when a batter hit a ball in a common way.
    /// </summary>
    /// <param name="hit"></param>
    public static void AddHit(Hit hit, bool isITPHR = false)
    {
        //Clears count.
        AtPlate.ClearCount();

        if(InGameManager.isBottom)
        {
            InGameManager.game.homeScoreBoard.H++;
        }
        else
        {
            InGameManager.game.awayScoreBoard.H++;
        }

        if (hit == Hit.SINGLE)
        {
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.SIN);
            BaseRunning.AdvanceRunner(1);
            Debug.Log("SINGLE");
        }
        else if (hit == Hit.DOUBLE)
        {
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.DBL);
            BaseRunning.AdvanceRunner(2);
            Debug.Log("DOUBLE");
        }
        else if (hit == Hit.TRIPLE)
        {
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.TRP);
            BaseRunning.AdvanceRunner(3);
            Debug.Log("TRIPLE");
        }
        else if (hit == Hit.HOME_RUN)
        {
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.HR_BAT);
            if (isITPHR)
            {
                currentBatter.stats.SetStat(1, PlayerStatistics.PS.ITPHR);
            }
            BaseRunning.AdvanceRunner(4);
            Debug.Log("HOME RUN");
        }
    }
    /// <summary>
    /// Determines whether hit or not.
    /// </summary>
    /// <returns></returns>
    public static bool HitDetermine(bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0f, 1f) < 0.5f;
        }
        else
        {
            return true;
        }
    }
}
