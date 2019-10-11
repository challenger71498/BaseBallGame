using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPlayerObject : MonoBehaviour
{
    public void SetText(Player player, Team team, Game game)
    {
        numberText.text = ((int)player.playerData.GetData(PlayerData.PP.NUMBER)).ToString();
        nameText.text = player.playerData.GetData(PlayerData.PP.NAME);
        positionText.text = Player.positionStringShort[(int)player.playerData.GetData(PlayerData.PP.POSITION)];
        if(player.GetType() == typeof(Batter))
        {
            if(game.playerOfTheMatch == player)
            {
                Color color = team.teamData.GetData(TeamData.TP.COLOR);
                color.a = 0.6f;
                backgroundImage.color = color;
            }
            else
            {
                backgroundImage.color = Colors.primaryDark;
            }
            AB_IPText.text = player.stats.GetStat(PlayerStatistics.PS.AB).ToString();
            R_HText.text = player.stats.GetStat(PlayerStatistics.PS.R).ToString();
            H_ERText.text = player.stats.GetStat(PlayerStatistics.PS.H_BAT).ToString();
            BBText.text = player.stats.GetStat(PlayerStatistics.PS.BB_BAT).ToString();
            RBI_KText.text = player.stats.GetStat(PlayerStatistics.PS.RBI).ToString();
            ratingText.text = player.stats.GetStat(PlayerStatistics.PS.RAT).ToString("0.0");
        }
        if (player.GetType() == typeof(Pitcher))
        {
            if (game.playerOfTheMatch == player)
            {
                backgroundImage.color = Colors.redDark;
            }
            else
            {
                backgroundImage.color = Colors.primaryDark;
            }
            AB_IPText.text = player.stats.GetStat(PlayerStatistics.PS.IP).ToString("0.#");
            R_HText.text = player.stats.GetStat(PlayerStatistics.PS.H_PIT).ToString();
            H_ERText.text = player.stats.GetStat(PlayerStatistics.PS.ER).ToString();
            BBText.text = player.stats.GetStat(PlayerStatistics.PS.BB_PIT).ToString();
            RBI_KText.text = player.stats.GetStat(PlayerStatistics.PS.K_PIT).ToString();
            ratingText.text = player.stats.GetStat(PlayerStatistics.PS.RAT).ToString("0.0");
        }
    }

    public Image backgroundImage;
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI AB_IPText;
    public TextMeshProUGUI R_HText;
    public TextMeshProUGUI H_ERText;
    public TextMeshProUGUI BBText;
    public TextMeshProUGUI RBI_KText;
    public TextMeshProUGUI ratingText;
}
