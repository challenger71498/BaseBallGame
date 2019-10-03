using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Team
{
    //Constructor
    public Team(string cityName = "Seoul", string teamName = "Bears", string shortName = "SLB", int year = 1990, int month = 1, int day = 1, List<KeyValuePair<int, Player>> playerList = null)
    {
        teamData = new TeamData(cityName, teamName, shortName, year, month, day);
        teamStats = new TeamStatistics();
        players = new SerializableList<KeyValuePair<int, Player>>();
        if (playerList != null)
        {
            players.d = playerList;
        }
        startingMembers = new SerializableList<KeyValuePair<Player.Position, Player>>();
        battingOrder = new SerializableList<Batter>();
        startPitchOrder = new SerializableList<Pitcher>();
    }

    //Override functions
    public override string ToString()
    {
        return teamData.GetData(TeamData.TP.NAME);
    }

    //Member functions
    public float GetPrefAverage(PlayerData.PP pref, List<KeyValuePair<int, Player>> players)
    {
        float sum = 0;

        bool isInSerializableDictPref = false;
        foreach (KeyValuePair<PlayerData.PP, List<PlayerData.PP>> prefPair in PlayerData.serializableDictPrefs)
        {
            if (prefPair.Value.Contains(pref))
            {
                isInSerializableDictPref = true;
                break;
            }
        }

        foreach (KeyValuePair<int, Player> player in players)
        {
            if (isInSerializableDictPref)
            {
                sum += player.Value.playerData.GetDictData(pref);
            }
            else
            {
                sum += player.Value.playerData.GetData(pref);
            }
        }

        return sum / players.Count;
    }

    public float GetStatAverage(PlayerStatistics.PS stat, List<KeyValuePair<int, Player>> players)
    {
        float sum = 0;

        bool isPitcherStat = PlayerStatistics.pitcherPS.Contains(stat);

        foreach (KeyValuePair<int, Player> playerPair in players)
        {
            if (isPitcherStat)
            {
                if (playerPair.Value.GetType() == typeof(Batter))
                {
                    continue;
                }
            }
            else
            {
                if (playerPair.Value.GetType() == typeof(Pitcher))
                {
                    continue;
                }
            }

            sum += playerPair.Value.stats.GetSeason(stat);
        }

        return sum / players.Count;
    }

    public void SetSchedule(DateTime date, League league)
    {
        DateTime currentDate;
        for (currentDate = new DateTime(date.Year, date.Month, date.Day); (currentDate - date).Days < 14; currentDate = currentDate.AddDays(1))
        {
            Game game = league.FindGame(currentDate, this);

            if (game == null)
            {
                continue;
            }
            else
            {
                Values.schedules.Add(Values.schedules.Count, new Schedule_MatchUp(Values.schedules.Count, game.date.date, game));
            }
        }
    }

    public Player GetPlayerByPosition(Player.Position position)
    {
        foreach (KeyValuePair<Player.Position, Player> playerPair in startingMembers.d)
        {
            if(playerPair.Key == position)
            {
                return playerPair.Value;
            }
        }

        throw new NullReferenceException("There is no such player with position " + position.ToString() + ".");
    }

    public Player GetKeyPlayer()
    {
        return players[UnityEngine.Random.Range(0, players.d.Count)].Value;
    }

    //Data members
    public SerializableList<KeyValuePair<int, Player>> players;
    public SerializableList<KeyValuePair<Player.Position, Player>> startingMembers;
    public SerializableList<Batter> battingOrder;
    public SerializableList<Pitcher> startPitchOrder;
    public TeamData teamData;
    public TeamStatistics teamStats;
}
