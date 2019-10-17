using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class SortDropdownItem : MonoBehaviour
{
    public SortDropdown sortDropdown;
    public PlayerListObject listObject;

    public void OnClick()
    {
        //changes sort mode.
        sortDropdown = GameObject.Find("Sort").GetComponent<SortDropdown>();
        GameManager GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        listObject = GameManager.playerContent.GetComponent<PlayerListObject>();

        GameManager.sortMode = (SortDropdown.SortMode)sortDropdown.selectedItemIndex;
        
        //changes order.
        if (GameManager.recentClick == null || gameObject != GameManager.recentClick)
        {
            GameManager.recentClick = gameObject;
        }
        else
        {
            if (SortDropdown.isAscendingOrder) SortDropdown.isAscendingOrder = false;
            else SortDropdown.isAscendingOrder = true;

            sortDropdown.selectedImage.rectTransform.Rotate(new Vector3(0, 0, 180));
        }

        //lastly refreshes.
        if(PlayerListObject.isModeNow)
        {
            listObject.RefreshPlayerList(GameManager.mode, GameManager.sortMode, true, PlayerListObject.currentStartingMemberFilter);
        }
        else
        {
            listObject.RefreshPlayerList(PlayerListObject.currentMetaPosition, GameManager.sortMode, true, PlayerListObject.currentStartingMemberFilter);
        }
    }
}
