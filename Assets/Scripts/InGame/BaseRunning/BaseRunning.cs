using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BaseRunning
{
    /// <summary>
    /// Determines whether attempt to steal base or not.
    /// </summary>
    /// <param name="batter"></param>
    /// <returns></returns>
    public static bool BaseStealDetermine(Batter batter, bool isRandom = false)
    {
        if (isRandom)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Advances runners specified amount of.
    /// </summary>
    /// <param name="amount"></param>
    public static void AdvanceRunner(int amount, bool includeBatter = true)
    {
        void Advance(int runnerBase)
        {
            if (runnerBase == 3)
            {
                RunnerToHomePlate(InGameManager.runnerInBases[3]);
            }
            else if (InGameManager.runnerInBases[runnerBase + 1] != null)
            {
                Advance(runnerBase + 1);
            }

            InGameManager.runnerInBases[runnerBase + 1] = InGameManager.runnerInBases[runnerBase];
            InGameManager.runnerInBases[runnerBase] = null;
        }

        if (includeBatter)
        {
            InGameManager.runnerInBases[0] = InGameManager.currentBatter;
            for (int i = 0; i < amount; ++i)
            {
                Advance(i);
            }
        }
        else
        {
            for (int i = 1; i <= amount; ++i)
            {
                Advance(i);
            }
        }
    }

    /// <summary>
    /// If runner reaches to home plate.
    /// </summary>
    /// <param name="batter"></param>
    public static void RunnerToHomePlate(Batter batter)
    {
        InGameManager.runnerInBases[3] = null;
        //Add score by 1;
        if (InGameManager.currentAttack == InGameManager.game.home)
        {
            InGameManager.game.homeScoreBoard.AddRun(1);
        }
        else if (InGameManager.currentAttack == InGameManager.game.away)
        {
            InGameManager.game.awayScoreBoard.AddRun(1);
        }
        else
        {
            throw new System.NullReferenceException("Error: There is no appropriate team matching. Check currentAttack and Game instance again.");
        }

        //And other statistic stuffs.
        bool isEarnedRun = true;
        if (isEarnedRun)
        {
            //Earned Run
            InGameManager.currentPitcher.stats.SetStat(1, PlayerStatistics.PS.ER);
        }
        //Runs Batted In
        InGameManager.currentBatter.stats.SetStat(1, PlayerStatistics.PS.RBI);
        //Run
        batter.stats.SetStat(1, PlayerStatistics.PS.R);
    }
}
