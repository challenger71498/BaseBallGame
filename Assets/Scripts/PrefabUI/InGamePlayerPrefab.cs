using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGamePlayerPrefab
{
    public InGamePlayerPrefab(GameObject obj)
    {
        Transform transform = obj.transform;
        haveBall = transform.GetChild(0).GetComponent<Image>();
        ToggleHaveBall(false);
        condition = transform.GetChild(1).GetComponent<Image>();
        playerColor = transform.GetChild(2).GetComponent<Image>();
        //positionText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void ToggleHaveBall(bool isActive)
    {
        haveBall.gameObject.SetActive(isActive);
    }

    public void SetByPlayer(Player player, Team team)
    {
        condition.fillAmount = player.playerData.GetData(PlayerData.PP.CONDITION) / 100f;
        playerColor.color = team.teamData.GetData(TeamData.TP.COLOR);
        //positionText.text = Player.positionStringShort[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        nameText.text = ((string)player.playerData.GetData(PlayerData.PP.NAME)).Split(' ')[1];
    }

    public void SetByPlayerConditionOnly(Player player)
    {
        condition.fillAmount = player.playerData.GetData(PlayerData.PP.CONDITION) / 100f;
    }

    public Image haveBall;
    public Image condition;
    public Image playerColor;
    //public TextMeshProUGUI positionText;
    public TextMeshProUGUI nameText;
}
