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


    public Game(Team _home, Team _away, int year, int month, int day)
    {
        isPlayed = false;
        home = _home;
        away = _away;
        date = new SerializableDateTime(new DateTime(year, month, day));
    }

    public Game(Team _home, Team _away, DateTime _date)
    {
        isPlayed = false;
        home = _home;
        away = _away;
        date = new SerializableDateTime(new DateTime(_date.Year, _date.Month, _date.Day));
    }

    public void WriteData()
    {
        if(isPlayed)
        {
            throw new Exception("Game already played. Cannot re-play played game.");
        }
        isPlayed = true;
    }

    public Team GetTeam(Team team)
    {
        if(home == team)
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
        if(home == team)
        {
            if(homeScoreBoard.R > awayScoreBoard.R)
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
        else if(away == team)
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
    public class ScoreBoard
    {
        public ScoreBoard()
        {
            inningScores = new List<int>();
            R = 0;
            H = 0;
            E = 0;
        }

        public void AddRun(int inning)
        {
            if (inningScores.Count < inning)
            {
                inningScores.Add(1);
            }
            else
            {
                inningScores[inning] += 1;
            }
        }

        public List<int> inningScores;
        public int R;
        public int H;
        public int E;
    }

    public bool isPlayed;

    public SerializableDateTime date;

    public Team home;
    public Team away;

    public Pitcher homeStarterPitcher;  //Need to be assigned.
    public Pitcher awayStarterPitcher;

    public ScoreBoard homeScoreBoard;
    public ScoreBoard awayScoreBoard;
}
