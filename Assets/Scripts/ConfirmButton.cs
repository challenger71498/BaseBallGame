using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class ConfirmButton : MonoBehaviour
{
    public Button button;
    public Button dropdown;
    public TextMeshProUGUI text;
    public Image image;

    public int index;
    

    public void onClick()
    {
        Values.schedules[index].isConfirmed = true;
        dropdown.interactable = false;
        button.interactable = false;
        text.text = "CONFIRMED";

        Values.schedules[index].selectedItem = dropdown.GetComponent<CustomDropdown>().selectedItemIndex;
        Debug.Log(Values.schedules[index].selectedItem);
    }
}
