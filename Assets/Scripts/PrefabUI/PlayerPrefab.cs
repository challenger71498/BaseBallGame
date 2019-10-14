using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPrefab : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject playerPanelPrefab;
    public GameObject statPrefab;
    public GameObject pitchesPrefab;

    public static GameObject playerPanel;
    public static GameObject stat;
    public static GameObject pitches;

    [Header("GameObjects")]
    public Image condition;
    public Image listAccentPanel;
    public Image listAccentPanel2;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI numberText;
    public GameObject statLayout;
    public TextMeshProUGUI overallText;
    public Image isStartingMember;
    public TextMeshProUGUI orderText;

    public void Start()
    {
        playerPanel = playerPanelPrefab;
        stat = statPrefab;
        pitches = pitchesPrefab;
    }

    /// <summary>
    /// Sets prefab by a player instance.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="isPrefAvailable"></param>
    public void SetByPlayer(Player player, bool isPrefAvailable)
    {
        SetConditionAmount(player.playerData.GetData(PlayerData.PP.CONDITION));
        SetPosition(player.playerData.GetData(PlayerData.PP.POSITION));
        SetName(player.playerData.GetData(PlayerData.PP.NAME));
        SetNumber(player.playerData.GetData(PlayerData.PP.NUMBER));
        SetOverall(player.playerData.GetData(PlayerData.PP.OVERALL));
        SetStartMemberImage(player.isStartingMember);
        SetOrder(player.order);

        if(isPrefAvailable)
        {
            SetPref(player);
        }
    }

    /// <summary>
    /// Sets condition text.
    /// </summary>
    /// <param name="amount"></param>
    public void SetConditionAmount(float amount)
    {
        condition.fillAmount = (100 - amount) / 100f;
    }

    /// <summary>
    /// Sets position text.
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Player.Position position)
    {
        listAccentPanel.color = Player.positionColor[(int)position];        //ListAccentPanel
        listAccentPanel2.color = Player.positionColor[(int)position];       //ListAccentPanel2
        positionText.text = Player.positionStringShort[(int)position];      //Position
    }

    /// <summary>
    /// Sets name text.
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
        nameText.text = name;   //Name
    }

    /// <summary>
    /// Sets number text.
    /// </summary>
    /// <param name="number"></param>
    public void SetNumber(int number)
    {
        numberText.text = number.ToString(); //Number
    }

    /// <summary>
    /// Sets overall text.
    /// </summary>
    /// <param name="overall"></param>
    public void SetOverall(float overall)
    {
        overallText.text = overall.ToString();   //Overall
        overallText.color = Player.ColorPicker(overall, new bool[] { false, true, true, true, true });
    }

    /// <summary>
    /// Sets start member image.
    /// </summary>
    /// <param name="isStarting"></param>
    public void SetStartMemberImage(bool isStarting)
    {
        if(isStarting)
        {
            isStartingMember.color = Color.white;
        }
        else
        {
            isStartingMember.color = Color.clear;
        }
    }

    /// <summary>
    /// Sets order text.
    /// </summary>
    /// <param name="order"></param>
    public void SetOrder(int order)
    {
        if(order == -1)
        {
            orderText.text = "";
        }
        else
        {
            orderText.text = OrdinalNumbers.AddOrdinal(order);
        }
    }

    /// <summary>
    /// Sets prefs.
    /// </summary>
    /// <param name="player"></param>
    public void SetPref(Player player)
    {
        foreach (KeyValuePair<string, float> stat in player.finalStats.d)
        {
            StatInstantiate(stat.Key, stat.Value);
        }
        if (player.GetType() == typeof(Pitcher))
        {
            PitchesInstantiate(((Pitcher)player).pitches.d);
        }
    }
    
    /// <summary>
    /// Instantiates stat prefab.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="value"></param>
    void StatInstantiate(string title, float value)
    {
        GameObject statObject = Instantiate(stat, statLayout.transform);
        TextMeshProUGUI titleText = statObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        titleText.text = title;    //Stat.Title
        TextMeshProUGUI valueText = statObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        valueText.text = Mathf.FloorToInt(value).ToString();    //Stat.Value
        for (int i = 0; i < Player.statRange.Count; ++i)
        {
            if (Mathf.FloorToInt(value) < Player.statRange[i])
            {
                if (i == 4)
                {
                    titleText.color = Player.statColor[i];
                }
                valueText.color = Player.statColor[i];
                valueText.alpha = Player.statAlpha[i];
                break;
            }
        }
    }

    /// <summary>
    /// Instantiates pitches prefab.
    /// </summary>
    /// <param name="pitchesDictionary"></param>
    void PitchesInstantiate(Dictionary<Pitcher.Pitch, float> pitchesDictionary)
    {
        GameObject pitchesObject = Instantiate(pitches, statLayout.transform);
        string text = "";
        foreach (Pitcher.Pitch pitch in pitchesDictionary.Keys)
        {
            text += Pitcher.PitchStringShort[(int)pitch] + " ";
        }
        text.Remove(text.Length - 1);
        pitchesObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }

    /// <summary>
    /// Instantiates playerPanel prefab.
    /// </summary>
    /// <returns></returns>
    public static GameObject PlayerInstantiate(Player player, Transform parentTransform, PlayerList.PlayerView playerView = PlayerList.PlayerView.UNDEFINED, bool isPrefShown = true)
    {
        GameManager GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject playerObject = Instantiate(playerPanel, parentTransform);
        PlayerPrefab playerPrefab = playerObject.GetComponent<PlayerPrefab>();
        playerPrefab.SetByPlayer(player, isPrefShown);

        //For button script
        if (playerView != PlayerList.PlayerView.UNDEFINED)
        {
            PlayerList playerList = playerObject.GetComponent<PlayerList>();
            playerList.player = player;
            playerObject.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                playerList.OnClick(GameManager, playerView);
            });
        }

        return playerObject;
    }
}
