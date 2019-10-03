using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardPanel : MonoBehaviour
{
    public InGameObjects InGameObjects;
    public List<GameObject> scoreObjects = new List<GameObject>();

    public void OnClick()
    {
        gameObject.SetActive(false);
        InGameObjects.scorePanel.gameObject.SetActive(true);
        InGameObjects.inningPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Initializes board panel.
    /// </summary>
    public void Initialize()
    {
        InGameObjects.homeTeamName.text = InGameManager.game.home.teamData.GetData(TeamData.TP.TEAM_NAME);
        InGameObjects.awayTeamName.text = InGameManager.game.away.teamData.GetData(TeamData.TP.TEAM_NAME);

        for(int i = 0; i < InGameObjects.titleLayout.transform.childCount; ++i)
        {
            Destroy(InGameObjects.titleLayout.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < InGameObjects.scoreLayout.transform.childCount; ++i)
        {
            Destroy(InGameObjects.scoreLayout.transform.GetChild(i).gameObject);
        }

        GameObject titleObject = Instantiate(InGameObjects.titlePrefab, InGameObjects.titleLayout.transform);
        titleObject.GetComponent<TextMeshProUGUI>().text = "1";
        AddScorePanel();
    }

    /// <summary>
    /// Adds a score panel to board panel.
    /// </summary>
    public void AddScorePanel()
    {
        if(!InGameManager.isGameEnd)
        {
            GameObject scoreObject = Instantiate(InGameObjects.scorePrefab, InGameObjects.scoreLayout.transform);
            scoreObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0";
            scoreObjects.Add(scoreObject);
            if (InGameManager.isBottom)
            {
                GameObject titleObject = Instantiate(InGameObjects.titlePrefab, InGameObjects.titleLayout.transform);
                titleObject.GetComponent<TextMeshProUGUI>().text = (InGameManager.currentInning + 1).ToString();
                if(InGameManager.currentInning >= 9)
                {
                    InGameManager.game.homeScoreBoard.inningScores.d.Add(0);
                }
            }
            else
            {
                if (InGameManager.currentInning >= 9)
                {
                    InGameManager.game.awayScoreBoard.inningScores.d.Add(0);
                }
            }
        }
    }

    public void UpdateLayout()
    {
        TextMeshProUGUI scoreText = scoreObjects[scoreObjects.Count - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (InGameManager.isBottom)
        {
            InGameObjects.homeTeamName.color = InGameManager.game.home.teamData.GetData(TeamData.TP.COLOR);
            InGameObjects.awayTeamName.color = Color.white;
            scoreText.text = InGameManager.game.homeScoreBoard.inningScores.d[InGameManager.currentInning - 1].ToString();
        }
        else
        {
            InGameObjects.awayTeamName.color = InGameManager.game.away.teamData.GetData(TeamData.TP.COLOR);
            InGameObjects.homeTeamName.color = Color.white;
            scoreText.text = InGameManager.game.awayScoreBoard.inningScores.d[InGameManager.currentInning - 1].ToString();
        }

        InGameObjects.homeRunText.text = InGameManager.game.homeScoreBoard.R.ToString();
        InGameObjects.awayRunText.text = InGameManager.game.awayScoreBoard.R.ToString();
        InGameObjects.homeHitText.text = InGameManager.game.homeScoreBoard.H.ToString();
        InGameObjects.awayHitText.text = InGameManager.game.awayScoreBoard.H.ToString();
        InGameObjects.homeErrorText.text = InGameManager.game.homeScoreBoard.E.ToString();
        InGameObjects.awayErrorText.text = InGameManager.game.awayScoreBoard.E.ToString();
    }
}
