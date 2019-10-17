using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sort
{
    public static void PlayerSort(List<KeyValuePair<int, Player>> playerList, SortDropdown.SortMode sortMode)
    {
        if (sortMode == SortDropdown.SortMode.NAME)
        {
            playerList.Sort(PlayerCompareByName);
        }
        else if (sortMode == SortDropdown.SortMode.NUMBER)
        {
            playerList.Sort(PlayerCompareByNumber);
        }
        else if (sortMode == SortDropdown.SortMode.OVERALL)
        {
            playerList.Sort(PlayerCompareByOverall);
        }
        else if (sortMode == SortDropdown.SortMode.POSITION)
        {
            playerList.Sort(PlayerCompareByPosition);
        }
        else if (sortMode == SortDropdown.SortMode.ORDER)
        {
            playerList.Sort(PlayerCompareByOrder);
        }

        if (!SortDropdown.isAscendingOrder)
        {
            playerList.Reverse();
        }
    }

    public static int PlayerCompareByName(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        var NameComparison = ((string)pair2.Value.playerData.GetData(PlayerData.PP.NAME)).CompareTo(pair1.Value.playerData.GetData(PlayerData.PP.NAME));
        if (NameComparison != 0)
        {
            return -NameComparison;
        }
        else
        {
            return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
        }
    }

    public static int PlayerCompareByOverall(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        var OverallComparison = pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
        return OverallComparison;
    }

    public static int PlayerCompareByPosition(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        var PositionComparison = ((int)pair1.Value.playerData.GetData(PlayerData.PP.POSITION)).CompareTo((int)(pair2.Value.playerData.GetData(PlayerData.PP.POSITION)));
        if (PositionComparison != 0)
        {
            return PositionComparison;
        }
        else
        {
            return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
        }
    }

    public static int PlayerCompareByNumber(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        var NumberComparison = ((int)pair1.Value.playerData.GetData(PlayerData.PP.NUMBER)).CompareTo(pair2.Value.playerData.GetData(PlayerData.PP.NUMBER));
        if (NumberComparison != 0)
        {
            return NumberComparison;
        }
        else
        {
            return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
        }
    }
    public static int PlayerCompareByOrder(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        int order1 = pair1.Value.order;
        int order2 = pair2.Value.order;

        if (order1 == -1)
        {
            order1 = 100;
        }

        if (order2 == -1)
        {
            order2 = 100;
        }

        int orderComp = order1.CompareTo(order2);
        if (orderComp != 0)
        {
            return orderComp;
        }
        else
        {
            return (int)pair1.Value.playerData.GetData(PlayerData.PP.POSITION).CompareTo(pair2.Value.playerData.GetData(PlayerData.PP.POSITION));
        }
    }
}
