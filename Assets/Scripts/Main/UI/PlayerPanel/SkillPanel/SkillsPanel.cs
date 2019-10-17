using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsPanel : MonoBehaviour
{
    [Header("Game Management")]
    public GameManager GameManager;
    public Transform contentTransform;

    [Header("Player List")]
    public PlayerListObject PlayerListObject;

    public void OnEnable()
    {
        PlayerListObject.RefreshPlayerList(GameManager.mode, GameManager.sortMode, true, PlayerListObject.currentStartingMemberFilter);
    }

    public void RefreshByPlayer(Player player)
    {
        //playerinfo
        GameManager.PIP_playerName.text = player.playerData.GetData(PlayerData.PP.NAME);
        GameManager.PIP_number.text = player.playerData.GetData(PlayerData.PP.NUMBER).ToString();
        GameManager.PIP_AccentPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.PIP_AccentPanel2.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.PIP_postion.text = Player.positionString[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.PIP_height.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.HEIGHT)).ToString() + "cm";
        GameManager.PIP_weight.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.WEIGHT)).ToString() + "kg";
        GameManager.PIP_age.text = ((int)(player.GetAge().Days / 365.2425)).ToString();

        if (player.playerData.GetData(PlayerData.PP.IS_LEFT_HANDED))
        {
            GameManager.PIP_leftHanded.color = Colors.yellow;
            GameManager.PIP_leftHanded.text = "Left-handed";
        }
        else
        {
            GameManager.PIP_leftHanded.color = Color.white;
            GameManager.PIP_leftHanded.text = "Right-Handed";
        }

        //Diamond
        GameManager.PIP_diamondBGPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];

        int counter = 0;
        foreach (KeyValuePair<string, float> stat in player.finalStats.d)
        {
            float statValue = stat.Value;
            GameManager.PIP_uiPolygon.VerticesDistances[counter] = Mathf.FloorToInt(statValue) / 100f;
            GameManager.PIP_stats[counter].text = stat.Key;
            GameManager.PIP_values[counter].text = Mathf.FloorToInt(statValue).ToString();

            GameManager.PIP_values[counter].color = Player.ColorPicker(statValue);
            GameManager.PIP_values[counter].alpha = Player.AlphaPicker(statValue, 0.7f, new bool[] { true, false, false, false, false });

            counter++;
        }
        if (counter == 3)
        {
            GameManager.PIP_uiPolygon.VerticesDistances[3] = 0;
            GameManager.PIP_stats[3].text = "";
            GameManager.PIP_values[3].text = "";
        }

        GameManager.PIP_uiPolygon.DrawPolygon(GameManager.PIP_uiPolygon.sides, GameManager.PIP_uiPolygon.VerticesDistances, GameManager.PIP_uiPolygon.rotation);
        GameManager.PIP_uiPolygon.SetAllDirty();

        //Overall
        GameManager.PIP_overall.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.OVERALL)).ToString();

        float value = Mathf.FloorToInt(player.GetOverall());
        GameManager.PIP_overallPanel.color = Player.ColorPicker(value);
        GameManager.PIP_overallAccentPanel.color = Player.ColorPicker(Mathf.FloorToInt(value));
        GameManager.PIP_overall.alpha = Player.AlphaPicker(value, 0.7f, new bool[] { true, false, false, false, false });
        GameManager.PIP_overall.alpha = Player.AlphaPicker(value, 1f, new bool[] { false, true, true, true, true });

        //Stats
        for (int i = 0; i < GameManager.PIP_statPanel.transform.childCount; ++i)
        {
            Destroy(GameManager.PIP_statPanel.transform.GetChild(i).gameObject);
        }

        foreach (KeyValuePair<PlayerData.PP, object> stat in player.playerData.data.d)
        {
            if (!PlayerData.statPrefs.Contains(stat.Key)) continue;

            SkillPrefab.SkillInstantiate(player, stat.Key, contentTransform);

            if (PlayerData.serializableDictPrefs.ContainsKey(stat.Key))
            {
                foreach (KeyValuePair<PlayerData.PP, float> valuePair in ((SerializableDictPP)stat.Value).d)
                {
                    SkillPrefab.SkillInstantiate(player, valuePair.Key, contentTransform, default, true);
                }
            }

        }
    }
}
