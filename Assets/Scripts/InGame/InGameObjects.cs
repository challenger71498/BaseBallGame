using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameObjects : MonoBehaviour
{
    [Header("OutPanel")]
    public OutPanelLayout outPanelLayout;
    public Image[] strikes;
    public Image[] balls;
    public Image[] outs;

    [Header("InningPanel")]
    public InningPanel inningPanel;
    public TextMeshProUGUI inningText;
    public Image inningImage;

    [Header("BasePanel")]
    public BasePanel basePanel;
    public Image[] bases;

    [Header("ScorePanel")]
    public ScorePanel scorePanel;
    public TextMeshProUGUI leftScore;
    public TextMeshProUGUI rightScore;

    [Header("BoardPanel")]
    public BoardPanel boardPanel;
}
