using UnityEngine;
using UnityEngine.UI;

public class RoasterPitchersPanelButton : MonoBehaviour
{
    //Gameobjects
    public GameManager gameManager;
    public RoastersPanel RoastersPanel;

    //data members
    public static GameObject focusedObject;
    public GameObject firstFocused;
    public string buttonName;
    
    public Colors.ColorList buttonColor = Colors.ColorList.DARK_BLUE;

    public void OnEnable()
    {
        if(focusedObject == null)
        {
            focusedObject = firstFocused;
            focusedObject.GetComponent<RoasterPitchersPanelButton>().OnClick();
        }
    }

    public void OnClick()
    {
        //Enables last focused object.
        if (focusedObject != null)
        {
            focusedObject.GetComponent<Button>().interactable = true;
            focusedObject.GetComponent<Image>().color = Colors.primaryDark;
        }

        //Toggles this object.
        gameObject.GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = Colors.colors[(int)buttonColor];

        //Change focused object to this.
        focusedObject = gameObject;

        RoastersPanel.Refresh();
    }
}
