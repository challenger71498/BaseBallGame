using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainPanel : MonoBehaviour
{
    public static GameObject focusedObject = null;
    public static GameObject markedObject = null;

    public Training.Train train;
    public Player player;

    public void OnClick()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(focusedObject != null)
        {
            focusedObject.GetComponent<Button>().interactable = true;
        }
        focusedObject = gameObject;
        gameObject.GetComponent<Button>().interactable = false;

        gameManager.TP_title.text = Training.trainString[(int)train];
        gameManager.TP_description.text = Training.trainDescription[(int)train];

        for(int i = 0; i < gameManager.TP_effectContent.transform.childCount; ++i)
        {
            Destroy(gameManager.TP_effectContent.transform.GetChild(i).gameObject);
        }

        foreach(KeyValuePair<PlayerData.PP, float> pair in player.GetTraining(train).modifier)
        {
            int days = (int)(1 / pair.Value);
            gameManager.EffectInstantiate(pair.Key, days);
        }

        TrainingApply.player = player;
        TrainingApply.train = train;

        if (player.train != train)
        {
            gameManager.TP_apply.interactable = true;
            gameManager.TP_applyText.text = "Apply";
        }
        else
        {
            gameManager.TP_apply.interactable = false;
            gameManager.TP_applyText.text = "Applied";
        }
    }
}
