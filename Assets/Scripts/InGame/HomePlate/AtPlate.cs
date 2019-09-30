using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AtPlate
{
    /// <summary>
    /// Determines whether swing or not.
    /// </summary>
    /// <returns></returns>
    public static bool SwingDetermine(bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0f, 1f) < 0.2f;
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
        if (InGameManager.strikeCount < 2)
        {
            InGameManager.strikeCount++;
            Debug.Log("STRIKE");
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
        if (isHBP)
        {
            InGameManager.currentPitcher.stats.SetStat(1, PlayerStatistics.PS.HB);
            InGameManager.currentBatter.stats.SetStat(1, PlayerStatistics.PS.HBP);
            BaseRunning.AdvanceRunner(1);
            ClearCount();
            Debug.Log("HBP");
        }
        else if (isIBB)
        {
            InGameManager.currentPitcher.stats.SetStat(1, PlayerStatistics.PS.IBB_PIT);
            InGameManager.currentBatter.stats.SetStat(1, PlayerStatistics.PS.IBB_BAT);
            BaseRunning.AdvanceRunner(1);
            ClearCount();
            Debug.Log("IBB");
        }
        else if (InGameManager.ballCount >= 3)
        {
            InGameManager.currentPitcher.stats.SetStat(1, PlayerStatistics.PS.BB_PIT);
            InGameManager.currentBatter.stats.SetStat(1, PlayerStatistics.PS.BB_BAT);
            BaseRunning.AdvanceRunner(1);
            ClearCount();
            Debug.Log("BASE ON BALLS");
        }
        else
        {
            InGameManager.ballCount++;
            Debug.Log("BALL");
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
        //Clears count.
        ClearCount();

        //Statistics.
        InGameManager.currentPitcher.stats.SetStat(0.1f, PlayerStatistics.PS.IP);

        //Determine which out happened, and apply appropriate stats.
        //Strikout
        if (outs == Out.STRIKEOUT)
        {
            InGameManager.currentPitcher.stats.SetStat(1, PlayerStatistics.PS.K_PIT);
            InGameManager.currentBatter.stats.SetStat(1, PlayerStatistics.PS.K_BAT);
            Debug.Log("STRIKEOUT");
        }
        else if (outs == Out.GROUND_BALL)
        {
            InGameManager.currentPitcher.stats.SetStat(1, PlayerStatistics.PS.GB_PIT);
            InGameManager.currentBatter.stats.SetStat(1, PlayerStatistics.PS.GB_BAT);
            Debug.Log("GROUNDBALL");
        }
        else if (outs == Out.FLY_BALL)
        {
            InGameManager.currentPitcher.stats.SetStat(1, PlayerStatistics.PS.FB_PIT);
            InGameManager.currentBatter.stats.SetStat(1, PlayerStatistics.PS.FB_BAT);
            Debug.Log("FLYBALL");
        }

        //Advance Batter.
        if (InGameManager.outCount < 2)
        {
            InGameManager.outCount++;
            if (InGameManager.currentAttack == InGameManager.game.home)
            {
                Innings.AdvanceBattingOrder(ref InGameManager.homeCurrentBattersIndex);
                AdvanceBatterToPlate();
            }
            else if (InGameManager.currentAttack == InGameManager.game.away)
            {
                Innings.AdvanceBattingOrder(ref InGameManager.awayCurrentBattersIndex);
                AdvanceBatterToPlate();
            }
        }
        else
        {
            if(InGameManager.isBottom)
            {
                Innings.AdvanceInning();
            }
            else
            {
                Innings.AdvanceInning();
            }
        }
    }

    /// <summary>
    /// Clears strike and ball count.
    /// </summary>
    public static void ClearCount()
    {
        InGameManager.strikeCount = 0;
        InGameManager.ballCount = 0;
    }

    /// <summary>
    /// Place batter at plate.
    /// </summary>
    public static void AdvanceBatterToPlate()
    {
        if (InGameManager.currentAttack == InGameManager.game.home)
        {
            InGameManager.runnerInBases[0] = InGameManager.homeBattingOrder[InGameManager.homeCurrentBattersIndex];
            InGameManager.currentBatter = InGameManager.runnerInBases[0];
        }
        else if (InGameManager.currentAttack == InGameManager.game.away)
        {
            InGameManager.runnerInBases[0] = InGameManager.awayBattingOrder[InGameManager.awayCurrentBattersIndex];
            InGameManager.currentBatter = InGameManager.runnerInBases[0];
        }
        else
        {
            throw new System.Exception("There is no currentAttack team matching any home or away team. Check currentAttack variable to fix.");
        }

        InGameManager.currentBatter.stats.SetStat(1, PlayerStatistics.PS.PA);
        InGameManager.currentPitcher.stats.SetStat(1, PlayerStatistics.PS.BF);
    }
}
