using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Schedule_Test_noSelect : Schedule
{
    public Schedule_Test_noSelect(int _index, DateTime _date,
        string _title = "Test schedule without items.",
        Categories _category = Categories.TESTING,
        ScheduleCategory _schedules = ScheduleCategory.TESTING_NOSELECT,
        string _desc = "")
        : base(_index, _date, _title, _category, _schedules, _desc)
    {
        description = "Hey, this is just a sample testing schedule in " + date.ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US")) + ", without selectable items!";

        isSelectable = false;
    }
}
