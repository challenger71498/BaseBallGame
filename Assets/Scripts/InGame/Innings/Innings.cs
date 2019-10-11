using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        InGameManager.currentPitcher.stats.SetStat(0.7f, PlayerStatistics.PS.IP);

        //If the game satisfies the end condition, end game.
        if ((InGameManager.game.homeScoreBoard.R != InGameManager.game.awayScoreBoard.R)
            && InGameManager.currentInning >= 9 && (InGameManager.isBottom || InGameManager.game.homeScoreBoard.R > InGameManager.game.awayScoreBoard.R))
        {
            EndGame();
            return;
        }

        if (InGameManager.isUIEnabled)
        {
            //BoardPanel UI.
            InGameObjects InGameObjects = GameObject.Find("InGameManager").GetComponent<InGameObjects>();
            InGameObjects.boardPanel.AddScorePanel();
        }

        //Switch side.
        Pitcher tempPitcher = InGameManager.currentPitcher;
        InGameManager.currentPitcher = InGameManager.otherPitcher;
        InGameManager.otherPitcher = tempPitcher;

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

        if (InGameManager.isBottom)
        {
            InGameManager.isBottom = false;
            InGameManager.currentInning++;
        }
        else
        {
            InGameManager.isBottom = true;
        }

        for (int i = 0; i < 4; ++i)
        {
            InGameManager.runnerInBases[i] = null;
        }

        InGameManager.outCount = 0;
        AtPlate.ClearCount();

        //UI
        if (InGameManager.isUIEnabled)
        {
            InGameObjects InGameObjects = GameObject.Find("InGameManager").GetComponent<InGameObjects>();
            InGameObjects.PlayerUIApply.SetPlayers();
        }
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
        void SetZeroBatter<T>(HashSet<T> set) where T : Player
        {
            foreach (T player in set)
            {
                foreach (PlayerStatistics.PS stat in PlayerStatistics.generalPS)
                {
                    if (player.stats.FIndStat(stat))
                    {
                        player.stats.SetStat(0, stat);
                    }

                    //NOTE: THIS SHOULD BE REMOVED AFTER RATING FUNCTION DEFINED.
                    player.stats.SetStat(Random.Range(3f, 9.9f), PlayerStatistics.PS.RAT);
                }

                if (typeof(T) == typeof(Batter))
                {
                    foreach (PlayerStatistics.PS stat in PlayerStatistics.batterPS)
                    {
                        if (!player.stats.FIndStat(stat))
                        {
                            player.stats.SetStat(0, stat);
                        }
                    }
                }
                else if (typeof(T) == typeof(Pitcher))
                {
                    foreach (PlayerStatistics.PS stat in PlayerStatistics.pitcherPS)
                    {
                        if (!player.stats.FIndStat(stat))
                        {
                            player.stats.SetStat(0, stat);
                        }
                    }
                }
            }
        }

        Player FindHighestRate()
        {
            float rate = 0;
            Player highestPlayer = null;

            void rateRefresh<T>(HashSet<T> set) where T : Player
            {
                foreach (Player player in set)
                {
                    float playerRate = player.stats.GetStat(PlayerStatistics.PS.RAT);
                    if (playerRate > rate)
                    {
                        rate = playerRate;
                        highestPlayer = player;
                    }
                }
            }

            rateRefresh(InGameManager.game.homeBatterSet);
            rateRefresh(InGameManager.game.awayBatterSet);
            rateRefresh(InGameManager.game.homePitcherSet);
            rateRefresh(InGameManager.game.awayPitcherSet);

            return highestPlayer;
        }

        //Set unassigned stats to be zero.
        SetZeroBatter(InGameManager.game.homeBatterSet);
        SetZeroBatter(InGameManager.game.awayBatterSet);
        SetZeroBatter(InGameManager.game.homePitcherSet);
        SetZeroBatter(InGameManager.game.awayPitcherSet);

        //Assigns POTM.
        InGameManager.game.playerOfTheMatch = FindHighestRate();

        //Assigns win or loss to the game.
        if (InGameManager.game.homeScoreBoard.R > InGameManager.game.awayScoreBoard.R)
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
        
        if (InGameManager.isUIEnabled)
        {
            InGameObjects InGameObjects = GameObject.Find("InGameManager").GetComponent<InGameObjects>();
            InGameObjects.resultPanel.SetActive(true);
            InGameObjects.resultPanel.GetComponent<ResultPanel>().RefreshItems(InGameManager.game);
        }
    }
}
