using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPanelLayout : MonoBehaviour
{
    InGameObjects inGameObjects;

    public void UpdateLayout()
    {
        inGameObjects = GameObject.Find("InGameManager").GetComponent<InGameObjects>();

        for(int i = 0; i < InGameManager.strikeCount; ++i)
        {
            inGameObjects.strikes[i].color = Colors.yellowDark;
        }
        for(int i = 0; i < InGameManager.ballCount; ++i)
        {
            inGameObjects.balls[i].color = Colors.greenDark;
        }
        for (int i = 0; i < InGameManager.outCount; ++i)
        {
            inGameObjects.outs[i].color = Colors.redDark;
        }
    }

    public void ClearLayout()
    {
        for(int i = 0; i < 2; ++i)
        {
            inGameObjects.strikes[i].color = Color.white;
            inGameObjects.outs[i].color = Color.white;
        }
        for (int i = 0; i < 3; ++i)
        {
            inGameObjects.balls[i].color = Color.white;
        }
    }
}
