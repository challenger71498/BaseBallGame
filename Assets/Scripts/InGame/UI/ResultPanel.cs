using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public InGameObjects InGameObjects;

    /// <summary>
    /// When on enabled.
    /// </summary>
    private void OnEnable()
    {
        //Disables upper buttons, and forces to show boardpanel.
        InGameObjects.speedButton.SetActive(false);
        InGameObjects.pauseButton.SetActive(false);
        InGameObjects.boardPanel.gameObject.SetActive(true);
        InGameObjects.scorePanel.gameObject.SetActive(false);
        InGameObjects.inningPanel.gameObject.SetActive(false);
        InGameObjects.boardPanel.gameObject.GetComponent<Button>().interactable = false;
    }

    /// <summary>
    /// Refreshes result panel objects.
    /// </summary>
    /// <param name="game"></param>
    public void RefreshItems(Game game)
    {
        //Team name and score.
        InGameObjects.homeTeamNameText_RP.text = game.home.ToString();
        InGameObjects.awayTeamNameText_RP.text = game.away.ToString();
        InGameObjects.homeScoreText_RP.text = game.homeScoreBoard.R.ToString();
        InGameObjects.awayScoreText_RP.text = game.awayScoreBoard.R.ToString();

        //Team won.
        InGameObjects.leftSide0_RP.gameObject.SetActive(false);
        InGameObjects.leftSide1_RP.gameObject.SetActive(false);
        InGameObjects.rightSide0_RP.gameObject.SetActive(false);
        InGameObjects.rightSide1_RP.gameObject.SetActive(false);
        if(game.GetGameResult(game.home) == Game.GameResult.WIN)
        {
            InGameObjects.homeWon_RP.SetActive(true);
            InGameObjects.awayWon_RP.SetActive(false);
            InGameObjects.leftSide0_RP.gameObject.SetActive(true);
            InGameObjects.leftSide1_RP.gameObject.SetActive(true);
            Color color = game.home.teamData.GetData(TeamData.TP.COLOR);
            color.a = 0.1f;
            InGameObjects.leftSide0_RP.color = color;
            color.a = 0.3f;
            InGameObjects.leftSide1_RP.color = color;
        }
        else if (game.GetGameResult(game.home) == Game.GameResult.LOSS)
        {
            InGameObjects.homeWon_RP.SetActive(false);
            InGameObjects.awayWon_RP.SetActive(true);
            InGameObjects.rightSide0_RP.gameObject.SetActive(true);
            InGameObjects.rightSide1_RP.gameObject.SetActive(true);
            Color color = game.away.teamData.GetData(TeamData.TP.COLOR);
            color.a = 0.1f;
            InGameObjects.rightSide0_RP.color = color;
            color.a = 0.3f;
            InGameObjects.rightSide1_RP.color = color;
        }
        else
        {
            InGameObjects.homeWon_RP.SetActive(false);
            InGameObjects.awayWon_RP.SetActive(false);
        }

        //Team win-loss.
        InGameObjects.homeWinLossText_RP.text = game.home.teamStats.GetData(TeamStatistics.TS.WIN).ToString() + "W " + game.home.teamStats.GetData(TeamStatistics.TS.LOSS).ToString() + "L";
        InGameObjects.awayWinLossText_RP.text = game.away.teamStats.GetData(TeamStatistics.TS.WIN).ToString() + "W " + game.away.teamStats.GetData(TeamStatistics.TS.LOSS).ToString() + "L";
        //Stadium and weather.
        Stadium stadium = (Stadium)game.home.teamData.GetData(TeamData.TP.STADIUM);
        InGameObjects.stadiumNameText_RP.text = stadium.ToString();
        InGameObjects.weatherText_RP.text = stadium.currentWeather.ToString();

        //Players.
        for(int i = 1; i < InGameObjects.homeBatters_RP.transform.childCount; ++i)
        {
            Destroy(InGameObjects.homeBatters_RP.transform.GetChild(i).gameObject);
        }
        for (int i = 1; i < InGameObjects.homePitchers_RP.transform.childCount; ++i)
        {
            Destroy(InGameObjects.homePitchers_RP.transform.GetChild(i).gameObject);
        }
        for (int i = 1; i < InGameObjects.awayBatters_RP.transform.childCount; ++i)
        {
            Destroy(InGameObjects.awayBatters_RP.transform.GetChild(i).gameObject);
        }
        for (int i = 1; i < InGameObjects.awayPitchers_RP.transform.childCount; ++i)
        {
            Destroy(InGameObjects.awayPitchers_RP.transform.GetChild(i).gameObject);
        }

        //Home
        foreach (Batter batter in game.homeBatterSet)
        {
            GameObject resultPlayerObject = Instantiate(InGameObjects.resultPlayerPrefab, InGameObjects.homeBatters_RP.transform);
            resultPlayerObject.GetComponent<ResultPlayerPrefab>().SetText(batter, game.home, game);
        }
        foreach(Pitcher pitcher in game.homePitcherSet)
        {
            GameObject resultPlayerObject = Instantiate(InGameObjects.resultPlayerPrefab, InGameObjects.homePitchers_RP.transform);
            resultPlayerObject.GetComponent<ResultPlayerPrefab>().SetText(pitcher, game.home, game);
        }
        //Away
        foreach (Batter batter in game.awayBatterSet)
        {
            GameObject resultPlayerObject = Instantiate(InGameObjects.resultPlayerPrefab, InGameObjects.awayBatters_RP.transform);
            resultPlayerObject.GetComponent<ResultPlayerPrefab>().SetText(batter, game.away, game);
        }
        foreach (Pitcher pitcher in game.awayPitcherSet)
        {
            GameObject resultPlayerObject = Instantiate(InGameObjects.resultPlayerPrefab, InGameObjects.awayPitchers_RP.transform);
            resultPlayerObject.GetComponent<ResultPlayerPrefab>().SetText(pitcher, game.away, game);
        }
    }
}
