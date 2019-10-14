using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingsPanel : MonoBehaviour
{
    public GameManager GameManager;

    public void RefreshByPlayer(Player player)
    {
        //Training
        //Player info
        GameManager.TP_playerName.text = player.playerData.GetData(PlayerData.PP.NAME);
        GameManager.TP_number.text = ((int)player.playerData.GetData(PlayerData.PP.NUMBER)).ToString();
        GameManager.TP_AccentPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.TP_AccentPanel2.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.TP_position.text = Player.positionString[(int)player.playerData.GetData(PlayerData.PP.POSITION)];


        //Remove remaining training panels.
        for (int i = 0; i < GameManager.TP_content.transform.childCount; ++i)
        {
            Destroy(GameManager.TP_content.transform.GetChild(i).gameObject);
        }

        //Instantiate training panels.
        foreach (Training.Train train in Training.trainAll)
        {
            GameManager.TrainingInstantiate(Trainings.trainings[train], player);
        }

        if (player.GetType() == typeof(Pitcher))
        {
            foreach (Training.Train train in Training.trainPitcher)
            {
                GameManager.TrainingInstantiate(Trainings.trainings[train], player);
            }
        }
        else if (player.GetType() == typeof(Batter))
        {
            foreach (Training.Train train in Training.trainBatter)
            {
                GameManager.TrainingInstantiate(Trainings.trainings[train], player);
            }
        }
    }
}
