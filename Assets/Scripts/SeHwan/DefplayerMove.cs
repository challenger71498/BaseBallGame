using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefplayerMove : MonoBehaviour
{
    public GameObject playerObj;
    public inGamePlayer inGam;
    public Vector2 playerPlace;
    public float speed;
    public float fullTime = 0;


    public void calculatePlayer() //공을 잡을 수비수의 위치 계산
    {
        int i;
        bool isCatch = false;
        for (i = 0; i < ball.times.Count - 1; i++)
        {
            fullTime += ball.times[i];
            if (Vector2.Distance(playerPlace, ball.landingPlaces[i + 1]) <= fullTime * speed)
            {
                ball.realBounceConter++;
                isCatch = true;
                playerPlace.x = ball.landingPlaces[i + 1].x;
                playerPlace.y = ball.landingPlaces[i + 1].y; //플레이어의 위치를 공의 위치로 변경
                ball.realBounceConter++;
                ball.realTime = fullTime; //공이 잡힌 시간을 공의 realTime에 저장
                break;
            }
        }
        if (isCatch == false)
        {
            fullTime += ball.times[i];
            playerPlace = ball.GuLuneDa(ballmovement.Ball, ball.landingPlaces[i], playerPlace, speed);
            ball.realTime = fullTime;
        }
    }

    // Start is called before the first frame update


    void Start()
    {

        //speed = inGam.RealSpeed;
        //Debug.Log(ball.maxHeights[0]);//start함수 실행순서?
        //playerPlace = inGam.location;
        //RectTransform Rt = playerObj.GetComponent<RectTransform>();
        //Rt.anchoredPosition = playerPlace;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
