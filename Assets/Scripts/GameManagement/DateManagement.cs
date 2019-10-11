using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DateManagement
{
    public static void ProceedDate()
    {
        Values.league.AdjustWeather();
        Values.date = Values.date.AddDays(1);
    }
}
