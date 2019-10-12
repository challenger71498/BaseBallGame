using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanel : MonoBehaviour
{
    public GameManager gameManager;

    private void OnEnable()
    {
        gameManager.RefreshPlayerList(gameManager.mode, gameManager.sortMode);
    }
}
