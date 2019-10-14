using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsPanel : MonoBehaviour
{
    [Header("Game Management")]
    public GameManager GameManager;

    public void RefreshByPlayer(Player player)
    {
        //Statistic Panel
        //Player Info
        GameManager.SP_playerName.text = player.playerData.GetData(PlayerData.PP.NAME);
        GameManager.SP_number.text = ((int)player.playerData.GetData(PlayerData.PP.NUMBER)).ToString();
        GameManager.SP_AccentPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.SP_AccentPanel2.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.SP_position.text = Player.positionString[(int)player.playerData.GetData(PlayerData.PP.POSITION)];

        //Statistics
        //Save object before get destroyed.
        PlayerStatistics.PS focusedStat = default;
        bool focusedFlag = false;
        if (StatisticPanel.focusedObject != null)
        {
            focusedStat = StatisticPanel.focusedObject.GetComponent<StatisticPanel>().stat;
            focusedFlag = true;
        }

        //Remove remaining gameobjects.
        int count = GameManager.SP_content.transform.childCount;
        for (int i = 0; i < count; ++i)
        {
            DestroyImmediate(GameManager.SP_content.transform.GetChild(0).gameObject);
            //I used DestroyImmediate cuz gonna use childCount right after this line.
            //Destroy doesn't actually 'destroy' its child immediately :(
        };

        //Instantiate new gameobjects.
        foreach (KeyValuePair<PlayerStatistics.PS, float> statPair in player.stats.seasonStats[Values.date.Year].d)
        {
            StatisticPrefab.StatisticsInstantiate(player, statPair.Key, GameManager.SP_content.transform);
        }

        //Refresh focused object if possible.
        if (focusedFlag)
        {
            bool isStatThere = false;
            for (int i = 0; i < GameManager.SP_content.transform.childCount; ++i)
            {
                GameObject statObject = GameManager.SP_content.transform.GetChild(i).gameObject;
                StatisticPanel statPanel = statObject.GetComponent<StatisticPanel>();

                if (statPanel.stat == focusedStat)
                {
                    isStatThere = true;
                    statPanel.OnClick();
                    break;
                }
            };
            if (!isStatThere)
            {
                GameManager.SP_graphPanel.SetActive(false);
            }
        }
    }
}
