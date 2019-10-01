﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Innings
{
    /// <summary>
    /// Advances inning.
    /// </summary>
    public static void AdvanceInning()
    {
        //Innings pitched
        InGameManager.currentPitcher.stats.SetStat(0.8f, PlayerStatistics.PS.IP);

        //If the game satisfies the end condition, end game.
        if ((InGameManager.game.homeScoreBoard.R != InGameManager.game.awayScoreBoard.R) && InGameManager.currentInning >= 9 && InGameManager.isBottom)
        {
            EndGame();
            return;
        }

        //Switch side.
        InGameManager.currentPitcher = InGameManager.otherPitcher;

        if (InGameManager.currentAttack == InGameManager.game.home)
        {
            InGameManager.currentAttack = InGameManager.game.away;
            InGameManager.currentDefend = InGameManager.game.home;

            InGameManager.currentBatter = InGameManager.awayBattingOrder[InGameManager.awayCurrentBattersIndex];
        }
        else if (InGameManager.currentAttack == InGameManager.game.away)
        {
            InGameManager.currentAttack = InGameManager.game.home;
            InGameManager.currentDefend = InGameManager.game.away;

            InGameManager.currentBatter = InGameManager.homeBattingOrder[InGameManager.homeCurrentBattersIndex];
        }
        else
        {
            throw new System.NullReferenceException("Error: There is no appropriate team matching. Check currentAttack and Game instance again.");
        }

        if(InGameManager.isBottom)
        {
            InGameManager.isBottom = false;
            InGameManager.currentInning++;
        }
        else
        {
            InGameManager.isBottom = true;
        }

        for(int i = 0; i < 4; ++i)
        {
            InGameManager.runnerInBases[i] = null;
        }

        InGameManager.outCount = 0;
        AtPlate.ClearCount();
    }

    /// <summary>
    /// Advances batting order by 1.
    /// </summary>
    /// <param name="index"></param>
    public static void AdvanceBattingOrder(ref int index)
    {
        index++;
        index %= 9;
    }

    /// <summary>
    /// Finishes game.
    /// </summary>
    public static void EndGame()
    {
        InGameManager.game.isPlayed = true;
        InGameManager.isGameEnd = true;
        //Shows a summary tab after finishes a game.

        //TEMPORARILY Changes scene to main.
        SceneManager.LoadScene("Main");
    }
}
