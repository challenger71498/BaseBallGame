using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;


public class sort_in_sort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //
    //Sort Functions
    //


    //public void PlayerSort(List<KeyValuePair<int, Player>> playerList, SortDropdown.SortMode sortMode)
    //{
    //    if (sortMode == SortDropdown.SortMode.NAME)
    //    {
    //        playerList.Sort(PlayerCompareByName);
    //    }
    //    else if (sortMode == SortDropdown.SortMode.NUMBER)
    //    {
    //        playerList.Sort(PlayerCompareByNumber);
    //    }
    //    else if (sortMode == SortDropdown.SortMode.OVERALL)
    //    {
    //        playerList.Sort(PlayerCompareByOverall);
    //    }
    //    else if (sortMode == SortDropdown.SortMode.POSITION)
    //    {
    //        playerList.Sort(PlayerCompareByPosition);
    //    }

    //    if (!SortDropdown.isAscendingOrder)
    //    {
    //        playerList.Reverse();
    //    }
    //}

    //public int PlayerCompareByName(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    //{
    //    var NameComparison = ((string)pair2.Value.playerData.GetData(PlayerData.PP.NAME)).CompareTo(pair1.Value.playerData.GetData(PlayerData.PP.NAME));
    //    if (NameComparison != 0)
    //    {
    //        return NameComparison;
    //    }
    //    else
    //    {
    //        return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
    //    }

    //    //return ((string)pair2.Value.playerData.GetData(PlayerData.PP.NAME)).CompareTo(pair1.Value.playerData.GetData(PlayerData.PP.NAME));
    //}

    //public int PlayerCompareByOverall(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    //{
    //    var OverallComparison = pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
    //    if (OverallComparison != 0)
    //    {
    //        return OverallComparison;
    //    }
    //    else
    //    {
    //        return ((int)pair1.Value.playerData.GetData(PlayerData.PP.NUMBER)).CompareTo(pair2.Value.playerData.GetData(PlayerData.PP.NUMBER));
    //    }

    //}

    //public int PlayerCompareByPosition(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    //{
    //    var PositionComparison = ((int)pair1.Value.playerData.GetData(PlayerData.PP.POSITION)).CompareTo((int)(pair2.Value.playerData.GetData(PlayerData.PP.POSITION)));
    //    if (PositionComparison != 0)
    //    {
    //        return PositionComparison;
    //    }
    //    else
    //    {
    //        return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
    //    }

    //    //return ((int)pair1.Value.playerData.GetData(PlayerData.PP.POSITION)).CompareTo((int)(pair2.Value.playerData.GetData(PlayerData.PP.POSITION)));
    //}

    //public int PlayerCompareByNumber(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    //{
    //    var NumberComparison = ((int)pair1.Value.playerData.GetData(PlayerData.PP.NUMBER)).CompareTo(pair2.Value.playerData.GetData(PlayerData.PP.NUMBER));
    //    if (NumberComparison != 0)
    //    {
    //        return NumberComparison;
    //    }
    //    else
    //    {
    //        return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
    //    }

    //    //return ((int)pair1.Value.playerData.GetData(PlayerData.PP.NUMBER)).CompareTo(pair2.Value.playerData.GetData(PlayerData.PP.NUMBER));
    //}



    //public int PlayerCompareByName(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    //{
    //    return ((string)pair2.Value.playerData.GetData(PlayerData.PP.NAME)).CompareTo(pair1.Value.playerData.GetData(PlayerData.PP.NAME));
    //}

    //public int PlayerCompareByOverall(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    //{
    //    return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
    //}

    //public int PlayerCompareByPosition(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    //{
    //    return ((int)pair1.Value.playerData.GetData(PlayerData.PP.POSITION)).CompareTo((int)(pair2.Value.playerData.GetData(PlayerData.PP.POSITION)));
    //}

    //public int PlayerCompareByNumber(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    //{
    //    return ((int)pair1.Value.playerData.GetData(PlayerData.PP.NUMBER)).CompareTo(pair2.Value.playerData.GetData(PlayerData.PP.NUMBER));
    //}


}
