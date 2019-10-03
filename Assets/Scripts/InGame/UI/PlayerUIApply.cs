using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIApply : MonoBehaviour
{
    public TotalMovement TotalMovement;
    
    public void SetPlayers(bool conditionOnly = false)
    {
        Player.Position[] positions =
        {
            Player.Position.CATCHER,
            InGameManager.currentPitcher.playerData.GetData(PlayerData.PP.POSITION),
            Player.Position.FIRST_BASE_MAN,
            Player.Position.RIGHT_FIELDER,
            Player.Position.SECOND_BASE_MAN,
            Player.Position.CENTER_FIELDER,
            Player.Position.SHORT_STOP,
            Player.Position.LEFT_FIELDER,
            Player.Position.THIRD_BASE_MAN
        };

        Dictionary<Player.Position, GameObject> dict = new Dictionary<Player.Position, GameObject>();
        for (int i = 0; i < TotalMovement.inGamObjs.Count; ++i)
        {
            dict.Add(positions[i], TotalMovement.inGamObjs[i]);
        }
        
        foreach(KeyValuePair<Player.Position, GameObject> pair in dict)
        {
            InGamePlayerObject InGamePlayerObject = new InGamePlayerObject(pair.Value);
            if(conditionOnly)
            {
                if (Player.metaPosition[Player.MetaPosition.STARTER_PITCHER].Contains(pair.Key) || Player.metaPosition[Player.MetaPosition.RELIEF_PITCHER].Contains(pair.Key))
                {
                    InGamePlayerObject.SetByPlayerConditionOnly(InGameManager.currentPitcher);
                }
                else
                {
                    InGamePlayerObject.SetByPlayerConditionOnly(InGameManager.currentDefend.GetStartingBatterByPosition(pair.Key));
                }
            }
            else
            {
                if (Player.metaPosition[Player.MetaPosition.STARTER_PITCHER].Contains(pair.Key) || Player.metaPosition[Player.MetaPosition.RELIEF_PITCHER].Contains(pair.Key))
                {
                    InGamePlayerObject.SetByPlayer(InGameManager.currentPitcher, InGameManager.currentDefend);
                }
                else
                {
                    InGamePlayerObject.SetByPlayer(InGameManager.currentDefend.GetStartingBatterByPosition(pair.Key), InGameManager.currentDefend);
                }
            }
        }
    }
}
