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
        for (int i = 0; i < Values.myTeam.startingMembers.d.Count; ++i)
        {
            if (Values.myTeam.startingMembers[i].Value.playerData.GetData(PlayerData.PP.NAME) == playerFirst.playerData.GetData(PlayerData.PP.NAME) &&
                Values.myTeam.startingMembers[i].Value.playerData.GetData(PlayerData.PP.NUMBER) == playerFirst.playerData.GetData(PlayerData.PP.NUMBER))
            {
                Debug.Log(playerFirst.isSubstitute + " " + playerSecond.isSubstitute);

                bool tempStartingmember = playerSecond.isStartingMember == true;        //for deep copy.
                bool tempSub = playerSecond.isSubstitute == true;                       //for deep copy.
                playerSecond.isStartingMember = playerFirst.isStartingMember == true;   //for deep copy.
                playerSecond.isSubstitute = playerFirst.isSubstitute == true;           //for deep copy.
                playerFirst.isStartingMember = tempStartingmember;
                playerFirst.isSubstitute = tempSub;
                playerFirst.playerData.SetData(PlayerData.PP.POSITION, positionSecond);
                playerSecond.playerData.SetData(PlayerData.PP.POSITION, positionFirst);
                
                for (int j = 0; j < Values.myTeam.startingMembers.d.Count; ++j)
                {
                    if (Values.myTeam.startingMembers[j].Value.playerData.GetData(PlayerData.PP.NAME) == playerSecond.playerData.GetData(PlayerData.PP.NAME) &&
                        Values.myTeam.startingMembers[j].Value.playerData.GetData(PlayerData.PP.NUMBER) == playerSecond.playerData.GetData(PlayerData.PP.NUMBER))
                    {
                        Values.myTeam.startingMembers[j] = new KeyValuePair<Player.Position, Player>(Values.myTeam.startingMembers[j].Key, playerFirst);
                        break;
                    }
                }
                Values.myTeam.startingMembers[i] = new KeyValuePair<Player.Position, Player>(Values.myTeam.startingMembers[i].Key, playerSecond);
                break;
            }
        };

        NotificationExample noti = GameManager.notificationPanel.GetComponent<NotificationExample>();
        noti.descriptionText = "Successfully changed " + playerFirst.playerData.GetData(PlayerData.PP.NAME) + " to " + playerSecond.playerData.GetData(PlayerData.PP.NAME) + ".";
        noti.ShowNotification();

        BackToFieldViewButton.OnClick();
    }
}
