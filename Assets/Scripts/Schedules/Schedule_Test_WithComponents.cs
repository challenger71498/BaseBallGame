using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Schedule_Test_WithComponents : Schedule
{
    public Schedule_Test_WithComponents(int _index, DateTime _date,
        string _title = "Test schedule with components.",
        Categories _category = Categories.TESTING,
        ScheduleCategory _schedules = ScheduleCategory.TESTING_WITHCOMPONENT,
        string _desc = "")
        : base(_index, _date, _title, _category, _schedules, _desc, true)
    {
        description = "Hey, this is just a sample testing schedule in " + date.ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US")) + ", with some components!";
        
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
