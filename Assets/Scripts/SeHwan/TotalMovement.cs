using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalMovement : MonoBehaviour
{
    public GameObject ballObj;
    public GameObject PlayerObj1;
    public inGamePlayer inGam1;
    public GameObject PlayerObj2;
    public inGamePlayer inGam2;
    public GameObject PlayerObj3;
    public inGamePlayer inGam3;
    public GameObject PlayerObj4;
    public inGamePlayer inGam4;
    public GameObject PlayerObj5;
    public inGamePlayer inGam5;
    public List<inGamePlayer> inGams = new List<inGamePlayer>();

    public float realtime = 0;

    int level = 0;

    static public Vector3 Ball;

    Vector2 L;

    void Calculate()
    {
        ballObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1, 0);

        Ball = ball.Hit(36, 1, true);

        L = ball.LandingPlace2(ref Ball);
        Debug.Log("LnadingPlace2 이후: " + Ball.x);
        L = ball.Bounding(0.6f, L, ref Ball);
        Debug.Log("Bounding이후: " + Ball.x);
        L = ball.GuLuneDa2(Ball, L);
        Debug.Log("L : " + L);
        //ball.GuLuneDa(Ball, L, G.location, G.RealSpeed);

        //float h = ball.GetHeight(60);

    }



    public void calculatePlayer(inGamePlayer G) //공을 잡을 수비수G의 위치 계산
    {
        int i;
        bool isCatch = false;
        Vector2 playerPlace = G.location;
        Debug.Log(playerPlace);
        float fullTime = 0;
        for (i = 0; i < ball.times.Count; i++)
        {
            fullTime += ball.times[i];
            ball.realBounceConter++;
            Debug.Log("플레이어 위치: " + playerPlace + " 공의 위치: " + ball.landingPlaces[i + 1] + " 시간: " + ball.times[i]);
            Debug.Log("거리차이: " + Vector2.Distance(playerPlace, ball.landingPlaces[i + 1]) + " 선수의 거리이동: " + fullTime * G.RealSpeed);
            if (Vector2.Distance(playerPlace, ball.landingPlaces[i + 1]) <= fullTime * G.RealSpeed)
            {
                Debug.Log(Vector2.Distance(playerPlace, ball.landingPlaces[i + 1]));
                isCatch = true;
                playerPlace.x = ball.landingPlaces[i + 1].x;
                playerPlace.y = ball.landingPlaces[i + 1].y; //플레이어의 위치를 공의 위치로 변경
                //ball.realBounceConter++;
                ball.realTime = fullTime; //공이 잡힌 시간을 공의 realTime에 저장
                break;
            }
            Debug.Log(i + " 직전위치: " + ball.landingPlaces[i]);
        }
        Debug.Log("직후위치: " + ball.landingPlaces[i]);
        if (isCatch == false)
        {
            float S = Vector2.Distance(playerPlace, ball.landingPlaces[i]);
            if (S <= fullTime * G.RealSpeed)
            {
                playerPlace = ball.GuLuneDa(ballmovement.Ball, ball.landingPlaces[i], playerPlace, G.RealSpeed);
            }

            else
            {
                float Time = S / G.RealSpeed;
                ball.times.Add(Time);
                ball.maxHeights.Add(0);
            }
            ball.realBounceConter++; //<-이거?
            fullTime += ball.times[i];
            ball.realTime = fullTime;
        }
    }

    void setInGamePlayerList()
    {
        inGams.Add(inGam1);
        inGams.Add(inGam2);
        inGams.Add(inGam3);
        inGams.Add(inGam4);
        inGams.Add(inGam5);

        for (int i = 0; i < inGams.Count; i++)
        {
            RectTransform Rt = inGams[i].GetComponent<RectTransform>();
            Rt.anchoredPosition = inGams[i].location * 4;
        }
        //RectTransform Rt = PlayerObj1.GetComponent<RectTransform>();
        //Rt.anchoredPosition = inGam1.location;
    }

    inGamePlayer ballCatchPlayer(List<inGamePlayer> IGL)
    {
        return IGL[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        setInGamePlayerList();//선수들을 리스트에 정렬
        Calculate();//구르기 전까지의 공의 위치 저장
        calculatePlayer(inGam1);
        Debug.Log(ball.realBounceConter);

        for (int i = 0; i < ball.times.Count; i++)
        {
            Debug.Log(i + " : " + ball.times[i] + " : " + ball.landingPlaces[i + 1] + " : " + ball.realBounceConter);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (ball.times.Count > level && realtime >= ball.times[level])
        {
            realtime = 0;
            level++;
        }
        if (level < ball.realBounceConter)//1 이상만 들어가면 끝까지 실행
        {
            realtime += Time.deltaTime; // ball.times[level]로 왜 나눴지?
            RectTransform Rt = ballObj.GetComponent<RectTransform>();
            Rt.anchoredPosition = Vector2.Lerp(ball.landingPlaces[level] * 4, ball.landingPlaces[level + 1] * 4, realtime / ball.times[level]);
            float a = 4 * ball.maxHeights[level] / Mathf.Pow(ball.times[level], 2);
            float z = 0.04f * ((-1 * a) * Mathf.Pow((realtime - 0.5f * ball.times[level]), 2) + ball.maxHeights[level]);
            if (ball.GoToGround == false)
            {
                Rt.localScale = new Vector3(1 + z, 1 + z, 0);
            }

        }
    }
}
