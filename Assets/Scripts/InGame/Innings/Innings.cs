using System.Collections;
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
        if ((InGameManager.game.homeScoreBoard.R != InGameManager.game.awayScoreBoard.R)
            && InGameManager.currentInning >= 9 && (InGameManager.isBottom || InGameManager.game.homeScoreBoard.R > InGameManager.game.awayScoreBoard.R))
        {
            EndGame();
            return;
        }

        if(InGameManager.isUIEnabled)
        {
            //BoardPanel UI.
            InGameObjects InGameObjects = GameObject.Find("InGameManager").GetComponent<InGameObjects>();
            InGameObjects.boardPanel.AddScorePanel();
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
        //Assigns win or loss to the game.
        if(InGameManager.game.homeScoreBoard.R > InGameManager.game.awayScoreBoard.R)
        {
            InGameManager.game.home.teamStats.SetData(TeamStatistics.TS.WIN, 1);
            InGameManager.game.away.teamStats.SetData(TeamStatistics.TS.LOSS, 1);
        }
        else if (InGameManager.game.homeScoreBoard.R < InGameManager.game.awayScoreBoard.R)
        {
            InGameManager.game.away.teamStats.SetData(TeamStatistics.TS.WIN, 1);
            InGameManager.game.home.teamStats.SetData(TeamStatistics.TS.LOSS, 1);
        }

        //Marks game as played.
        InGameManager.game.isPlayed = true;
        InGameManager.isGameEnd = true;
        //Shows a summary tab after finishes a game.

        //TEMPORARILY Changes scene to main.
        if(InGameManager.isUIEnabled)
        {
            Values.date = Values.date.AddDays(1);
            SceneManager.LoadScene("Main");
        }
    }
}
