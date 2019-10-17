using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToFieldViewButton : MonoBehaviour
{
    public RoastersPanel RoastersPanel;
    public void OnClick()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        gameManager.RP_statsPanel.SetActive(false);
        gameManager.RP_fieldViewPanel.SetActive(true);
        gameManager.RP_middlePanel.SetActive(false);
        gameManager.RP_pitchersPanel.SetActive(true);

        RoastersPanel.Refresh();
    }
}
