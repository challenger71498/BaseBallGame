using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Game
{
    public enum GameResult
    {
        WIN, DRAW, LOSS
    }


    public Game(Team _home, Team _away, Pitcher _homePitcher, Pitcher _awayPitcher, int year, int month, int day)
    {
        isPlayed = false;
        home = _home;
        away = _away;
        date = new SerializableDateTime(new DateTime(year, month, day));
        homeScoreBoard = new ScoreBoard();
        awayScoreBoard = new ScoreBoard();
        homeStarterPitcher = _homePitcher;
        awayStarterPitcher = _awayPitcher;
    }

    public Game(Team _home, Team _away, Pitcher _homePitcher, Pitcher _awayPitcher, DateTime _date)
    {
        isPlayed = false;
        home = _home;
        away = _away;
        date = new SerializableDateTime(new DateTime(_date.Year, _date.Month, _date.Day));
        homeScoreBoard = new ScoreBoard();
        awayScoreBoard = new ScoreBoard();
        homeStarterPitcher = _homePitcher;
        awayStarterPitcher = _awayPitcher;
    }

    public void WriteData()
    {
        if (isPlayed)
        {
            throw new Exception("Game already played. Cannot re-play played game.");
        }
        isPlayed = true;
    }

    public Team GetTeam(Team team)
    {
        if (home == team)
        {
            return home;
        }
        else if (away == team)
        {
            return away;
        }
        else
        {
            throw new NullReferenceException("There is no such team named as " + team.teamData.GetData(TeamData.TP.NAME) + ".");
        }
    }

    public int GetScore(Team team)
    {
        if (home == team)
        {
            return homeScoreBoard.R;
        }
        else if (away == team)
        {
            return awayScoreBoard.R;
        }
        else
        {
            throw new NullReferenceException("There is no such team named as " + team.teamData.GetData(TeamData.TP.NAME) + ".");
        }
    }

    public GameResult GetGameResult(Team team)
    {
        if (home == team)
        {
            if (homeScoreBoard.R > awayScoreBoard.R)
            {
                return GameResult.WIN;
            }
            else if (homeScoreBoard.R < awayScoreBoard.R)
            {
                return GameResult.LOSS;
            }
            else
            {
                return GameResult.DRAW;
            }
        }
        else if (away == team)
        {
            if (homeScoreBoard.R > awayScoreBoard.R)
            {
                return GameResult.LOSS;
            }
            else if (homeScoreBoard.R < awayScoreBoard.R)
            {
                return GameResult.WIN;
            }
            else
            {
                return GameResult.DRAW;
            }
        }
        else
        {
            throw new NullReferenceException("There is no such team named as " + team.teamData.GetData(TeamData.TP.NAME) + ".");
        }
    }

    /// <summary>
    /// A data structure class for score boarding.
    /// </summary>
    [Serializable]
    public class ScoreBoard
    {
        public ScoreBoard()
        {
            inningScores = new SerializableList<int>();
            for(int i = 0; i < 9; ++i)
            {
                inningScores.d.Add(0);
            }

            R = 0;
            H = 0;
            E = 0;
        }

        public void AddRun(int inning)
        {
            if (inningScores.d.Count < inning)
            {
                inningScores.d.Add(1);
            }
            else
            {
                inningScores[inning - 1] += 1;
            }
            R += 1;
        }

        public SerializableList<int> inningScores;
        public int R;
        public int H;
        public int E;
    }

    public bool isPlayed;

    public SerializableDateTime date;

    public Team home;
    public Team away;

    public Pitcher homeStarterPitcher;
    public Pitcher awayStarterPitcher;

    public ScoreBoard homeScoreBoard;
    public ScoreBoard awayScoreBoard;
}
