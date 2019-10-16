using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeModeButton : MonoBehaviour
{
    public GameManager GameManager;
    public TextMeshProUGUI changeText;

    public enum ChangeMode
    {
        UNDEFINED, PLAYERS, ORDERS
    }

    public static List<string> changeModeString = new List<string>()
    {
        "Undefined", "Players", "Orders"
    };

    public static ChangeMode changeMode = ChangeMode.PLAYERS;

    public void OnClick()
    {
        if(changeMode == ChangeMode.PLAYERS)
        {
            changeMode = ChangeMode.ORDERS;
            RoastersPanel.Refresh(GameManager);
        }
        else if (changeMode == ChangeMode.ORDERS)
        {
            if(PlayerList.focusedPrevObject != null)
            {
                PlayerList.focusedPrevObject.GetComponent<UnityEngine.UI.Image>().color = Colors.primarySemiDark;
                PlayerList.focusedPrevObject = null;
            }
            changeMode = ChangeMode.PLAYERS;
            RoastersPanel.Refresh(GameManager);
        }
        changeText.text = changeModeString[(int)changeMode];
    }
}
