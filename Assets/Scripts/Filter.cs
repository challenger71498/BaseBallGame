using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Filter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameManager gameManager;

    public enum Mode
    {
        ALL, BATTERS, PITCHERS
    }

    public string[] filterString =
    {
        "all", "batters", "pitchers"
    };

    public void OnClick()
    {
        if (mode == Mode.PITCHERS)
        {
            mode = Mode.ALL;
        }
        else
        {
            mode = (Mode)((int)mode + 1);
        }
        gameManager.mode = mode;

        gameManager.RefreshPlayerList(mode, gameManager.sortMode);

        text.text = filterString[(int)mode];
    }

    public Mode mode = Mode.ALL;
}
