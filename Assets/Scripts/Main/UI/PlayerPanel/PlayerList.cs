using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class PlayerList : MonoBehaviour {
    
    //prefabs
    public GameObject statComponent;
    public GameObject statSmallComponent;

    //public static variables
    public static GameObject focusedObject;

    //player data
    public Player player;
    public Player playerSecond;

    private void OnEnable()
    {
        StartCoroutine("Check");
    }

    private void OnDisable()
    {
        StopCoroutine("Check");
        //focusedObject = null;
    }

    IEnumerator Check()
    {
        yield return new WaitForFixedUpdate();
        if (focusedObject == null && !GameObject.Find("PitchersPanel"))
        {
            focusedObject = gameObject;
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (PlayerTabButton.focusedObject != null) PlayerTabButton.focusedObject.SetActive(true);
            else gameManager.playersPanel.SetActive(true);

            foreach(GameObject diamondBG in gameManager.PIP_diamondBGs)
            {
                diamondBG.SetActive(false);
                yield return new WaitForFixedUpdate();
                diamondBG.SetActive(true);
            }

            gameManager.RP_diamond.gameObject.SetActive(false);
            gameManager.RP_diamondSecond.gameObject.SetActive(false);
            yield return new WaitForFixedUpdate();
            gameManager.RP_diamond.gameObject.SetActive(true);
            gameManager.RP_diamondSecond.gameObject.SetActive(true);

            if(GameObject.Find("RoastersPanel"))
            {
                OnClick(gameManager, PlayerView.COMPARE);
            }
            else
            {
                OnClick(gameManager);
            }
        }
    }

    public enum PlayerView
    {
        SKILLS_AND_STATISTICS, ROASTER, COMPARE
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameManager"></param>
    /// <param name="playerView">Which view you have now.</param>
    public void OnClick(GameManager gameManager, PlayerView playerView = PlayerView.SKILLS_AND_STATISTICS, bool isFieldView = false) {
        //Toggle
        if(focusedObject != null) {
            focusedObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameManager.skillPanel.SetActive(true);
        }

        if (!isFieldView)
        {
            focusedObject = gameObject;
            GetComponent<Button>().interactable = false;
        }

        //Data application

        if(playerView == PlayerView.SKILLS_AND_STATISTICS)
        {
            //playerinfo
            gameManager.PIP_playerName.text = player.playerData.GetData(PlayerData.PP.NAME);
            gameManager.PIP_number.text = player.playerData.GetData(PlayerData.PP.NUMBER).ToString();
            gameManager.PIP_AccentPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.PIP_AccentPanel2.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.PIP_postion.text = Player.positionString[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.PIP_height.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.HEIGHT)).ToString() + "cm";
            gameManager.PIP_weight.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.WEIGHT)).ToString() + "kg";
            gameManager.PIP_age.text = ((int)(player.GetAge().Days / 365.2425)).ToString();

            if (player.playerData.GetData(PlayerData.PP.IS_LEFT_HANDED))
            {
                gameManager.PIP_leftHanded.color = Colors.yellow;
                gameManager.PIP_leftHanded.text = "Left-handed";
            }
            else
            {
                gameManager.PIP_leftHanded.color = Color.white;
                gameManager.PIP_leftHanded.text = "Right-Handed";
            }

            //Diamond
            gameManager.PIP_diamondBGPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];

            int counter = 0;
            foreach (KeyValuePair<string, float> stat in player.finalStats.d)
            {
                float statValue = stat.Value;
                gameManager.PIP_uiPolygon.VerticesDistances[counter] = Mathf.FloorToInt(statValue) / 100f;
                gameManager.PIP_stats[counter].text = stat.Key;
                gameManager.PIP_values[counter].text = Mathf.FloorToInt(statValue).ToString();

                gameManager.PIP_values[counter].color = Player.ColorPicker(statValue);
                gameManager.PIP_values[counter].alpha = Player.AlphaPicker(statValue, 0.7f, new bool[] { true, false, false, false, false });
                
                counter++;
            }
            if (counter == 3)
            {
                gameManager.PIP_uiPolygon.VerticesDistances[3] = 0;
                gameManager.PIP_stats[3].text = "";
                gameManager.PIP_values[3].text = "";
            }

            gameManager.PIP_uiPolygon.DrawPolygon(gameManager.PIP_uiPolygon.sides, gameManager.PIP_uiPolygon.VerticesDistances, gameManager.PIP_uiPolygon.rotation);
            gameManager.PIP_uiPolygon.SetAllDirty();

            //Overall
            gameManager.PIP_overall.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.OVERALL)).ToString();

            float value = Mathf.FloorToInt(player.GetOverall());
            gameManager.PIP_overallPanel.color = Player.ColorPicker(value);
            gameManager.PIP_overallAccentPanel.color = Player.ColorPicker(Mathf.FloorToInt(value));
            gameManager.PIP_overall.alpha = Player.AlphaPicker(value, 0.7f, new bool[] { true, false, false, false, false });
            gameManager.PIP_overall.alpha = Player.AlphaPicker(value, 1f, new bool[] { false, true, true, true, true });
            
            //Stats
            for (int i = 0; i < gameManager.PIP_statPanel.transform.childCount; ++i)
            {
                Destroy(gameManager.PIP_statPanel.transform.GetChild(i).gameObject);
            }

            foreach (KeyValuePair<PlayerData.PP, object> stat in player.playerData.data.d)
            {
                if (!PlayerData.statPrefs.Contains(stat.Key)) continue;

                GameObject statObject = Instantiate(statComponent.gameObject, gameManager.PIP_statPanel.transform);

                float statValue = player.playerData.GetData(stat.Key);

                statObject.transform.GetChild(0).GetComponent<Image>().color = Player.ColorPicker(statValue);
                statObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Player.ColorPicker(statValue);

                statObject.transform.GetChild(1).GetComponent<Image>().fillAmount = Mathf.FloorToInt(statValue) / 100f;
                statObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerData.PPString[(int)stat.Key];
                statObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(statValue).ToString();
                TextMeshProUGUI valueSecond = statObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
                Image arrow = statObject.transform.GetChild(6).GetComponent<Image>();

                statObject.transform.GetChild(2).gameObject.SetActive(false);
                valueSecond.gameObject.SetActive(false);
                arrow.gameObject.SetActive(false);

                if (PlayerData.serializableDictPrefs.ContainsKey(stat.Key))
                {
                    foreach (KeyValuePair<PlayerData.PP, float> valuePair in ((SerializableDictPP)stat.Value).d)
                    {
                        GameObject statSmallObject = Instantiate(statSmallComponent, gameManager.PIP_statPanel.transform);
                        float statSmallValue = valuePair.Value;

                        statSmallObject.transform.GetChild(0).GetComponent<Image>().color = Player.ColorPicker(statSmallValue);
                        statSmallObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Player.ColorPicker(statSmallValue);

                        statSmallObject.transform.GetChild(1).GetComponent<Image>().fillAmount = Mathf.FloorToInt(statSmallValue) / 100f;
                        statSmallObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerData.PPString[(int)valuePair.Key];
                        statSmallObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(statSmallValue).ToString();
                        TextMeshProUGUI valueSmallSecond = statSmallObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
                        Image arrowSmall = statSmallObject.transform.GetChild(6).GetComponent<Image>();

                        statSmallObject.transform.GetChild(2).gameObject.SetActive(false);
                        valueSmallSecond.gameObject.SetActive(false);
                        arrowSmall.gameObject.SetActive(false);
                    }
                }
                
            }

            //Statistic Panel
            //Player Info
            gameManager.SP_playerName.text = player.playerData.GetData(PlayerData.PP.NAME);
            gameManager.SP_number.text = ((int)player.playerData.GetData(PlayerData.PP.NUMBER)).ToString();
            gameManager.SP_AccentPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.SP_AccentPanel2.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.SP_position.text = Player.positionString[(int)player.playerData.GetData(PlayerData.PP.POSITION)];

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
            int count = gameManager.SP_content.transform.childCount;
            for (int i = 0; i < count; ++i)
            {
                DestroyImmediate(gameManager.SP_content.transform.GetChild(0).gameObject);
                //I used DestroyImmediate cuz gonna use childCount right after this line.
                //Destroy doesn't actually 'destroy' its child immediately :(
            };
            
            //Instantiate new gameobjects.
            foreach(KeyValuePair<PlayerStatistics.PS, float> statPair in player.stats.seasonStats[Values.date.Year].d)
            {
                gameManager.StatisticsInstantiate(statPair.Key, player.stats.GetSeason(statPair.Key, Values.date.Year), PlayerStatistics.StatAverage(statPair.Key, Values.date.Year), 1);
            }

            //Refresh focused object if possible.
            if (focusedFlag)
            {
                bool isStatThere = false;
                for (int i = 0; i < gameManager.SP_content.transform.childCount; ++i)
                {
                    GameObject statObject = gameManager.SP_content.transform.GetChild(i).gameObject;
                    StatisticPanel statPanel = statObject.GetComponent<StatisticPanel>();

                    if (statPanel.stat == focusedStat)
                    {
                        isStatThere = true;
                        statPanel.OnClick();
                        break;
                    }
                };
                if(!isStatThere)
                {
                    gameManager.SP_graphPanel.SetActive(false);
                }
            }

            //Training
            //Player info
            gameManager.TP_playerName.text = player.playerData.GetData(PlayerData.PP.NAME);
            gameManager.TP_number.text = ((int)player.playerData.GetData(PlayerData.PP.NUMBER)).ToString();
            gameManager.TP_AccentPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.TP_AccentPanel2.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.TP_position.text = Player.positionString[(int)player.playerData.GetData(PlayerData.PP.POSITION)];


            //Remove remaining training panels.
            for(int i = 0; i < gameManager.TP_content.transform.childCount; ++i)
            {
                Destroy(gameManager.TP_content.transform.GetChild(i).gameObject);
            }

            //Instantiate training panels.
            foreach (Training.Train train in Training.trainAll)
            {
                gameManager.TrainingInstantiate(Trainings.trainings[train], player);
            }

            if(player.GetType() == typeof(Pitcher))
            {
                foreach(Training.Train train in Training.trainPitcher)
                {
                    gameManager.TrainingInstantiate(Trainings.trainings[train], player);
                }
            }
            else if (player.GetType() == typeof(Batter))
            {
                foreach (Training.Train train in Training.trainBatter)
                {
                    gameManager.TrainingInstantiate(Trainings.trainings[train], player);
                }
            }
        }
        else if (playerView == PlayerView.ROASTER)
        {
            gameManager.RP_statsPanel.SetActive(true);
            gameManager.RP_fieldViewPanel.SetActive(false);
            gameManager.RP_middlePanel.SetActive(true);
            gameManager.RP_pitchersPanel.SetActive(false);

            int instantiatedAmount = gameManager.RefreshPlayerList(player.playerData.GetData(PlayerData.PP.META_POSITION), gameManager.sortMode, GameManager.StartingMemberFilter.ALL, PlayerView.COMPARE);

            //Removes myself.
            for (int i = 0; i < gameManager.playerContent.transform.childCount; ++i)
            {
                GameObject playerObject = gameManager.playerContent.transform.GetChild(i).gameObject;
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
                NotificationExample noti = gameManager.notificationPanel.GetComponent<NotificationExample>();
                noti.descriptionText = "There is no substitute player available.";
                noti.ShowNotification();
                gameManager.RP_statsPanel.SetActive(false);
                gameManager.RP_fieldViewPanel.SetActive(true);
                gameManager.RP_middlePanel.SetActive(false);
                gameManager.RP_pitchersPanel.SetActive(true);
                RoastersPanel.Refresh(gameManager);
                return;
            }

            //Set change target.
            ChangeButton.playerFirst = player;
            ChangeButton.positionFirst = player.playerData.GetData(PlayerData.PP.POSITION);

            //Player Info
            gameManager.RP_nameFirst.text = player.playerData.GetData(PlayerData.PP.NAME);
            gameManager.RP_numberFirst.text = player.playerData.GetData(PlayerData.PP.NUMBER).ToString();
            gameManager.RP_accentPanel.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.RP_accentPanel2.color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
            gameManager.RP_position.text = Player.positionString[(int)player.playerData.GetData(PlayerData.PP.POSITION)];

            //Diamond
            gameManager.RP_middlePanel.GetComponent<Image>().color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)];

            int counter = 0;
            foreach (KeyValuePair<string, float> stat in player.finalStats.d)
            {
                float statValue = stat.Value;

                gameManager.RP_diamond.VerticesDistances[counter] = Mathf.FloorToInt(statValue) / 100f;
                gameManager.RP_titles[counter].text = stat.Key;
                gameManager.RP_valueSeconds[counter].text = Mathf.FloorToInt(statValue).ToString();

                gameManager.RP_valueSeconds[counter].color = Player.ColorPicker(statValue);
                gameManager.RP_valueSeconds[counter].alpha = Player.AlphaPicker(statValue, 0.7f, new bool[]{ true, false, false, false , false });
                
                counter++;
            }
            if (counter == 3)
            {
                gameManager.RP_diamond.VerticesDistances[3] = 0;
                gameManager.RP_titles[3].text = "";
                gameManager.RP_valueSeconds[3].text = "";
            }

            gameManager.RP_diamond.DrawPolygon(gameManager.RP_diamond.sides, gameManager.RP_diamond.VerticesDistances, gameManager.RP_diamond.rotation);
            gameManager.RP_diamond.SetAllDirty();

            //Overall
            gameManager.RP_overallFirst.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.OVERALL)).ToString();


            for (int i = 0; i < Player.statRange.Count; ++i)
            {
                float value = Mathf.FloorToInt(player.GetOverall());

                gameManager.RP_overallPanelFirst.color = Player.ColorPicker(value);
                gameManager.RP_overallAccentPanelFirst.color = Player.ColorPicker(value);
                gameManager.RP_overallBGFirst.color = Player.ColorPicker(value);
                gameManager.RP_overallFirst.alpha = Player.AlphaPicker(value, 0.7f, new bool[] { true, false, false, false, false });
                gameManager.RP_overallFirst.alpha = Player.AlphaPicker(value, 1f, new bool[] { false, true, true, true, true });
            }

            //Stats
            for (int i = 0; i < gameManager.RP_content.transform.childCount; ++i)
            {
                Destroy(gameManager.RP_content.transform.GetChild(i).gameObject);
            }

            foreach (KeyValuePair<PlayerData.PP, object> stat in player.playerData.data.d)
            {
                if (!PlayerData.statPrefs.Contains(stat.Key)) continue;

                float statValue = player.playerData.GetData(stat.Key);
                
                GameObject statObject = Instantiate(statComponent.gameObject, gameManager.RP_content.transform);

                statObject.transform.GetChild(0).GetComponent<Image>().color = Colors.primaryDark;
                statObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().color = Player.ColorPicker(statValue);

                statObject.transform.GetChild(1).GetComponent<Image>().fillAmount = Mathf.FloorToInt((float)statValue) / 100f;
                statObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerData.PPString[(int)stat.Key];
                statObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt((float)statValue).ToString();
                TextMeshProUGUI valueSecond = statObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
                Image arrow = statObject.transform.GetChild(6).GetComponent<Image>();

                statObject.transform.GetChild(2).gameObject.SetActive(true);
                valueSecond.gameObject.SetActive(true);
                arrow.gameObject.SetActive(true);

                if (PlayerData.serializableDictPrefs.ContainsKey(stat.Key))
                {
                    foreach (KeyValuePair<PlayerData.PP, float> valuePair in ((SerializableDictPP)(stat.Value)).d)
                    {
                        GameObject statSmallObject = Instantiate(statSmallComponent, gameManager.RP_content.transform);
                        float statSmallValue = valuePair.Value;

                        statSmallObject.transform.GetChild(0).GetComponent<Image>().color = Player.ColorPicker(statSmallValue);
                        statSmallObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().color = Player.ColorPicker(statSmallValue);

                        statSmallObject.transform.GetChild(1).GetComponent<Image>().fillAmount = Mathf.FloorToInt(statSmallValue) / 100f;
                        statSmallObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerData.PPString[(int)valuePair.Key];
                        statSmallObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(statSmallValue).ToString();
                        TextMeshProUGUI valueSmallSecond = statSmallObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
                        Image arrowSmall = statSmallObject.transform.GetChild(6).GetComponent<Image>();

                        statSmallObject.transform.GetChild(2).gameObject.SetActive(true);
                        valueSmallSecond.gameObject.SetActive(true);
                        arrowSmall.gameObject.SetActive(true);
                    }
                }
            }
        }
        else if (playerView == PlayerView.COMPARE)
        {
            //Sets change target.
            ChangeButton.playerSecond = player;
            ChangeButton.positionSecond = player.playerData.GetData(PlayerData.PP.POSITION);

            //Player Info
            gameManager.RP_nameSecond.text = player.playerData.GetData(PlayerData.PP.NAME);
            gameManager.RP_numberSecond.text = player.playerData.GetData(PlayerData.PP.NUMBER).ToString();

            //Diamond
            int counter = 0;
            foreach (KeyValuePair<string, float> stat in player.finalStats.d)
            {
                gameManager.RP_diamondSecond.VerticesDistances[counter] = Mathf.FloorToInt(stat.Value) / 100f;
                gameManager.RP_values[counter].text = Mathf.FloorToInt(stat.Value).ToString();

                float statValue = stat.Value;

                gameManager.RP_values[counter].color = Player.ColorPicker(statValue);
                gameManager.RP_values[counter].alpha = Player.AlphaPicker(statValue, 0.7f, new bool[] { true, false, false, false, false });
                
                counter++;
            }
            if (counter == 3)
            {
                gameManager.RP_diamondSecond.VerticesDistances[3] = 0;
                gameManager.RP_valueSeconds[3].text = "";
            }

            gameManager.RP_diamondSecond.DrawPolygon(gameManager.RP_diamondSecond.sides, gameManager.RP_diamondSecond.VerticesDistances, gameManager.RP_diamondSecond.rotation);
            gameManager.RP_diamondSecond.SetAllDirty();

            //Overall
            gameManager.RP_overallSecond.text = Mathf.FloorToInt(player.playerData.GetData(PlayerData.PP.OVERALL)).ToString();

            float value = Mathf.FloorToInt(player.GetOverall());

            gameManager.RP_overallPanelSecond.color = Player.ColorPicker(value);
            gameManager.RP_overallAccentPanelSecond.color = Player.ColorPicker(value);
            gameManager.RP_overallBGSecond.color = Player.ColorPicker(value);
            gameManager.RP_overallSecond.alpha = Player.AlphaPicker(value, 0.7f, new bool[] { true, false, false, false, false });
            gameManager.RP_overallSecond.alpha = Player.AlphaPicker(value, 1f, new bool[] { false, true, true, true, true });

            //Stats
            for (int i = 0; i < gameManager.RP_content.transform.childCount; ++i)
            {
                Transform statTranform = gameManager.RP_content.transform.GetChild(i);

                for(int j = 0; j < PlayerData.PPString.Count; ++j)
                {
                    if(statTranform.GetChild(3).GetComponent<TextMeshProUGUI>().text == PlayerData.PPString[j])
                    {
                        int statValue;
                        if(statTranform.GetComponent<StatComponent>().isSmall)
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
                        
                        if(statValue < int.Parse(statTranform.GetChild(5).GetComponent<TextMeshProUGUI>().text))
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
        
    }
}