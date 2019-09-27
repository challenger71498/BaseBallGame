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

    public float BallRealtime = 0;
    public float RealTime =0;

    int CatchTime = -1; //공이 몇번째에 잡히는가? index로 쓰이기 때문에 -1부터 시작
    int level = 0;

    static public Vector3 Ball;

   // Vector2 L;

    void Calculate()
    {
        ballObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1, 0);

        newBALL.Hit(45, 1, true);

        newBALL.FirstFly();
        do
        {
            newBALL.NerfPower(0.3f);
            newBALL.Bounding();
        } while (newBALL.GetLastMaxHeight()>0.1f);
        newBALL.GuLuneDaTilEnd(1.9f);

    }



    public void calculatePlayer(inGamePlayer G) //공을 잡을 수비수G의 위치 계산, 플레이어의 시간은 고려하지 않았음
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

        for(int i = 0; i < newBALL.GetListCount(); i++) //검사용
        {
            Debug.Log(i + " : " +newBALL.LandingLocations[i]  + " : " + newBALL.TimeList[i]+ " : " + newBALL.MaxHeightList[i]);
        }
        calculatePlayer(ballCatchPlayer(inGams));
    }

    // Update is called once per frame
    void Update()
    {

        //-------------------------------------------------------------------------------------------------------------------------공
        if (CatchTime >= level && BallRealtime >= newBALL.TimeList[level]) //newBALL.GetListCount()를 잠시 CatchTime으로 변경
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
        RectTransform Prt = PlayerObj1.GetComponent<RectTransform>();
        inGam1.PlusDeltaTime(Time.deltaTime);

        if(inGam1.GetTime()<=inGam1.GetDistanceTime(Vector2.Distance(newBALL.LandingLocations[level+1], inGam1.location)))
        {
            Prt.anchoredPosition = Vector2.Lerp(inGam1.location * 4, newBALL.LandingLocations[level+1] * 4, inGam1.GetTime() / inGam1.GetDistanceTime(Vector2.Distance(newBALL.LandingLocations[level + 1], inGam1.location)));
        }


        //-------------------------------------------------------------------------------------------------------------------------선수
    }
}
