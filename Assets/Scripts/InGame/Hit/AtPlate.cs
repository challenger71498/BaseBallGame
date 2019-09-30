using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AtPlate
{
    public static Game game = InGameManager.game;
    public static Team currentAttack = InGameManager.currentAttack;

    public static Batter[] runnerInBases = InGameManager.runnerInBases;
    public static Batter currentBatter = InGameManager.currentBatter;
    public static Pitcher currentPitcher = InGameManager.currentPitcher;
    public static List<KeyValuePair<int, Batter>> homeBattingOrder = InGameManager.homeBattingOrder;
    public static List<KeyValuePair<int, Batter>> awayBattingOrder = InGameManager.awayBattingOrder;

    public static int strikeCount = InGameManager.strikeCount;
    public static int ballCount = InGameManager.ballCount;
    public static int outCount = InGameManager.outCount;

    /// <summary>
    /// Determines whether swing or not.
    /// </summary>
    /// <returns></returns>
    public static bool SwingDetermine(bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0, 1) < 0.5f;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Adds strike count.
    /// </summary>
    public static void AddStrike()
    {
        if (strikeCount < 2)
        {
            strikeCount++;
        }
        else
        {
            //Stirkeout.
            AddOut(Out.STRIKEOUT);
        }
    }

    /// <summary>
    /// Adds ball count.
    /// </summary>
    public static void AddBall(bool isHBP = false, bool isIBB = false)
    {
        if (ballCount < 3)
        {
            ballCount++;
        }
        else
        {
            BaseRunning.AdvanceRunner(1);
            if (isHBP)
            {
                currentPitcher.stats.SetStat(1, PlayerStatistics.PS.HB);
                currentBatter.stats.SetStat(1, PlayerStatistics.PS.HBP);
            }
            if (isIBB)
            {
                currentPitcher.stats.SetStat(1, PlayerStatistics.PS.IBB_PIT);
                currentBatter.stats.SetStat(1, PlayerStatistics.PS.IBB_BAT);
            }
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.BB_PIT);
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.BB_BAT);
            ClearCount();
        }
    }

    public enum Out
    {
        STRIKEOUT, FLY_BALL, GROUND_BALL
    }

    /// <summary>
    /// Adds out count.
    /// </summary>
    public static void AddOut(Out outs)
    {
        //Statistics.
        currentPitcher.stats.SetStat(0.1f, PlayerStatistics.PS.IP);

        //Determine which out happened, and apply appropriate stats.
        //Strikout
        if (outs == Out.STRIKEOUT)
        {
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.K_PIT);
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.K_BAT);
        }
        else if (outs == Out.GROUND_BALL)
        {
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.GB_PIT);
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.GB_BAT);
        }
        else if (outs == Out.FLY_BALL)
        {
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.FB_PIT);
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.FB_BAT);
        }

        //Advance Batter.
        if (outCount < 2)
        {
            outCount++;
            if (currentAttack == game.home)
            {
                Innings.AdvanceBattingOrder(ref InGameManager.homeCurrentBattersIndex);
                AdvanceBatterToPlate();
            }
            else if (currentAttack == game.away)
            {
                Innings.AdvanceBattingOrder(ref InGameManager.awayCurrentBattersIndex);
                AdvanceBatterToPlate();
            }
        }
        else
        {
            Innings.AdvanceInning();
        }
    }

    /// <summary>
    /// Clears strike and ball count.
    /// </summary>
    public static void ClearCount()
    {
        strikeCount = 0;
        ballCount = 0;
    }

    /// <summary>
    /// Place batter at plate.
    /// </summary>
    public static void AdvanceBatterToPlate()
    {
        if (currentAttack == game.home)
        {
            runnerInBases[0] = homeBattingOrder[InGameManager.homeCurrentBattersIndex].Value;
            currentBatter = runnerInBases[0];
        }
        else if (currentAttack == game.away)
        {
            runnerInBases[0] = awayBattingOrder[InGameManager.awayCurrentBattersIndex].Value;
            currentBatter = runnerInBases[0];
        }
        else
        {
            throw new System.Exception("There is no currentAttack team matching any home or away team. Check currentAttack variable to fix.");
        }

        currentBatter.stats.SetStat(1, PlayerStatistics.PS.PA);
        currentPitcher.stats.SetStat(1, PlayerStatistics.PS.BF);
    }
}
