using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class SortDropdownItem : MonoBehaviour
{
    public SortDropdown sortDropdown;

    public void OnClick()
    {
        //changes sort mode.
        sortDropdown = GameObject.Find("Sort").GetComponent<SortDropdown>();
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.sortMode = (SortDropdown.SortMode)sortDropdown.selectedItemIndex;
        
        //changes order.
        if (gameManager.recentClick == null || gameObject != gameManager.recentClick)
        {
            gameManager.recentClick = gameObject;
        }
        else
        {
            if (SortDropdown.isAscendingOrder) SortDropdown.isAscendingOrder = false;
            else SortDropdown.isAscendingOrder = true;

            sortDropdown.selectedImage.rectTransform.Rotate(new Vector3(0, 0, 180));
        }

        //lastly refreshes.
        if(GameManager.isModeNow)
        {
            gameManager.RefreshPlayerList(gameManager.mode, gameManager.sortMode, GameManager.currentStartingMemberFilter);
        }
        else
        {
            gameManager.RefreshPlayerList(GameManager.currentMetaPosition, gameManager.sortMode, GameManager.currentStartingMemberFilter);
        }
    }
}
