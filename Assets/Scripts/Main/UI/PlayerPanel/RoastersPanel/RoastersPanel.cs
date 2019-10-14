using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoastersPanel : MonoBehaviour
{
    public GameObject filter;
    public GameObject filterText;
    public PlayerListObject listObject;
    public GameManager GameManager;

    private void OnEnable()
    {
        filter.SetActive(false);
        filterText.SetActive(false);

        GameManager.RP_statsPanel.SetActive(false);
        GameManager.RP_fieldViewPanel.SetActive(true);
        GameManager.RP_middlePanel.SetActive(false);
        GameManager.RP_pitchersPanel.SetActive(true);
        
        Refresh();
    }

    private void OnDisable()
    {
        filter.SetActive(true);
        filterText.SetActive(true);
        
        listObject.RefreshPlayerList(GameManager.mode, GameManager.sortMode);
    }
    
    public void RefreshByPlayer(Player player)
    {
        GameManager.RP_statsPanel.SetActive(true);
        GameManager.RP_fieldViewPanel.SetActive(false);
        GameManager.RP_middlePanel.SetActive(true);
        GameManager.RP_pitchersPanel.SetActive(false);

        int instantiatedAmount = 0; // GameManager.RefreshPlayerList(player.playerData.GetData(PlayerData.PP.META_POSITION), GameManager.sortMode, Filter.StartingMemberFilter.ALL, PlayerView.COMPARE);

        //Removes myself.
        for (int i = 0; i < GameManager.playerContent.transform.childCount; ++i)
        {
            GameObject playerObject = GameManager.playerContent.transform.GetChild(i).gameObject;
            Player playerItem = playerObject.GetComponent<PlayerList>().player;
            if (player.playerData.GetData(PlayerData.PP.NAME) == playerItem.playerData.GetData(PlayerData.PP.NAME))
            {
                Destroy(playerObject);
                instantiatedAmount--;
                //break;
            }
        }

        if (instantiatedAmount == 0)
        {
            NotificationExample noti = GameManager.notificationPanel.GetComponent<NotificationExample>();
            noti.descriptionText = "There is no substitute player available.";
            noti.ShowNotification();
            GameManager.RP_statsPanel.SetActive(false);
            GameManager.RP_fieldViewPanel.SetActive(true);
            GameManager.RP_middlePanel.SetActive(false);
            GameManager.RP_pitchersPanel.SetActive(true);
            //RoastersPanel.Refresh(GameManager);
            return;
        }

        //Set change target.
        ChangeButton.playerFirst = player;
        ChangeButton.positionFirst = player.playerData.GetData(PlayerData.PP.POSITION);

        //Player Info
        GameManager.RP_nameFirst.text = player.playerData.GetData(PlayerData.PP.NAME);
        GameManager.RP_numberFirst.text = player.playerData.GetData(PlayerData.PP.NUMBER).ToString();
        GameManager.RP_accentPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.RP_accentPanel2.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        GameManager.RP_position.text = Player.positionString[(int)player.playerData.GetData(PlayerData.PP.POSITION)];

        //Diamond
        GameManager.RP_middlePanel.GetComponent<Image>().color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];

        int counter = 0;
        foreach (KeyValuePair<string, float> stat in player.finalStats.d)
        {
            float statValue = stat.Value;

            GameManager.RP_diamond.VerticesDistances[counter] = Mathf.FloorToInt(statValue) / 100f;
            GameManager.RP_titles[counter].text = stat.Key;
            GameManager.RP_valueSeconds[counter].text = Mathf.FloorToInt(statValue).ToString();

            GameManager.RP_valueSeconds[counter].color = Player.ColorPicker(statValue);
            GameManager.RP_valueSeconds[counter].alpha = Player.AlphaPicker(statValue, 0.7f, new bool[] { true, false, false, false, false });

            counter++;
        }

        GameManager.RP_diamond.DrawPolygon(GameManager.RP_diamond.sides, GameManager.RP_diamond.VerticesDistances, GameManager.RP_diamond.rotation);
        GameManager.RP_diamond.SetAllDirty();

        //Overall
        GameManager.RP_overallFirst.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.OVERALL)).ToString();


        for (int i = 0; i < Player.statRange.Count; ++i)
        {
            float value = Mathf.FloorToInt(player.GetOverall());

            GameManager.RP_overallPanelFirst.color = Player.ColorPicker(value);
            GameManager.RP_overallAccentPanelFirst.color = Player.ColorPicker(value);
            GameManager.RP_overallBGFirst.color = Player.ColorPicker(value);
            GameManager.RP_overallFirst.alpha = Player.AlphaPicker(value, 0.7f, new bool[] { true, false, false, false, false });
            GameManager.RP_overallFirst.alpha = Player.AlphaPicker(value, 1f, new bool[] { false, true, true, true, true });
        }

        //Stats
        for (int i = 0; i < GameManager.RP_content.transform.childCount; ++i)
        {
            Destroy(GameManager.RP_content.transform.GetChild(i).gameObject);
        }

        foreach (KeyValuePair<PlayerData.PP, object> stat in player.playerData.data.d)
        {
            if (!PlayerData.statPrefs.Contains(stat.Key)) continue;

            SkillPrefab.SkillInstantiate(player, stat.Key, GameManager.RP_content.transform);

            if (PlayerData.serializableDictPrefs.ContainsKey(stat.Key))
            {
                foreach (KeyValuePair<PlayerData.PP, float> valuePair in ((SerializableDictPP)(stat.Value)).d)
                {
                    SkillPrefab.SkillInstantiate(player, valuePair.Key, GameManager.RP_content.transform, default, true);
                }
            }
        }
    }

    public void CompareByPlayer(Player player)
    {
        //Sets change target.
        ChangeButton.playerSecond = player;
        ChangeButton.positionSecond = player.playerData.GetData(PlayerData.PP.POSITION);

        //Player Info
        GameManager.RP_nameSecond.text = player.playerData.GetData(PlayerData.PP.NAME);
        GameManager.RP_numberSecond.text = player.playerData.GetData(PlayerData.PP.NUMBER).ToString();

        //Diamond
        int counter = 0;
        foreach (KeyValuePair<string, float> stat in player.finalStats.d)
        {
            GameManager.RP_diamondSecond.VerticesDistances[counter] = Mathf.FloorToInt(stat.Value) / 100f;
            GameManager.RP_values[counter].text = Mathf.FloorToInt(stat.Value).ToString();

            float statValue = stat.Value;

            GameManager.RP_values[counter].color = Player.ColorPicker(statValue);
            GameManager.RP_values[counter].alpha = Player.AlphaPicker(statValue, 0.7f, new bool[] { true, false, false, false, false });

            counter++;
        }

        GameManager.RP_diamondSecond.DrawPolygon(GameManager.RP_diamondSecond.sides, GameManager.RP_diamondSecond.VerticesDistances, GameManager.RP_diamondSecond.rotation);
        GameManager.RP_diamondSecond.SetAllDirty();

        //Overall
        GameManager.RP_overallSecond.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.OVERALL)).ToString();

        float value = Mathf.FloorToInt(player.GetOverall());

        GameManager.RP_overallPanelSecond.color = Player.ColorPicker(value);
        GameManager.RP_overallAccentPanelSecond.color = Player.ColorPicker(value);
        GameManager.RP_overallBGSecond.color = Player.ColorPicker(value);
        GameManager.RP_overallSecond.alpha = Player.AlphaPicker(value, 0.7f, new bool[] { true, false, false, false, false });
        GameManager.RP_overallSecond.alpha = Player.AlphaPicker(value, 1f, new bool[] { false, true, true, true, true });

        //Stats
        for (int i = 0; i < GameManager.RP_content.transform.childCount; ++i)
        {
            Transform statTranform = GameManager.RP_content.transform.GetChild(i);

            for (int j = 0; j < PlayerData.PPString.Count; ++j)
            {
                if (statTranform.GetChild(3).GetComponent<TextMeshProUGUI>().text == PlayerData.PPString[j])
                {
                    int statValue;
                    if (statTranform.GetComponent<StatComponent>().isSmall)
                    {
                        statValue = Mathf.FloorToInt(player.playerData.GetDictData((PlayerData.PP)j));
                    }
                    else
                    {
                        statValue = Mathf.FloorToInt((float)player.playerData.GetData((PlayerData.PP)j));
                    }

                    statTranform.GetChild(0).GetComponent<Image>().color = Player.ColorPicker(statValue);

                    statTranform.GetChild(4).GetComponent<TextMeshProUGUI>().text = statValue.ToString();
                    statTranform.GetChild(2).GetComponent<Image>().fillAmount = statValue / 100f;

                    if (statValue < int.Parse(statTranform.GetChild(5).GetComponent<TextMeshProUGUI>().text))
                    {
                        statTranform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Colors.red;
                    }
                    else
                    {
                        statTranform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.white;
                    }

                    for (int k = 0; k < Player.statRange.Count; ++k)
                    {
                        if (statValue < Player.statRange[k])
                        {
                            statTranform.transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = Player.statColor[k];
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    public void Refresh()
    {
        if (RoasterPitchersPanelButton.focusedObject.GetComponent<RoasterPitchersPanelButton>().buttonName == "Pitcher")
        {
            listObject.RefreshPlayerList(Filter.Mode.PITCHERS, SortDropdown.SortMode.POSITION, GameManager.RP_pitchersPanelContent, Filter.StartingMemberFilter.MEMBER_ONLY, PlayerList.PlayerView.ROASTER);
        }
        else if (RoasterPitchersPanelButton.focusedObject.GetComponent<RoasterPitchersPanelButton>().buttonName == "Substitutes")
        {
            listObject.RefreshPlayerList(Filter.Mode.ALL, SortDropdown.SortMode.POSITION, GameManager.RP_pitchersPanelContent, Filter.StartingMemberFilter.SUB_ONLY, PlayerList.PlayerView.ROASTER);
        }
    }
}
