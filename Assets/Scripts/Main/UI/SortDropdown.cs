using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;

public class SortDropdown : CustomDropdown
{
    public static bool isAscendingOrder = true;

    public SortDataToDropdown sortDataToDropdownItem;
    public GameManager gameManager;
    public Sprite sprite;

    public enum SortMode
    {
        OVERALL, POSITION, NAME, NUMBER
    }

    public static string[] SortModeString =
    {
        "Overall", "Position", "Name", "Number"
    };

    void Start()
    {
        Refresh();
        gameManager.recentClick = itemParent.transform.GetChild(0).gameObject;
    }

    public override void Refresh()
    {
        //clears item list.
        sortDataToDropdownItem.ClearItem();

        //loads items to data list.
        for (int i = 0; i < Enum.GetNames(typeof(SortMode)).Length; ++i)
        {
            sortDataToDropdownItem.DataToItem(SortModeString[i], sprite);
        }

        //and refreshes.
        base.Refresh();

        //adding onclick listener.
        for(int i = 0; i < itemParent.transform.childCount; ++i)
        {
            GameObject itemObject = itemParent.transform.GetChild(i).gameObject;
            dropdownItems[i].OnItemSelection.AddListener(delegate ()
            {
                itemObject.GetComponent<SortDropdownItem>().OnClick();
            });
        }
    }
}
