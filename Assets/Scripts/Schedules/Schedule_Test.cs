using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Schedule_Test : Schedule
{
    public Schedule_Test(int _index, DateTime _date,
        string _title = "Test Schedule.",
        Categories _category = Categories.TESTING,
        ScheduleCategory _schedules = ScheduleCategory.TESTING,
        string _desc = "")
        : base(_index, _date, _title, _category, _schedules, _desc)
    {
        description = "Hey, this is just a sample testing schedule in " + date.ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US")) + "!";
        
        items = new string[] {
            "Say hello to my friend.",
        "This is a test schedule item.",
        "Yes, of course you can.",
        "Sorry, better next time.",
        "I'm really disappointed with that."
        };
        
        isSelectable = true;
    }
}
