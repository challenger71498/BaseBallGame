using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerObject
{
    public PlayerObject(GameObject obj)
    {
        Transform target = obj.transform;
        condition = target.GetChild(0).GetComponent<Image>();
        listAccentPanel = target.GetChild(1).GetComponent<Image>();
        listAccentPanel2 = target.GetChild(2).GetComponent<Image>();
        positionText = target.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = target.GetChild(3).GetComponent<TextMeshProUGUI>();
        numberText = target.GetChild(4).GetComponent<TextMeshProUGUI>();
        statLayout = target.GetChild(5).gameObject;
        overallText = target.GetChild(6).GetComponent<TextMeshProUGUI>();
        isStartingMember = target.GetChild(7).GetComponent<Image>();
    }

    public void SetByPlayer(Player player)
    {
        SetConditionAmount(player.playerData.GetData(PlayerData.PP.CONDITION));
        SetPosition(player.playerData.GetData(PlayerData.PP.POSITION));
        SetName(player.playerData.GetData(PlayerData.PP.NAME));
        SetNumber(player.playerData.GetData(PlayerData.PP.NUMBER));
        SetOverall(player.playerData.GetData(PlayerData.PP.OVERALL));
        SetStartMemberImage(player.isStartingMember);
    }

    public void SetConditionAmount(float amount)
    {
        condition.fillAmount = (100 - amount) / 100f;
    }

    public void SetPosition(Player.Position position)
    {
        listAccentPanel.color = Player.positionColor[(int)position];        //ListAccentPanel
        listAccentPanel2.color = Player.positionColor[(int)position];       //ListAccentPanel2
        positionText.text = Player.positionStringShort[(int)position];      //Position
    }

    public void SetName(string name)
    {
        nameText.text = name;   //Name
    }

    public void SetNumber(int number)
    {
        numberText.text = number.ToString(); //Number
    }

    public void SetOverall(float overall)
    {
        overallText.text = overall.ToString();   //Overall
        overallText.color = Player.ColorPicker(overall, new bool[] { false, true, true, true, true });
    }

    public void SetStartMemberImage(bool isStarting)
    {
        if(isStarting)
        {
            isStartingMember.color = Color.white;
        }
        else
        {
            isStartingMember.color = Color.clear;
        }
    }

    public Image condition;
    public Image listAccentPanel;
    public Image listAccentPanel2;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI numberText;
    public GameObject statLayout;
    public TextMeshProUGUI overallText;
    public Image isStartingMember;
}
