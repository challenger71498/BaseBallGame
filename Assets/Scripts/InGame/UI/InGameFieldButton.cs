using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using TMPro;

public class InGameFieldButton : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject statComponent;
    public GameObject statSmallComponent;

    [Header("GameObjects")]
    public Player.Position position;

    Player player;

    public virtual void OnEnable()
    {
        InGameObjects InGameObjects = GameObject.Find("InGameManager").GetComponent<InGameObjects>();

        InGameObjects.statsPanel.SetActive(false);
        InGameObjects.fieldViewPanel.SetActive(true);
        InGameObjects.middlePanel.SetActive(false);

        TextMeshProUGUI positionName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        positionName.text = Player.positionStringShort[(int)position];

        if (position == Player.Position.STARTER_PITCHER)
        {
            if(InGameManager.currentDefend == Values.myTeam)
            {
                player = InGameManager.currentPitcher;
            }
            else
            {
                player = InGameManager.otherPitcher;
            }
        }
        else
        {
            if (InGameManager.currentAttack == Values.myTeam)
            {
                foreach (Batter batter in InGameManager.homeBattingOrder)
                {
                    if (batter.playerData.GetData(PlayerData.PP.POSITION) == position && !batter.isSubstitute)
                    {
                        player = batter;
                        break;
                    }
                }
            }
            else
            {
                foreach (Batter batter in InGameManager.awayBattingOrder)
                {
                    if (batter.playerData.GetData(PlayerData.PP.POSITION) == position && !batter.isSubstitute)
                    {
                        player = batter;
                        break;
                    }
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

        for (int i = 0; i < FieldButton.conditionColor.Length; ++i)
        {
            if (Mathf.FloorToInt(cond) < FieldButton.conditionRange[i])
            {
                condition.color = FieldButton.conditionColor[i];
                break;
            }
        }

        string conditionTemp = "";
        for (int i = 0; i < Mathf.FloorToInt(cond / 10); ++i)
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
