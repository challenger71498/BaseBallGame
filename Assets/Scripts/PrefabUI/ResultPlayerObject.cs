using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPlayerObject : MonoBehaviour
{
    public void SetText(Player player)
    {
        numberText.text = ((string)player.playerData.GetData(PlayerData.PP.NUMBER)).ToString();
        nameText.text = player.playerData.GetData(PlayerData.PP.NAME);
        positionText.text = player.playerData.GetData(PlayerData.PP.POSITION);
        if(player.GetType() == typeof(Batter))
        {
            //backgroundImage.color
            AB_IPText.text = player.stats.GetStat(PlayerStatistics.PS.AB).ToString();
            R_HText.text = player.stats.GetStat(PlayerStatistics.PS.R).ToString();
            H_ERText.text = player.stats.GetStat(PlayerStatistics.PS.H_BAT).ToString();
            BBText.text = player.stats.GetStat(PlayerStatistics.PS.BB_BAT).ToString();
            RBI_KText.text = player.stats.GetStat(PlayerStatistics.PS.RBI).ToString();
            //ratingText.text
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
