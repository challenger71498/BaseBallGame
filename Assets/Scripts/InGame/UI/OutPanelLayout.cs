using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPanelLayout : MonoBehaviour
{
    public InGameObjects inGameObjects;

    public void UpdateLayout()
    {
        for(int i = 0; i < InGameManager.strikeCount; ++i)
        {
            inGameObjects.strikes[i].color = Colors.yellow;
        }
        for(int i = 0; i < InGameManager.ballCount; ++i)
        {
            inGameObjects.balls[i].color = Colors.green;
        }
        for (int i = 0; i < InGameManager.outCount; ++i)
        {
            inGameObjects.outs[i].color = Colors.red;
        }
    }

    public void ClearLayout()
    {
        for (int i = 0; i < 2; ++i)
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
