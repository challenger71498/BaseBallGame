using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PitchedWild
{
    public static void AddStatistics(Pitcher pitcher)
    {
        pitcher.stats.SetStat(1, PlayerStatistics.PS.WP);
    }
}
