using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldButton : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject statComponent;
    public GameObject statSmallComponent;

    [Header("Game Management")]
    public GameManager GameManager;

    [Header("GameObjects")]
    public GameObject statsPanel;
    public GameObject fieldViewPanel;
    public GameObject middlePanel;
    public GameObject pitchersPanel;
    public Player.Position position;

    Player player;

    Color[] conditionColor =
    {
        Colors.red, Colors.yellow, Colors.green
    };

    int[] conditionRange =
    {
        33, 66, 100
    };

    public virtual void OnEnable()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.RP_statsPanel.SetActive(false);
        gameManager.RP_fieldViewPanel.SetActive(true);
        gameManager.RP_middlePanel.SetActive(false);
        gameManager.RP_pitchersPanel.SetActive(true);

        TextMeshProUGUI positionName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        positionName.text = Player.positionStringShort[(int)position];
        
        if(position == Player.Position.STARTER_PITCHER)
        {
            Game game = GameManager.game;
            if(game != null)
            {
                player = game.GetStarterPitcher(Values.myTeam);
            }
            else
            {
                gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            foreach (KeyValuePair<Player.Position, Player> playerPair in Values.myTeam.startingMembers.d)
            {
                if (playerPair.Key == position && !playerPair.Value.isSubstitute)
                {
                    player = playerPair.Value;
                    break;
                }
            }
        }

        TextMeshProUGUI name = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        string[] nameText = player.playerData.GetData(PlayerData.PP.NAME).Split(' ');
        string firstName = nameText[0][0].ToString();
        name.text = firstName + ". " + nameText[1];

        TextMeshProUGUI overall = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        int over = Mathf.FloorToInt((float)player.playerData.GetData(PlayerData.PP.OVERALL));

        for (int i = 0; i < Player.statRange.Count; ++i)
        {
            if (over < Player.statRange[i])
            {
                overall.color = Player.statColor[i];
                break;
            }
        }

        overall.text = over.ToString();

        TextMeshProUGUI condition = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        float cond = player.playerData.GetData(PlayerData.PP.CONDITION);

        for (int i = 0; i < conditionColor.Length; ++i)
        {
            if (Mathf.FloorToInt(cond) < conditionRange[i])
            {
                condition.color = conditionColor[i];
                break;
            }
        }

        string conditionTemp = "";
        for(int i = 0; i < Mathf.FloorToInt(cond / 10); ++i)
        {
            conditionTemp += "l";
        }

        condition.text = conditionTemp;
    }

    public void OnClick()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        PlayerList playerList = gameObject.AddComponent<PlayerList>();
        playerList.player = player;
        playerList.statComponent = statComponent;
        playerList.statSmallComponent = statSmallComponent;
        
        playerList.OnClick(gameManager, PlayerList.PlayerView.ROASTER, true);
    }
}
