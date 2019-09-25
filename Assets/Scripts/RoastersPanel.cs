using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoastersPanel : MonoBehaviour
{
    public GameObject filter;
    public GameObject filterText;

    private void OnEnable()
    {
        filter.SetActive(false);
        filterText.SetActive(false);

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.RP_statsPanel.SetActive(false);
        gameManager.RP_fieldViewPanel.SetActive(true);
        gameManager.RP_middlePanel.SetActive(false);
        gameManager.RP_pitchersPanel.SetActive(true);
        
        Refresh(gameManager);
    }

    private void OnDisable()
    {
        filter.SetActive(true);
        filterText.SetActive(true);

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        gameManager.RefreshPlayerList(gameManager.mode, gameManager.sortMode);
    }
    
    public static void Refresh(GameManager gameManager)
    {
        if (RoasterPitchersPanelButton.focusedObject.GetComponent<RoasterPitchersPanelButton>().buttonName == "Pitcher")
        {
            gameManager.RefreshPlayerList(Filter.Mode.PITCHERS, SortDropdown.SortMode.POSITION, gameManager.RP_pitchersPanelContent, GameManager.StartingMemberFilter.MEMBER_ONLY, PlayerList.PlayerView.ROASTER);
        }
        else if (RoasterPitchersPanelButton.focusedObject.GetComponent<RoasterPitchersPanelButton>().buttonName == "Substitutes")
        {
            gameManager.RefreshPlayerList(Filter.Mode.ALL, SortDropdown.SortMode.POSITION, gameManager.RP_pitchersPanelContent, GameManager.StartingMemberFilter.SUB_ONLY, PlayerList.PlayerView.ROASTER);
        }
    }
}
