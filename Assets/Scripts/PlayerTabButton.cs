using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTabButton : MonoBehaviour
{
    //data members
    public static GameObject focusedObject;

    public GameObject panel;
    public Colors.ColorList buttonColor = Colors.ColorList.DARK_BLUE;

    public void OnClick()
    {
        //Enables last focused object.
        if(focusedObject != null)
        {
            focusedObject.GetComponent<PlayerTabButton>().panel.SetActive(false);
            focusedObject.GetComponent<Button>().interactable = true;
            focusedObject.GetComponent<Image>().color = Colors.primaryDark;
        }

        //Toggles this object.
        panel.SetActive(true);
        gameObject.GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = Colors.colors[(int)buttonColor];

        //Change focused object to this.
        focusedObject = gameObject;
    }
}
