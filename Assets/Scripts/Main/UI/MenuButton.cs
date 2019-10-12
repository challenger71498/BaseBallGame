using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    //data members
    public static GameObject focusedObject;

    public GameObject panel;
    public Colors.ColorList buttonColor = Colors.ColorList.BLUE;

    bool isFocused = false;

    public void OnClick()
    {
        //Toggle.
        if(isFocused)
        {
            focusedObject = null;
            isFocused = false;
            panel.SetActive(false);
            GetComponent<Image>().color = Colors.primaryDark;
        }
        else
        {
            if (focusedObject)
            {
                MenuButton menuButton = focusedObject.GetComponent<MenuButton>();
                menuButton.isFocused = false;
                if (menuButton.panel) menuButton.panel.SetActive(false);

                focusedObject.GetComponent<Image>().color = Colors.primaryDark;
            }
            focusedObject = gameObject;
            isFocused = true;
            panel.SetActive(true);
            GetComponent<Image>().color = Colors.colors[(int)buttonColor];
        }
    }
}
