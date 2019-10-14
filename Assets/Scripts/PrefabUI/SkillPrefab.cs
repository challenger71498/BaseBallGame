using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPrefab : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject skillPrefab;
    public GameObject skillSmallPrefab;

    public static GameObject skill;
    public static GameObject skillSmall;

    [Header("GameObjects")]
    public Image ListAccentPanel;
    public Image ListPercentPanel;
    public Image ListPercentPanelSecond;
    public TextMeshProUGUI title;
    public TextMeshProUGUI value;
    public TextMeshProUGUI valueSecond;
    public GameObject arrow;

    public void Start()
    {
        skill = skillPrefab;
        skillSmall = skillSmallPrefab;
    }

    public void SetByPref(Player player, PlayerData.PP pref, Player playerCompare = default)
    {
        SetTitle(pref);
        SetValue(player, pref, playerCompare);
        if(playerCompare != default)
        {
            SetImage(player.playerData.GetData(pref), playerCompare.playerData.GetData(pref));
        }
        else
        {
            SetImage(player.playerData.GetData(pref));
        }
    }

    public void SetTitle(PlayerData.PP pref) 
    {
        title.text = PlayerData.PPString[(int)pref];
    }

    public void SetValue(Player player, PlayerData.PP pref, Player playerCompare = default)
    {
        value.text = Mathf.FloorToInt((float)player.playerData.GetData(pref)).ToString();
        if(playerCompare != default)
        {
            valueSecond.text = Mathf.FloorToInt((float)playerCompare.playerData.GetData(pref)).ToString();
        }
        else
        {
            valueSecond.text = "";
        }
    }

    public void SetImage(float value, float valueSecond = -1)
    {
        ListAccentPanel.color = Player.ColorPicker(value);
        title.color = Player.ColorPicker(value);
        ListPercentPanel.fillAmount = Mathf.FloorToInt(value) / 100f;
        if(valueSecond != -1)
        {
            ListPercentPanelSecond.fillAmount = Mathf.FloorToInt(valueSecond) / 100f;
        }
        else
        {
            arrow.SetActive(false);
            ListPercentPanelSecond.fillAmount = 0;
        }
    }

    public static void SkillInstantiate(Player player, PlayerData.PP pref, Transform parentTransform, Player playerCompare = default, bool isSmallPrefab = false)
    {
        SkillPrefab skillPrefab;
        if (isSmallPrefab)
        {
            skillPrefab = Instantiate(skillSmall, parentTransform).GetComponent<SkillPrefab>();
        }
        else
        {
            skillPrefab = Instantiate(skill, parentTransform).GetComponent<SkillPrefab>();
        }
        
        skillPrefab.SetByPref(player, pref, playerCompare);
    }
}
