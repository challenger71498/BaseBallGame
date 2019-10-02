using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameObjects : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject titlePrefab;
    public GameObject scorePrefab;

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
    public Image[] baseStealingAttepts;

    [Header("ScorePanel")]
    public ScorePanel scorePanel;
    public TextMeshProUGUI leftScore;
    public TextMeshProUGUI rightScore;

    [Header("BoardPanel")]
    public BoardPanel boardPanel;
    public TextMeshProUGUI homeTeamName;
    public TextMeshProUGUI awayTeamName;
    public GameObject titleLayout;
    public GameObject scoreLayout;
    public TextMeshProUGUI homeRunText;
    public TextMeshProUGUI awayRunText;
    public TextMeshProUGUI homeHitText;
    public TextMeshProUGUI awayHitText;
    public TextMeshProUGUI homeErrorText;
    public TextMeshProUGUI awayErrorText;
}
