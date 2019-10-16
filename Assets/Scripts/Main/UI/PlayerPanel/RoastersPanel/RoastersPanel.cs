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
        void RefreshPlayerList(SortDropdown.SortMode mode, PlayerList.PlayerView view)
        {
            if (RoasterPitchersPanelButton.focusedObject.GetComponent<RoasterPitchersPanelButton>().buttonName == "Pitcher")
            {
                if (ChangeModeButton.changeMode == ChangeModeButton.ChangeMode.PLAYERS)
                {
                    gameManager.RefreshPlayerList(Filter.Mode.PITCHERS, mode, gameManager.RP_pitchersPanelContent, GameManager.StartingMemberFilter.MEMBER_ONLY, view);
                }
                else if (ChangeModeButton.changeMode == ChangeModeButton.ChangeMode.ORDERS)
                {
                    gameManager.RefreshPlayerList(Player.Position.STARTER_PITCHER, mode, gameManager.RP_pitchersPanelContent, GameManager.StartingMemberFilter.MEMBER_ONLY, view);
                }
            }
            else if (RoasterPitchersPanelButton.focusedObject.GetComponent<RoasterPitchersPanelButton>().buttonName == "Substitutes")
            {
                gameManager.RefreshPlayerList(Filter.Mode.ALL, mode, gameManager.RP_pitchersPanelContent, GameManager.StartingMemberFilter.SUB_ONLY, view);
            }
            else if (RoasterPitchersPanelButton.focusedObject.GetComponent<RoasterPitchersPanelButton>().buttonName == "BattingOrder")
            {
                gameManager.RefreshPlayerList(Filter.Mode.BATTERS, mode, gameManager.RP_pitchersPanelContent, GameManager.StartingMemberFilter.MEMBER_ONLY, view);
            }
        }

        if (ChangeModeButton.changeMode == ChangeModeButton.ChangeMode.PLAYERS)
        {
            RefreshPlayerList(SortDropdown.SortMode.POSITION, PlayerList.PlayerView.ROASTER);
        }
        else if (ChangeModeButton.changeMode == ChangeModeButton.ChangeMode.ORDERS)
        {
            RefreshPlayerList(SortDropdown.SortMode.ORDER, PlayerList.PlayerView.ORDER);
        }
    }
}
