using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIApply : MonoBehaviour
{
    public TotalMovement TotalMovement;

    public void Start()
    {
        SetPlayers();
    }

    public void SetPlayers()
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
            InGamePlayerObject.SetByPlayer(InGameManager.currentDefend.GetPlayerByPosition(pair.Key), InGameManager.currentDefend);
        }
    }
}
