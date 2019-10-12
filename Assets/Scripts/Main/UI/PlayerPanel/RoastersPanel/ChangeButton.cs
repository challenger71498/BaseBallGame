using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class ChangeButton : MonoBehaviour
{
    public BackToFieldViewButton BackToFieldViewButton;
    public static Player playerFirst;
    public static Player playerSecond;
    public static Player.Position positionFirst;
    public static Player.Position positionSecond;

    public GameManager GameManager;

    public void OnClick()
    {
        SwapPlayer(playerFirst, playerSecond, Values.myTeam);
        //for (int i = 0; i < Values.myTeam.startingMembers.d.Count; ++i)
        //{
        //    if (Values.myTeam.startingMembers[i].Value.playerData.GetData(PlayerData.PP.NAME) == playerFirst.playerData.GetData(PlayerData.PP.NAME) &&
        //        Values.myTeam.startingMembers[i].Value.playerData.GetData(PlayerData.PP.NUMBER) == playerFirst.playerData.GetData(PlayerData.PP.NUMBER))
        //    {
        //        Debug.Log(playerFirst.isSubstitute + " " + playerSecond.isSubstitute);

        //        bool tempStartingmember = playerSecond.isStartingMember == true;        //for deep copy.
        //        bool tempSub = playerSecond.isSubstitute == true;                       //for deep copy.
        //        playerSecond.isStartingMember = playerFirst.isStartingMember == true;   //for deep copy.
        //        playerSecond.isSubstitute = playerFirst.isSubstitute == true;           //for deep copy.
        //        playerFirst.isStartingMember = tempStartingmember;
        //        playerFirst.isSubstitute = tempSub;
        //        playerFirst.playerData.SetData(PlayerData.PP.POSITION, positionSecond);
        //        playerSecond.playerData.SetData(PlayerData.PP.POSITION, positionFirst);
                
        //        for (int j = 0; j < Values.myTeam.startingMembers.d.Count; ++j)
        //        {
        //            if (Values.myTeam.startingMembers[j].Value.playerData.GetData(PlayerData.PP.NAME) == playerSecond.playerData.GetData(PlayerData.PP.NAME) &&
        //                Values.myTeam.startingMembers[j].Value.playerData.GetData(PlayerData.PP.NUMBER) == playerSecond.playerData.GetData(PlayerData.PP.NUMBER))
        //            {
        //                Values.myTeam.startingMembers[j] = new KeyValuePair<Player.Position, Player>(Values.myTeam.startingMembers[j].Key, playerFirst);
        //                break;
        //            }
        //        }
        //        Values.myTeam.startingMembers[i] = new KeyValuePair<Player.Position, Player>(Values.myTeam.startingMembers[i].Key, playerSecond);
        //        break;
        //    }
        //};

        //NotificationExample noti = GameManager.notificationPanel.GetComponent<NotificationExample>();
        //noti.descriptionText = "Successfully changed " + playerFirst.playerData.GetData(PlayerData.PP.NAME) + " to " + playerSecond.playerData.GetData(PlayerData.PP.NAME) + ".";
        //noti.ShowNotification();

        BackToFieldViewButton.OnClick();
    }

    public void SwapPlayer(Player playerBefore, Player playerAfter, Team team)
    {
        if(playerBefore.GetType() != playerAfter.GetType())
        {
            throw new System.Exception("Both type should be same.");
        }

        for (int i = 0; i < team.startingMembers.d.Count; ++i)
        {
            if (team.startingMembers[i].Value.playerData.GetData(PlayerData.PP.NAME) == playerBefore.playerData.GetData(PlayerData.PP.NAME) &&
                team.startingMembers[i].Value.playerData.GetData(PlayerData.PP.NUMBER) == playerBefore.playerData.GetData(PlayerData.PP.NUMBER))
            {
                bool tempStartingmember = playerAfter.isStartingMember == true;        //for deep copy.
                bool tempSub = playerAfter.isSubstitute == true;                       //for deep copy.
                playerAfter.isStartingMember = playerBefore.isStartingMember == true;   //for deep copy.
                playerAfter.isSubstitute = playerBefore.isSubstitute == true;           //for deep copy.
                playerBefore.isStartingMember = tempStartingmember;
                playerBefore.isSubstitute = tempSub;
                playerBefore.playerData.SetData(PlayerData.PP.POSITION, positionSecond);
                playerAfter.playerData.SetData(PlayerData.PP.POSITION, positionFirst);

                for (int j = 0; j < team.startingMembers.d.Count; ++j)
                {
                    if (team.startingMembers[j].Value.playerData.GetData(PlayerData.PP.NAME) == playerAfter.playerData.GetData(PlayerData.PP.NAME) &&
                        team.startingMembers[j].Value.playerData.GetData(PlayerData.PP.NUMBER) == playerAfter.playerData.GetData(PlayerData.PP.NUMBER))
                    {
                        team.startingMembers[j] = new KeyValuePair<Player.Position, Player>(team.startingMembers[j].Key, playerBefore);
                        break;
                    }
                }
                team.startingMembers[i] = new KeyValuePair<Player.Position, Player>(team.startingMembers[i].Key, playerAfter);

                //Algorithm for swap from start pitching order or batting order.
                if(playerBefore.GetType() == typeof(Pitcher))
                {
                    bool isBothThere = false;

                    for(int j = 0; j < team.startPitchOrder.d.Count; ++j)
                    {
                        if(team.startPitchOrder[j] == playerBefore)
                        {
                            for(int k = 0; k < team.startPitchOrder.d.Count; ++k)
                            {
                                if (team.startPitchOrder[k] == playerAfter)
                                {
                                    team.startPitchOrder[j] = (Pitcher)playerAfter;
                                    team.startPitchOrder[k] = (Pitcher)playerBefore;

                                    isBothThere = true;
                                    break;
                                }
                            }

                            if(!isBothThere)
                            {
                                team.startPitchOrder[j] = (Pitcher)playerAfter;
                            }
                            break;
                        }
                        else if(team.startPitchOrder[j] == playerAfter)
                        {
                            for (int k = 0; k < team.startPitchOrder.d.Count; ++k)
                            {
                                if (team.startPitchOrder[k] == playerBefore)
                                {
                                    team.startPitchOrder[j] = (Pitcher)playerBefore;
                                    team.startPitchOrder[k] = (Pitcher)playerAfter;

                                    isBothThere = true;
                                    break;
                                }
                            }

                            if (!isBothThere)
                            {
                                team.startPitchOrder[j] = (Pitcher)playerBefore;
                            }
                            break;
                        }
                    }
                }
                else if (playerBefore.GetType() == typeof(Batter))
                {
                    bool isBothThere = false;

                    for (int j = 0; j < team.battingOrder.d.Count; ++j)
                    {
                        if (team.battingOrder[j] == playerBefore)
                        {
                            for (int k = 0; k < team.battingOrder.d.Count; ++k)
                            {
                                if (team.battingOrder[k] == playerAfter)
                                {
                                    team.battingOrder[j] = (Batter)playerAfter;
                                    team.battingOrder[k] = (Batter)playerBefore;

                                    isBothThere = true;
                                    break;
                                }
                            }

                            if (!isBothThere)
                            {
                                team.battingOrder[j] = (Batter)playerAfter;
                            }
                            break;
                        }
                        else if (team.battingOrder[j] == playerAfter)
                        {
                            for (int k = 0; k < team.battingOrder.d.Count; ++k)
                            {
                                if (team.battingOrder[k] == playerBefore)
                                {
                                    team.battingOrder[j] = (Batter)playerBefore;
                                    team.battingOrder[k] = (Batter)playerAfter;

                                    isBothThere = true;
                                    break;
                                }
                            }

                            if (!isBothThere)
                            {
                                team.battingOrder[j] = (Batter)playerBefore;
                            }
                            break;
                        }
                    }
                }

                break;
            }
        };

        NotificationExample noti = GameManager.notificationPanel.GetComponent<NotificationExample>();
        noti.descriptionText = "Successfully changed " + playerBefore.playerData.GetData(PlayerData.PP.NAME) + " to " + playerAfter.playerData.GetData(PlayerData.PP.NAME) + ".";
        noti.ShowNotification();
    }
}
