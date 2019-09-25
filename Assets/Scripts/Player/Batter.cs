using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Batter : Player
{

    //initializer
    public Batter(int _index, PlayerData _playerData, PlayerStatistics _stats, Training.Train _train, float first = 0, float second = 0, float third = 0, float fourth = 0)
        : base(_index, _playerData, _stats, _train)
    {
        finalStats = new SerializableDict<string, float>
        {
            d = new Dictionary<string, float>() {
                {"STR", first},
                {"CTR", second},
                {"CON", third},
                {"INT", fourth}
            }
        };

        playerData.SetData(PlayerData.PP.OVERALL, GetOverall());

        CalcFinalStats();
    }

    //member functions
    public void CalcFinalStats()
    {
        //Some calculations for final stat ouput.
    }
}
