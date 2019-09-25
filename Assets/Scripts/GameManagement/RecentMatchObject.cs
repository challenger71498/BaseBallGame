using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecentMatchObject
{
    public RecentMatchObject(GameObject obj)
    {
        backGround = obj.GetComponent<Image>();
        Transform layout = obj.transform.GetChild(0);
        emblem = layout.GetChild(0).GetChild(0).GetComponent<Image>();
        teamAgainstText = layout.GetChild(1).GetComponent<TextMeshProUGUI>();
        ourScoreText = layout.GetChild(2).GetComponent<TextMeshProUGUI>();
        colonText = layout.GetChild(3).GetComponent<TextMeshProUGUI>();
        opponentScoreText = layout.GetChild(4).GetComponent<TextMeshProUGUI>();
        winLossText = layout.GetChild(5).GetComponent<TextMeshProUGUI>();
        dateText = layout.GetChild(6).GetComponent<TextMeshProUGUI>();
        noMatch = layout.GetChild(7).gameObject;
        noMatch.SetActive(false);
    }

    public void SetByGame(Game game, Team alies)
    {
        Team ours;
        Team opponent;

        if (alies == game.home)
        {
            ours = game.home;
            opponent = game.away;
        }
        else if(alies == game.away)
        {
            ours = game.away;
            opponent = game.home;
        }
        else
        {
            throw new System.NullReferenceException("This game does not have team name" + alies.teamData.GetData(TeamData.TP.NAME) + ".");
        }

        teamAgainstText.text = opponent.teamData.GetData(TeamData.TP.TEAM_NAME);
        if(opponent == Values.myTeam)
        {
            teamAgainstText.color = Colors.blue;
        }
        ourScoreText.text = game.GetScore(ours).ToString();
        opponentScoreText.text = game.GetScore(opponent).ToString();

        if (game.GetGameResult(ours) == Game.GameResult.WIN)
        {
            winLossText.text = "W";
            backGround.color = Colors.blueDark;
        }
        else if (game.GetGameResult(ours) == Game.GameResult.DRAW)
        {
            winLossText.text = "D";
        }
        else if (game.GetGameResult(ours) == Game.GameResult.LOSS)
        {
            winLossText.text = "L";
            backGround.color = Colors.redDark;
        }
        else
        {
            throw new System.Exception("Error caught while trying to allocate text to TextMeshProUGUI winLossText.");
        }

        dateText.text = game.date.date.Month.ToString() + "/" + game.date.date.Day.ToString();
    }

    public void SetNoGame()
    {
        emblem.transform.parent.gameObject.SetActive(false);
        teamAgainstText.text = "";
        ourScoreText.text = "";
        colonText.text = "";
        opponentScoreText.text = "";
        winLossText.text = "";
        dateText.text = "";

        noMatch.SetActive(true);
    }

    public Image backGround;
    public Image emblem;
    public TextMeshProUGUI teamAgainstText;
    public TextMeshProUGUI ourScoreText;
    public TextMeshProUGUI colonText;
    public TextMeshProUGUI opponentScoreText;
    public TextMeshProUGUI winLossText;
    public TextMeshProUGUI dateText;
    public GameObject noMatch;
}
