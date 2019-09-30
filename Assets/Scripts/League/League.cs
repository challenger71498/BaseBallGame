using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class League
{
    //Constructor.
    public League(bool isRandom = true, string leagueName = "", string shortName = "", int year = 1990, int month = 1, int day = 1, int teamAmount = 10, bool makeGameSchedule = true)
    {
        teams = new SerializableList<KeyValuePair<int, Team>>();
        stats = new LeagueStatistics();

        if (isRandom)
        {
            data = new LeagueData("Western League", "WSL", UnityEngine.Random.Range(1969, 1990), 5, 30);
            for (int i = 0; i < teamAmount; ++i)
            {
                teams.d.Add(new KeyValuePair<int, Team>(i, RandomTeamGenerator.CreateTeam()));
            }
        }
        else
        {
            data = new LeagueData(leagueName, shortName, year, month, day);
        }
    }

    //Member functions
    public float GetPrefAverage(PlayerData.PP pref)
    {
        float sum = 0;

        foreach (KeyValuePair<int, Team> teamPair in teams.d)
        {
            sum += teamPair.Value.GetPrefAverage(pref, teamPair.Value.players.d);
        }

        return sum / teams.d.Count;
    }

    public float GetStatAverage(PlayerStatistics.PS stat)
    {
        float sum = 0;

        foreach (KeyValuePair<int, Team> teamPair in teams.d)
        {
            sum += teamPair.Value.GetStatAverage(stat, teamPair.Value.players.d);
        }

        return sum / teams.d.Count;
    }

    public void BuildGameSchedule(int year = -1)
    {
        if (year == -1)
        {
            year = Values.date.Year;
        }

        //Special schedule initialization.

        /* INITIALIZATION COLDE NEEDED */

        //Game schedule initialization.
        //First, set date to saturday of week 4.
        DateTime currentDate = new DateTime(year, 3, 1);
        int week = 1;
        if (currentDate.DayOfWeek == DayOfWeek.Sunday)
        {
            week++;
            currentDate = currentDate.AddDays(6);
        }
        while (currentDate.DayOfWeek != DayOfWeek.Saturday)
        {
            currentDate = currentDate.AddDays(1);
        }
        while (week < 4)
        {
            currentDate = currentDate.AddDays(7);
            week++;
        }

        DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

        //Then initialize games.
        int gamesMade = 0;

        //This is a local function for circulate.
        int circularNine(int x, int r)
        {
            if (x - r >= 1)
            {
                return x - r;
            }
            else
            {
                return 9 + (x - r);
            }
        }

        bool findYear(DateTime date)
        {
            return date.Year == currentDate.Year &&
                date.Month == currentDate.Month &&
                date.Day == currentDate.Day;
        }

        Predicate<DateTime> predicate = findYear;

        SeasonGames = new Dictionary<DateTime, Game>();

        //Make 144 games per team, total 720 games per year.
        while (gamesMade < 144)
        {
            //If there's already a game schedule on currentDate, or it's monday, skip it.
            if (/*SeasonGames.Keys.ToList().FindIndex(predicate) != -1 || */(currentDate.DayOfWeek == DayOfWeek.Monday))
            {
                currentDate = currentDate.AddDays(1);
                continue;
            }
            int remainder = gamesMade % 9;
            int pitchingOrder = gamesMade % 5;
            SeasonGames.Add(new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 1), new Game(
                teams[0].Value, 
                teams[circularNine(1, remainder)].Value, 
                teams[0].Value.startPitchOrder[pitchingOrder], 
                teams[circularNine(1, remainder)].Value.startPitchOrder[pitchingOrder], 
                currentDate
                ));
            SeasonGames.Add(new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 2), new Game(
                teams[circularNine(9, remainder)].Value, 
                teams[circularNine(2, remainder)].Value,
                teams[circularNine(9, remainder)].Value.startPitchOrder[pitchingOrder],
                teams[circularNine(2, remainder)].Value.startPitchOrder[pitchingOrder],
                currentDate
                ));
            SeasonGames.Add(new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 3), new Game(
                teams[circularNine(8, remainder)].Value,
                teams[circularNine(3, remainder)].Value,
                teams[circularNine(8, remainder)].Value.startPitchOrder[pitchingOrder],
                teams[circularNine(3, remainder)].Value.startPitchOrder[pitchingOrder],
                currentDate
                ));
            SeasonGames.Add(new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 4), new Game(
                teams[circularNine(7, remainder)].Value,
                teams[circularNine(4, remainder)].Value,
                teams[circularNine(7, remainder)].Value.startPitchOrder[pitchingOrder],
                teams[circularNine(4, remainder)].Value.startPitchOrder[pitchingOrder],
                currentDate
                ));
            SeasonGames.Add(new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 5), new Game(
                teams[circularNine(6, remainder)].Value,
                teams[circularNine(5, remainder)].Value,
                teams[circularNine(6, remainder)].Value.startPitchOrder[pitchingOrder],
                teams[circularNine(5, remainder)].Value.startPitchOrder[pitchingOrder],
                currentDate
                ));
            //This is a bunch of lines for debug. :(
            //Debug.Log("_____________________________________________________");
            //Debug.Log("0: " + teams[0].Value.teamData.GetData(TeamData.TP.NAME) + " " + circularNine(1, remainder) + " " + teams[circularNine(1, remainder)].Value.teamData.GetData(TeamData.TP.NAME));
            //Debug.Log(circularNine(9, remainder) + " " + teams[circularNine(9, remainder)].Value.teamData.GetData(TeamData.TP.NAME) + " " + circularNine(2, remainder) + " " + teams[circularNine(2, remainder)].Value.teamData.GetData(TeamData.TP.NAME));
            //Debug.Log(circularNine(8, remainder) + " " + teams[circularNine(8, remainder)].Value.teamData.GetData(TeamData.TP.NAME) + " " + circularNine(3, remainder) + " " + teams[circularNine(3, remainder)].Value.teamData.GetData(TeamData.TP.NAME));
            //Debug.Log(circularNine(7, remainder) + " " + teams[circularNine(7, remainder)].Value.teamData.GetData(TeamData.TP.NAME) + " " + circularNine(4, remainder) + " " + teams[circularNine(4, remainder)].Value.teamData.GetData(TeamData.TP.NAME));
            //Debug.Log(circularNine(6, remainder) + " " + teams[circularNine(6, remainder)].Value.teamData.GetData(TeamData.TP.NAME) + " " + circularNine(5, remainder) + " " + teams[circularNine(5, remainder)].Value.teamData.GetData(TeamData.TP.NAME));
            //Debug.Log("_____________________________________________________");
            currentDate = currentDate.AddDays(1);
            gamesMade++;
        }

        Values.myTeam.SetSchedule(startDate, this);
    }

    public Game FindGame(DateTime date, Team team)
    {
        bool findYear(DateTime d)
        {
            return d.Year == date.Year &&
                d.Month == date.Month &&
                d.Day == date.Day;
        }

        Predicate<DateTime> predicate = findYear;

        int index = -1;
        while (true)
        {
            List<DateTime> keyList = SeasonGames.Keys.ToList();
            keyList.Insert(0, new DateTime(1, 1, 1));
            index = keyList.FindIndex(index + 1, predicate);
            if (index == 0 || index == -1)
            {
                break;
            }

            if (SeasonGames[keyList[index]].home == team || SeasonGames[keyList[index]].away == team)
            {
                return SeasonGames[keyList[index]];
            }
        }

        return null;
    }

    public List<Game> RecentMatches(int amount, Team team, DateTime date = default)
    {
        if (date == default)
        {
            date = Values.date;
        }

        date = date.AddDays(-1);

        List<Game> games = new List<Game>();

        for (int i = 0; i < amount; ++i)
        {
            bool isEndOfSeasonGames = false;

            while (FindGame(date, team) == null /*|| !FindGame(date, team).isPlayed*/)  //Should be uncommented after.
            {
                date = date.AddDays(-1);
                if (date < SeasonGames.Keys.ToList().Min())
                {
                    isEndOfSeasonGames = true;
                    break;
                }
            }

            if (isEndOfSeasonGames)
            {
                break;
            }
            else
            {
                games.Add(FindGame(date, team));
                date = date.AddDays(-1);
            }
        }
        return games;
    }

    //Data members
    public SerializableList<KeyValuePair<int, Team>> teams;
    public LeagueData data;
    public LeagueStatistics stats;
    public Dictionary<DateTime, Game> SeasonGames;
}
