using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticPanel : MonoBehaviour
{
    public static GameObject focusedObject = null;
    public PlayerStatistics.PS stat;

    public void OnClick()
    {
        //Focus control.
        if (focusedObject != null)
        {
            focusedObject.GetComponent<Button>().interactable = true;
        }
        focusedObject = gameObject;
        gameObject.GetComponent<Button>().interactable = false;

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SP_graphPanel.SetActive(true);

        Player player = PlayerList.focusedObject.GetComponent<PlayerList>().player;

        //Remove remaining graphs.
        for (int i = 0; i < gameManager.SP_graphContent.transform.childCount; ++i)
        {
            Destroy(gameManager.SP_graphContent.transform.GetChild(i).gameObject);
        }

        //Make graphs.
        List<float> dataPlayer = new List<float>();
        List<float> dataAverage = new List<float>();

        for(int year = Values.date.Year; player.stats.seasonStats.d.ContainsKey(year); --year)
        {
            dataPlayer.Add(player.stats.GetSeason(stat, year));
        }

        for(int year = Values.date.Year; PlayerStatistics.statisticSum.d.ContainsKey(year); --year)
        {
            dataAverage.Add(PlayerStatistics.StatAverage(stat, year));
        }

        float min = Mathf.Min(dataPlayer.Min(), dataAverage.Min());
        float max = Mathf.Max(dataPlayer.Max(), dataAverage.Max());

        for (int i = dataPlayer.Count - 1; i >= 0; --i)
        {
            gameManager.GraphInstantiate(Values.date.Year - i, min, max, dataPlayer[i], PlayerStatistics.StatAverage(stat, Values.date.Year - i));
        }

        //High and low setting.

        float high = max + (max - min) / 7;
        float low = min - (max - min) / 7;
        if (low < 0) low = 0;

        if(dataPlayer.Max() == (int)dataPlayer.Max())
        {
            high = Mathf.RoundToInt(high);
            low = Mathf.RoundToInt(low);
            gameManager.SP_high.GetComponent<TextMeshProUGUI>().text = high.ToString("F0");
            gameManager.SP_low.GetComponent<TextMeshProUGUI>().text = low.ToString("F0");
        }
        else
        {
            gameManager.SP_high.GetComponent<TextMeshProUGUI>().text = high.ToString("F1");
            gameManager.SP_low.GetComponent<TextMeshProUGUI>().text = low.ToString("F1");
        }
    }
}
