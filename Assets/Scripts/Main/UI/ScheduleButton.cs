using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class ScheduleButton : MonoBehaviour
{
    public int index;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI categoryText;
    public Image accentpanel;
    public VerticalLayoutGroup contentLayout;
    public GameObject dropdown;
    public Button confirm;

    public GameObject self;

    private void OnEnable()
    {
        StartCoroutine("ScheduleCheck");
    }

    private void OnDisable()
    {
        StopCoroutine("ScheduleCheck");
    }

    IEnumerator ScheduleCheck()
    {
        yield return new WaitForFixedUpdate();
        if(GameManager.ClickedObject == null)
        {
            GameManager.ClickedObject = gameObject;
            OnClick();
        }
    }

    public void OnClick()
    {
        Schedule schedule = Values.schedules[index];

        //Applies schedule data to main right panel.
        titleText.text = Values.schedules[index].GetTitle();
        categoryText.text = Schedule.categoryStrings[(int)schedule.GetCategories()];
        accentpanel.color = Schedule.categoryColors[(int)schedule.GetCategories()];
        
        Schedules.ApplyScheduleComponent(schedule);

        DataToDropdownItem dataToDropdownItem = dropdown.GetComponent<DataToDropdownItem>();
        dataToDropdownItem.ClearItem();
        for(int i = 0; i < schedule.GetItems().Length; ++i)
        {
            dataToDropdownItem.DataToItem(schedule.GetItems()[i], null);
        }
        if (schedule.selectedItem != -1) dropdown.GetComponent<CustomDropdown>().selectedItemIndex = schedule.selectedItem;
        else dropdown.GetComponent<CustomDropdown>().selectedItemIndex = 0;

        //Toggles.
        if (GameManager.ClickedObject != null) GameManager.ClickedObject.GetComponent<Button>().interactable = true;
        GetComponent<Button>().interactable = false;
        GameManager.ClickedObject = self;

        if(schedule.isSelectable)
        {
            dropdown.gameObject.SetActive(true);
            confirm.gameObject.SetActive(true);

            confirm.GetComponent<ConfirmButton>().index = index;
            if (!schedule.isConfirmed)
            {
                dropdown.GetComponent<Button>().interactable = true;
                confirm.interactable = true;
                confirm.GetComponentInChildren<TextMeshProUGUI>().text = "confirm";
            }
            else
            {
                dropdown.GetComponent<Button>().interactable = false;
                confirm.interactable = false;
                confirm.GetComponentInChildren<TextMeshProUGUI>().text = "confirmed";
            }
        }
        else
        {
            dropdown.gameObject.SetActive(false);
            confirm.gameObject.SetActive(false);
        }

        

        dropdown.GetComponent<CustomDropdown>().Refresh();
    }
}
