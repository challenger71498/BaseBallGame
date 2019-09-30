﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalMovement : MonoBehaviour
{
    public GameObject ballObj;
    //public GameObject PlayerObj1, PlayerObj2, PlayerObj3, PlayerObj4, PlayerObj5, PlayerObj6, PlayerObj7, PlayerObj8, PlayerObj9; 리스트만 사용하면 됩니다.
    //public inGamePlayer inGam1, inGam2, inGam3, inGam4, inGam5, inGam6, inGam7, inGam8, inGam9;
    [HideInInspector] public List<inGamePlayer> inGams;
    public List<GameObject> inGamObjs;

    public float BallRealtime = 0;
    public float RealTime =0; //경기장 시간

    int FirstCatchPlayerNumber = -1;
    int CatchTime = -1; //공이 몇번째에 잡히는가? index로 쓰이기 때문에 -1부터 시작
    int level = 0;

    static public Vector3 Ball;

   // Vector2 L;

    void Calculate()
    {
        ballObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1, 0);

        newBALL.Hit(38, 1, true);

        newBALL.FirstFly();
        do
        {
            newBALL.NerfPower(0.5f);
            newBALL.Bounding();
        } while (newBALL.GetLastMaxHeight()>0.1f);
        newBALL.GuLuneDaTilEnd(1.9f);

    }



    public void CalculatePlayer(inGamePlayer G) //공을 잡을 수비수G의 위치 계산, 플레이어의 시간은 고려하지 않았음
    {
        //---------------변수-------------------
        int i;
        bool isCatch = false;
        Vector2 playerPlace = G.location; 
        float fullTime = 0;

        //---------------내용-------------------
        for (i = 0; i < newBALL.GetListCount()-1; i++) //공이 구르기 전까지 공의 착륙지점과 수비수와의 거리를 비교
        {
            CatchTime++;
            fullTime += newBALL.TimeList[i];

            Debug.Log("공과 선수 사이의 거리"+Vector2.Distance(playerPlace, newBALL.LandingLocations[i + 1]) + "vs 선수의 이동거리 : " + fullTime * G.RealSpeed);

            if (Vector2.Distance(playerPlace, newBALL.LandingLocations[i+1]) <= fullTime * G.RealSpeed)
            {                
                isCatch = true;
                playerPlace.x = newBALL.LandingLocations[i + 1].x;
                playerPlace.y = newBALL.LandingLocations[i + 1].y; //플레이어의 위치를 공의 위치로 변경
                
                break;
            }            
        } //마지막에서 2번째 인덱스를 catchTime이 가져야 함
        if (isCatch == false) //구르기 전까지 공을 잡는것을 실패, 이 부분에서는 CatchTime이 추가되지 않음(리스트의 마지막 값이 아니라 newball.location을 사용할 예정
        {
            float S = Vector2.Distance(playerPlace, newBALL.LandingLocations[newBALL.GetListCount()]);
            fullTime += newBALL.TimeList[newBALL.GetListCount()-1];
            if (fullTime*G.RealSpeed >= Vector2.Distance(playerPlace, newBALL.LandingLocations[i + 1]))
            {
                newBALL.GuLuneDa(G.location, G.RealSpeed, 1.9f); //공의 위치 변경
                playerPlace =  newBALL.GetLocation(); //플레이어 위치를 GuLuneDa실행 후 공의 위치로 변경
            }
            else
            {
                playerPlace.x = newBALL.LandingLocations[i + 1].x;
                playerPlace.y = newBALL.LandingLocations[i + 1].y; //플레이어의 위치를 공의 위치로 변경
            }

        }
    }

    //테스트

    public void calculatePlayer2(List<inGamePlayer> Gs) //공을 잡을 수비수G의 위치 계산, 플레이어의 시간은 고려하지 않았음
    {
        //---------------변수-------------------
        int i, CatchPlayerNumber;
        bool isCatch = false; 
        Vector2 playerPlace;
        float tempfullTime = 0; //realfullTime을 담기 전에 비교할 변수
        float tempDistance = 0;
        float realfullTime = 0; //정해진 수비수의 공을 잡을때까지의 시간
        int tempCatchTime = -1; //CatchTime을 최종적으로 담기 전 비교할 변수
        //---------------내용-------------------
        if(newBALL.BallPowerList[0].y >=0 && newBALL.BallPowerList[0].y <= 30)
        {
            for (int a = 1; a <= 5; a++) //선수 전체 비교
            {
                
            }
        }
        else if(newBALL.BallPowerList[0].y > 30 && newBALL.BallPowerList[0].y <= 60)
        {

        }
        else if(newBALL.BallPowerList[0].y > 60 && newBALL.BallPowerList[0].y <= 90)
        {

        }
    }
    void SetInGamePlayerList() //순서대로, 포수-투수-1루수-우익수-2루수-중견수-유격수-좌익수-3루수 순서(방향에 따른 정렬)
    {
        //여기서 초기화 하지 않고 리스트에서 바로 초기화함.
        //inGamObjs.Add(PlayerObj1);
        //inGamObjs.Add(PlayerObj2);
        //inGamObjs.Add(PlayerObj3);
        //inGamObjs.Add(PlayerObj4);
        //inGamObjs.Add(PlayerObj5);
        //inGamObjs.Add(PlayerObj6);
        //inGamObjs.Add(PlayerObj7);
        //inGamObjs.Add(PlayerObj8);
        //inGamObjs.Add(PlayerObj9);

        //inGams.Add(inGam1); //포수
        //inGams.Add(inGam2); //투수
        //inGams.Add(inGam3); //1루수
        //inGams.Add(inGam4); //우익수
        //inGams.Add(inGam5); //2루수
        //inGams.Add(inGam6); //중견수
        //inGams.Add(inGam7); //유격수
        //inGams.Add(inGam8); //좌익수
        //inGams.Add(inGam9); //3루수

        for (int i = 0; i < inGamObjs.Count; i++)
        {
            inGams.Add(inGamObjs[i].GetComponent<inGamePlayer>());  //InGame Object에서 InGamePlayer 인스턴스를 inGams에 추가함.
            RectTransform Rt = inGams[i].GetComponent<RectTransform>();
            Rt.anchoredPosition = inGams[i].location * 4;
            inGams[i].setLocation(Rt.anchoredPosition);
        }
        //RectTransform Rt = PlayerObj1.GetComponent<RectTransform>();
        //Rt.anchoredPosition = inGam1.location;
    }

    int BallCatchPlayer()
    {
        //---------------변수-------------------
        float ShortestDistance = 1000;
        float tempDistance;
        int PlayerNumber = -1; //공을 잡을 수비수의 번호
        //---------------내용-------------------
        if (newBALL.BallPowerList[0].y <= 45)
        {
            for(int i =2; i <= 5; i++)
            {
                tempDistance = Vector2.Distance(newBALL.LandingLocations[1], inGams[i].locations[0]);
                Debug.Log("선수위치: " + inGams[i].locations[0] + " 공과의 거리차 :" + tempDistance);
                if(ShortestDistance > tempDistance)
                {
                    ShortestDistance = tempDistance;
                    PlayerNumber = i;
                }
            }
            return PlayerNumber;
        }

        return 7;//이건아직 다 완성못해서 그냥 좌익수가 하도록 함
    }

    //MoveTo 함수 test
    void MoveTo(inGamePlayer player, Vector2 Goal, float startTime) //startTime은 함수 호출 시간 , 이건 나중에 수비 매커니즘이 완성되면 제작
    {
        if(RealTime < startTime || (RealTime-startTime)> player.GetDistanceTime(Vector2.Distance(player.location,Goal)))
        {

        }
    }

    //수비 시뮬레이션
    void DefMove(List<bool> Ls)
    {
        if(newBALL.BallPowerList[0].y>=30 && newBALL.BallPowerList[0].y <= 60) //중앙을 향하는 공
        {
            if(Vector2.Distance(newBALL.LandingLocations[0],newBALL.LandingLocations[1])<= 35) //외야와 내야 사이에 첫 공의 착지지점이 위치
            {

            }
        }
    }


    // Start is called before the first frame update------------------------------------------------------------------------------
    void Start()
    {
        inGams = new List<inGamePlayer>();
        SetInGamePlayerList();//선수들을 리스트에 정렬
        Calculate();//구르기 전까지의 공의 위치 저장

        for(int i = 0; i < newBALL.GetListCount(); i++) //검사용
        {
            Debug.Log(i + " : " +newBALL.LandingLocations[i]  + " : " + newBALL.TimeList[i]+ " : " + newBALL.MaxHeightList[i]);
        }
        FirstCatchPlayerNumber = BallCatchPlayer();
        CalculatePlayer(inGams[FirstCatchPlayerNumber]);
    }

    // Update is called once per frame------------------------------------------------------------------------------
    void Update()
    {
        RealTime += Time.deltaTime;
        //-------------------------------------------------------------------------------------------------------------------------공
        if (CatchTime > level && BallRealtime >= newBALL.TimeList[level]) //newBALL.GetListCount()를 잠시 CatchTime으로 변경
        {
            BallRealtime = 0;
            level++;
        }
        else if (level <= CatchTime)//1 이상만 들어가면 끝까지 실행
        {
            BallRealtime += Time.deltaTime; // ball.times[level]로 왜 나눴지?
            RectTransform Rt = ballObj.GetComponent<RectTransform>();
            Rt.anchoredPosition = Vector2.Lerp(newBALL.LandingLocations[level] * 4, newBALL.LandingLocations[level+1] * 4, BallRealtime / newBALL.TimeList[level]);

            float a = 4 * newBALL.MaxHeightList[level] / Mathf.Pow(newBALL.TimeList[level], 2);
            float z = 0.04f * ((-1 * a) * Mathf.Pow((BallRealtime - 0.5f * newBALL.TimeList[level]), 2) + newBALL.MaxHeightList[level]);
            if (ball.GoToGround == false)
            {
                if(z < 0)
                {
                    Rt.localScale = new Vector3(1 , 1 , 0);
                }
                else
                    Rt.localScale = new Vector3(1 + z, 1 + z, 0);
            }

        }
        //-------------------------------------------------------------------------------------------------------------------------공

        //-------------------------------------------------------------------------------------------------------------------------선수
        //실제로는 inGamePleyer에서 GameObject를 받아와서 그걸로 해야함, 여기서는 일단 방법을 몰라 임의의 GameObject에서 실행
        RectTransform Prt = inGamObjs[FirstCatchPlayerNumber].GetComponent<RectTransform>(); //에러나면 다 0번째 element로 바꿔라
        inGams[FirstCatchPlayerNumber].PlusDeltaTime(Time.deltaTime);  //level을 대체할 변수 만들어야 함

        if(inGams[FirstCatchPlayerNumber].GetTime()<= inGams[FirstCatchPlayerNumber].GetDistanceTime(Vector2.Distance(newBALL.LandingLocations[CatchTime+1], inGams[FirstCatchPlayerNumber].location))) //이 내부의 level인덱스를 CatchTime으로 변환
        {
            Prt.anchoredPosition = Vector2.Lerp(inGams[FirstCatchPlayerNumber].location * 4, newBALL.LandingLocations[CatchTime + 1] * 4, inGams[FirstCatchPlayerNumber].GetTime() / inGams[FirstCatchPlayerNumber].GetDistanceTime(Vector2.Distance(newBALL.LandingLocations[CatchTime + 1], inGams[FirstCatchPlayerNumber].location)));
        }


        //-------------------------------------------------------------------------------------------------------------------------선수
    }
}
