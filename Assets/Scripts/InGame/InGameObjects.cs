using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameObjects : MonoBehaviour
{
    [Header("TotalMovement")]
    public TotalMovement TotalMovement;
    public PlayerUIApply PlayerUIApply;

    [Header("Prefabs")]
    public GameObject titlePrefab;
    public GameObject scorePrefab;
    public GameObject resultPlayerPrefab;

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

    [Header("PauseButton")]
    public GameObject pauseButton;
    public GameObject pausePanel;
    public GameObject optionPanel;

    [Header("SpeedButton")]
    public GameObject speedButton;

    [Header("ResultPanel")]
    public GameObject resultPanel;
    public TextMeshProUGUI homeTeamNameText_RP;
    public TextMeshProUGUI awayTeamNameText_RP;
    public TextMeshProUGUI homeWinLossText_RP;
    public TextMeshProUGUI awayWinLossText_RP;
    public GameObject homeWon_RP;
    public GameObject awayWon_RP;
    public TextMeshProUGUI homeScoreText_RP;
    public TextMeshProUGUI awayScoreText_RP;
    public TextMeshProUGUI stadiumNameText_RP;
    public TextMeshProUGUI weatherText_RP;
    public GameObject homePlayers_RP;
    public GameObject homeBatters_RP;
    public GameObject homePitchers_RP;
    public GameObject awayPlayers_RP;
    public GameObject awayBatters_RP;
    public GameObject awayPitchers_RP;
    public GameObject proceedButton_RP;

    [Header("FieldPanel")]
    public GameObject fieldPanel;
}
