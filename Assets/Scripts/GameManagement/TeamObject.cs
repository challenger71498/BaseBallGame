using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamObject
{
    public TeamObject(GameObject obj)
    {
        target = obj;
        homeAwayText = target.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        emblem = target.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        nameText = target.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        winLossText = target.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
        recentMatchesContent = target.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        keyPlayerObject = new PlayerObject(target.transform.GetChild(3).GetChild(0).GetChild(0).gameObject);
    }

    public TextMeshProUGUI homeAwayText;
    public Image emblem;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI winLossText;
    public GameObject recentMatchesContent;
    public PlayerObject keyPlayerObject;

    public GameObject target;
}
