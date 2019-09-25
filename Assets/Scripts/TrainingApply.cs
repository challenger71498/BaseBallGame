using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingApply : MonoBehaviour
{
    public static Player player;
    public static Training.Train train;

    public void OnClick()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player.train = train;

        TrainPanel.markedObject.transform.GetChild(2).GetComponent<Image>().color = Color.clear;
        TrainPanel.focusedObject.transform.GetChild(2).GetComponent<Image>().color = Color.white;
        TrainPanel.markedObject = TrainPanel.focusedObject;

        gameManager.TP_apply.interactable = false;
        gameManager.TP_applyText.text = "Applied";
    }
}
