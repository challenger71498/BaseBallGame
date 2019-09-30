﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Innings
{
    public static Game game = InGameManager.game;
    public static Team currentAttack = InGameManager.currentAttack;
    public static Team currentDefend = InGameManager.currentDefend;
    public static List<Batter> homeBattingOrder = InGameManager.homeBattingOrder;
    public static List<Batter> awayBattingOrder = InGameManager.awayBattingOrder;

    public static Batter[] runnerInBases = InGameManager.runnerInBases;
    public static Batter currentBatter = InGameManager.currentBatter;
    public static Pitcher currentPitcher = InGameManager.currentPitcher;
    public static Pitcher otherPitcher = InGameManager.otherPitcher;
    
    /// <summary>
    /// Advances inning.
    /// </summary>
    public static void AdvanceInning()
    {
        //Innings pitched
        currentPitcher.stats.SetStat(0.8f, PlayerStatistics.PS.IP);

        //If the game satisfies the end condition, end game.
        if ((game.homeScoreBoard.R != game.awayScoreBoard.R) || InGameManager.isGameEnd)
        {
            EndGame();
            return;
        }

        //Switch side.
        currentPitcher = otherPitcher;

        if (currentAttack == game.home)
        {
            currentAttack = game.away;
            currentDefend = game.home;

            currentBatter = awayBattingOrder[InGameManager.awayCurrentBattersIndex];
        }
        else if (currentAttack == game.away)
        {
            currentAttack = game.home;
            currentDefend = game.away;

            currentBatter = homeBattingOrder[InGameManager.homeCurrentBattersIndex];
        }
        else
        {
            throw new System.NullReferenceException("Error: There is no appropriate team matching. Check currentAttack and Game instance again.");
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
        game.isPlayed = true;
        InGameManager.isGameEnd = true;
        //Shows a summary tab after finishes a game.

        //TEMPORARILY Changes scene to main.
        SceneManager.LoadScene("Main");
    }
}
