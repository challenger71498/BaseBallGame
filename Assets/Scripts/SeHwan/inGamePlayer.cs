﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inGamePlayer : MonoBehaviour
{
    public Vector2 location;
    public Player player;
    public float AbsPower, AbsSpeed, Absaccuracy, RealPower, RealSpeed, RealAccuracy;
    float playerTime =0 ;
    float DistanceTime = 0; //특정 거리 이동시 걸리는 시간
   
    void setLocation(Vector2 loc)  //위치 변경
    {
        location = loc;
    }

    Vector2 getLocation()
    {
        return location;
    }

    public void PlusDeltaTime(float t)
    {
        playerTime += t;
    }
    public void ResetTime()
    {
        playerTime = 0;
    }
    public float GetTime()
    {
        return playerTime;
    }
    public float GetDistanceTime(float distance)
    {
        DistanceTime = distance / RealSpeed;
        return DistanceTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        //AbsPower = player.playerData.GetDictData(PlayerData.PP.STRENGTH);
        //AbsSpeed = player.playerData.GetDictData(PlayerData.PP.AGILITY);
        //Absaccuracy = player.playerData.GetDictData(PlayerData.PP.CONCENTRATION);
        //AbsSpeed = 100;
        //AbsPower = 90;
        //Absaccuracy = 88; //가정값

        //location = new Vector2(500, 500);

        //RealSpeed = 5 + 0.02f * AbsSpeed; //초속 (m/s)
       //RealPower = 25 + 0.2f * AbsPower; //친 공의 속도
       // RealAccuracy = 0.84f + 0.0015f * Absaccuracy; //정확도(0~1)

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
