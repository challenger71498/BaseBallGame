using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BaseRunning
{
    public static Game game = InGameManager.game;
    public static Team currentAttack = InGameManager.currentAttack;

    public static Batter[] runnerInBases = InGameManager.runnerInBases;
    public static Batter currentBatter = InGameManager.currentBatter;
    public static Pitcher currentPitcher = InGameManager.currentPitcher;
    
    /// <summary>
    /// Determines whether attempt to steal base or not.
    /// </summary>
    /// <param name="batter"></param>
    /// <returns></returns>
    public static bool BaseStealDetermine(Batter batter, bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0, 1) < 0.5f;
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
                RunnerToHomePlate(runnerInBases[3]);
            }

            if (runnerInBases[runnerBase + 1] != null)
            {
                Advance(runnerBase + 1);
            }
            runnerInBases[runnerBase + 1] = runnerInBases[runnerBase];
            runnerInBases[runnerBase] = null;
        }

        if (includeBatter)
        {
            runnerInBases[0] = currentBatter;
            for (int i = 0; i < amount; ++i)
            {
                Advance(amount);
            }
        }
        else
        {
            for (int i = 1; i <= amount; ++i)
            {
                Advance(amount);
            }
        }
    }

    /// <summary>
    /// If runner reaches to home plate.
    /// </summary>
    /// <param name="batter"></param>
    public static void RunnerToHomePlate(Batter batter)
    {
        runnerInBases[3] = null;
        //Add score by 1;
        if (currentAttack == game.home)
        {
            game.homeScoreBoard.AddRun(1);
        }
        else if (currentAttack == game.away)
        {
            game.awayScoreBoard.AddRun(1);
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
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.ER);
        }
        //Runs Batted In
        currentBatter.stats.SetStat(1, PlayerStatistics.PS.RBI);
        //Run
        batter.stats.SetStat(1, PlayerStatistics.PS.R);
    }
}
