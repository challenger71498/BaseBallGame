using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Schedule_MatchUp : Schedule
{
    public Schedule_MatchUp(
        int _index,
        DateTime _date,
        Game _game,
        string _title = "MatchUp",
        Categories _category = Categories.MATCHUP,
        ScheduleCategory _schedules = ScheduleCategory.MATCHUP,
        string _desc = ""
        )
        : base(_index, _date, _title, _category, _schedules, _desc)
    {
        description = "We have a match on " + date.ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US")) + ".";

        isSelectable = false;

        game = _game;

        if(game.home == Values.myTeam)
        {
            enemy = game.away;
        }
        else
        {
            enemy = game.home;
        }
    }

    public Game game;
    public Team enemy;
}
