using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePlayerList : MonoBehaviour
{
    public InGameObjects InGameObjects;

    public void OnClick()
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
            gameManager.RP_valueSeconds[counter].alpha = Player.AlphaPicker(statValue, 0.7f, new bool[] { true, false, false, false, false });

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
}
