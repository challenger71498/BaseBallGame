using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InningPanel : MonoBehaviour
{
    public InGameObjects inGameObjects;

    public void UpdateLayout()
    {
        inGameObjects.inningText.text = InGameManager.currentInning.ToString();
        if (!InGameManager.isBottom) //Top
        {
            inGameObjects.inningImage.rectTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            inGameObjects.inningImage.rectTransform.rotation = Quaternion.Euler(0, 0, 180);
        }
        
    }

    public void OnClick()
    {
        inGameObjects.boardPanel.gameObject.SetActive(true);
    }
}
