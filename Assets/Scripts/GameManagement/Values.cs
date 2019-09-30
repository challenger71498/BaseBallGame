using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is for global values.
public static class Values
{
    public static DateTime date = new DateTime(2019, 3, 23);
    public static int dollar = 10000;

    //data
    //sample data
    public static Dictionary<int, Schedule> sampleSchedules = new Dictionary<int, Schedule>() {
        {0, new Schedule_Test(0, new DateTime(2019, 03, 16)) },
        {1, new Schedule_Test_noSelect(1, new DateTime(2019, 03, 17)) },
        {2, new Schedule_Test(2, new DateTime(2019, 03, 18)) },
        {3, new Schedule_Test(3, new DateTime(2019, 03, 18)) },
        {4, new Schedule_Test_noSelect(4, new DateTime(2019, 03, 19)) },
        {5, new Schedule_Test_noSelect(5, new DateTime(2019, 03, 20)) },
        {6, new Schedule_Test(6, new DateTime(2019, 03, 21)) },
        {7, new Schedule_Test_noSelect(7, new DateTime(2019, 03, 22)) },
        {8, new Schedule_Test_noSelect(8, new DateTime(2019, 03, 24)) },
        {9, new Schedule_Test_WithComponents(9, new DateTime(2019, 03, 25)) }
    };

    public static League league;

    public static Team myTeam;

    public static Dictionary<int, Schedule> schedules;

    public static Dictionary<DateTime, List<Schedule>> scheduleByDate = new Dictionary<DateTime, List<Schedule>>();
}
