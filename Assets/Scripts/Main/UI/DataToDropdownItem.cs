using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;

public class DataToDropdownItem : MonoBehaviour
{
    public CustomDropdown customDropdown;
   
    //Clears dropdownItem list.
    public void ClearItem()
    {
        customDropdown.dropdownItems.Clear();
    }

    //Converts data to Item object, and adds it to dropdownItems List.
    public virtual void DataToItem(string name, Sprite sprite)
    {
        CustomDropdown.Item item = new CustomDropdown.Item
        {
            itemName = name,
            itemIcon = sprite,
            OnItemSelection = new UnityEngine.Events.UnityEvent()
        };

        customDropdown.dropdownItems.Add(item);

        item.OnItemSelection.AddListener(delegate ()
        {
            customDropdown.ChangeDropdownInfo(customDropdown.dropdownItems.IndexOf(item));
        });
    }
}
