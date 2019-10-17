using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListObject : MonoBehaviour
{
    //Refreshes player list.
    public static Filter.StartingMemberFilter currentStartingMemberFilter = Filter.StartingMemberFilter.ALL;
    public static bool isModeNow = true;
    public static Player.Position currentPosition;
    public static Player.MetaPosition currentMetaPosition;

    /// <summary>
    /// Clears current list.
    /// </summary>
    public void ClearList()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// A utility function for checking conditions at filtering.
    /// </summary>
    /// <param name="startingMemberFilter"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    bool Check(Filter.StartingMemberFilter startingMemberFilter, int i)
    {
        if (startingMemberFilter == Filter.StartingMemberFilter.MEMBER_EXCLUDED && Values.myTeam.players[i].Value.isStartingMember && !Values.myTeam.players[i].Value.isSubstitute)
        {
            return false;
        }
        else if (startingMemberFilter == Filter.StartingMemberFilter.MEMBER_ONLY && !Values.myTeam.players[i].Value.isStartingMember)
        {
            return false;
        }
        else if (startingMemberFilter == Filter.StartingMemberFilter.SUB_ONLY && !Values.myTeam.players[i].Value.isSubstitute)
        {
            return false;
        }
        else if (startingMemberFilter == Filter.StartingMemberFilter.SUB_EXCLUDED && Values.myTeam.players[i].Value.isSubstitute)
        {
            return false;
        }
        else return true;
    }

    /// <summary>
    /// Refreshes player list by filter mode.
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="sortMode"></param>
    /// <param name="startingMemberFilter"></param>
    /// <param name="playerView"></param>
    /// <returns></returns>
    public int RefreshPlayerList(
        Filter.Mode mode,
        SortDropdown.SortMode sortMode,
        bool isPrefShown = true,
        Filter.StartingMemberFilter startingMemberFilter = Filter.StartingMemberFilter.ALL,
        PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_STATISTICS_TRAININGS)
    {
        currentStartingMemberFilter = startingMemberFilter;
        isModeNow = true;

        int instantiatedAmount = 0;

        //Removes children of player content.
        ClearList();

        Sort.PlayerSort(Values.myTeam.players.d, sortMode);

        //Instantiates playerPanel to it.
        for (int i = 0; i < Values.myTeam.players.d.Count; ++i)
        {
            if(!Check(startingMemberFilter, i))
            {
                continue;
            }

            if (mode == Filter.Mode.BATTERS && Values.myTeam.players[i].Value.GetType() == typeof(Pitcher))
            {
                continue;
            }
            else if (mode == Filter.Mode.PITCHERS && Values.myTeam.players[i].Value.GetType() == typeof(Batter))
            {
                continue;
            }

            ++instantiatedAmount;
            PlayerPrefab.PlayerInstantiate(Values.myTeam.players[i].Value, transform, playerView, isPrefShown);
        }

        return instantiatedAmount;
    }

    /// <summary>
    /// Refreshes player list by position.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="sortMode"></param>
    /// <param name="startingMemberFilter"></param>
    /// <param name="playerView"></param>
    /// <param name="isPrefShown"></param>
    /// <returns></returns>
    public int RefreshPlayerList(
        Player.Position position,
        SortDropdown.SortMode sortMode,
        bool isPrefShown = true,
        Filter.StartingMemberFilter startingMemberFilter = Filter.StartingMemberFilter.ALL,
        PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_STATISTICS_TRAININGS)
    {
        currentStartingMemberFilter = startingMemberFilter;
        isModeNow = false;
        currentPosition = position;

        int instantiatedAmount = 0;

        //Removes children of player content.
        ClearList();

        Sort.PlayerSort(Values.myTeam.players.d, sortMode);

        //Instantiates playerPanel to it.
        for (int i = 0; i < Values.myTeam.players.d.Count; ++i)
        {
            if (!Check(startingMemberFilter, i))
            {
                continue;
            }

            if (Values.myTeam.players[i].Value.playerData.GetData(PlayerData.PP.POSITION) != position)
            {
                continue;
            }

            ++instantiatedAmount;
            PlayerPrefab.PlayerInstantiate(Values.myTeam.players[i].Value, transform, playerView, isPrefShown);
        }

        return instantiatedAmount;
    }

    /// <summary>
    /// Refreshes player list by meta position.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="sortMode"></param>
    /// <param name="startingMemberFilter"></param>
    /// <param name="playerView"></param>
    /// <param name="isPrefShown"></param>
    /// <returns></returns>
    public int RefreshPlayerList(
        Player.MetaPosition metaPosition,
        SortDropdown.SortMode sortMode,
        bool isPrefShown = true,
        Filter.StartingMemberFilter startingMemberFilter = Filter.StartingMemberFilter.ALL,
        PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_STATISTICS_TRAININGS)
    {
        currentStartingMemberFilter = startingMemberFilter;
        isModeNow = false;
        currentMetaPosition = metaPosition;

        int instantiatedAmount = 0;

        //Removes children of player content.
        ClearList();

        Sort.PlayerSort(Values.myTeam.players.d, sortMode);

        //Instantiates playerPanel to it.
        for (int i = 0; i < Values.myTeam.players.d.Count; ++i)
        {
            if (!Check(startingMemberFilter, i))
            {
                continue;
            }

            if (Values.myTeam.players[i].Value.playerData.GetData(PlayerData.PP.META_POSITION) != metaPosition)
            {
                continue;
            }

            ++instantiatedAmount;
            PlayerPrefab.PlayerInstantiate(Values.myTeam.players[i].Value, transform, playerView, isPrefShown);
        }

        return instantiatedAmount;
    }
}
