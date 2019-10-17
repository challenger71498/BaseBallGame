using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticPrefab : MonoBehaviour
{
    [Header("Prefabs")]
    public static Prefabs Prefabs;

    [Header("GameObjects")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI averageText;
    public TextMeshProUGUI ranking;

    public void Awake()
    {
        Prefabs = GameObject.Find("Prefabs").GetComponent<Prefabs>();
    }

    public void SetByStat(Player player, PlayerStatistics.PS stat)
    {
        SetTitle(PlayerStatistics.PSStringShort[(int)stat]);
        SetValueText(stat, player.stats.GetSeason(stat), PlayerStatistics.StatAverage(stat, Values.date.Year), 1);
    }

    public void SetTitle(string value)
    {
        title.text = value;
    }

    public void SetValueText(PlayerStatistics.PS stat, float player, float average, float ranking)
    {
        if (player == (int)player)
        {
            playerText.text = Mathf.FloorToInt(player).ToString();
            averageText.text = Mathf.FloorToInt(average).ToString("F0");
        }
        else
        {
            playerText.text = player.ToString("F3");
            averageText.text = average.ToString("F3");
        }

        if (PlayerStatistics.lowerBetter.Contains(stat))
        {
            if (player > average * 1.1f)
            {
                playerText.color = Colors.red;
                playerText.alpha = 0.5f;
            }
            else if (player < average * 0.8f)
            {
                playerText.color = Colors.green;
            }
        }
        else
        {
            if (player < average * 0.9f)
            {
                playerText.color = Colors.red;
                playerText.alpha = 0.5f;
            }
            else if (player > average * 1.2f)
            {
                playerText.color = Colors.green;
            }
        }
    }

    /// <summary>
    /// Instantiates statistics prefab.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="player"></param>
    /// <param name="average"></param>
    /// <param name="rank"></param>
    public static void StatisticsInstantiate(Player player, PlayerStatistics.PS stat, Transform parentTransform)
    {
        Prefabs = GameObject.Find("Prefabs").GetComponent<Prefabs>();
        GameObject statObject = Instantiate(Prefabs.statistics, parentTransform);
        StatisticPanel statisticPanel = statObject.GetComponent<StatisticPanel>();
        statisticPanel.stat = stat;
        StatisticPrefab statisticPrefab = statObject.GetComponent<StatisticPrefab>();
        statisticPrefab.SetByStat(player, stat);
    }
}
