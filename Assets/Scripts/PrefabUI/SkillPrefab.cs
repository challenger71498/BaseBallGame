using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPrefab : MonoBehaviour
{
    [Header("Prefabs")]
    public static Prefabs Prefabs;

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
        Prefabs = GameObject.Find("Prefabs").GetComponent<Prefabs>();
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
        int value1 = Mathf.FloorToInt((float)player.playerData.GetData(pref));
        value.text = value1.ToString();
        if(playerCompare != default)
        {
            int value2 = Mathf.FloorToInt((float)playerCompare.playerData.GetData(pref));
            valueSecond.text = value2.ToString();
            if (value1 < value2)
            {
                value.color = Colors.red;
                valueSecond.color = Color.white;
            }
            else
            {
                value.color = Color.white;
                valueSecond.color = Colors.red;
            }
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
            arrow.SetActive(true);
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
            skillPrefab = Instantiate(Prefabs.skillSmall, parentTransform).GetComponent<SkillPrefab>();
        }
        else
        {
            skillPrefab = Instantiate(Prefabs.skill, parentTransform).GetComponent<SkillPrefab>();
        }
        
        skillPrefab.SetByPref(player, pref, playerCompare);
    }
}
