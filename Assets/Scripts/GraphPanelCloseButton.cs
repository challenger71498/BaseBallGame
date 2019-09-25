using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphPanelCloseButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StatisticPanel.focusedObject.GetComponent<Button>().interactable = true;
        StatisticPanel.focusedObject = null;

        gameManager.SP_graphPanel.SetActive(false);
    }
}
