using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Filter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameManager gameManager;
    public PlayerListObject listObject;

    public enum Mode
    {
        ALL, BATTERS, PITCHERS
    }

    public string[] filterString =
    {
        "all", "batters", "pitchers"
    };

    public enum StartingMemberFilter
    {
        ALL, MEMBER_ONLY, MEMBER_EXCLUDED, SUB_ONLY, SUB_EXCLUDED
    }

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

        listObject.RefreshPlayerList(mode, gameManager.sortMode);

        text.text = filterString[(int)mode];
    }

    public Mode mode = Mode.ALL;
}
