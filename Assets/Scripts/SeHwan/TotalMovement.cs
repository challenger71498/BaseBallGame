using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalMovement : MonoBehaviour
{
    public GameObject ballObj;
    //public GameObject PlayerObj1, PlayerObj2, PlayerObj3, PlayerObj4, PlayerObj5, PlayerObj6, PlayerObj7, PlayerObj8, PlayerObj9; 리스트만 사용하면 됩니다.
    //public inGamePlayer inGam1, inGam2, inGam3, inGam4, inGam5, inGam6, inGam7, inGam8, inGam9;
    [HideInInspector] public List<inGamePlayer> inGams;
    public List<GameObject> inGamObjs;
    public List<RectTransform> inGamMoves;
    public List<Vector2> BaseLocation = new List<Vector2>() { new Vector2(27.3f, 0), new Vector2(27.3f, 27.3f), new Vector2(0, 27.3f) };//1,2,3루

    public float BallRealtime = 0;
    public float RealTime =0; //경기장 시간

    int FirstCatchPlayerNumber = -1;
    int CatchTime = -1; //공이 몇번째에 잡히는가? index로 쓰이기 때문에 -1부터 시작
    int level = 0;
    int Magnification = 7;

    static public Vector3 Ball;

    void Calculate()
    {

        ballObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1, 0);

        newBALL.Hit(35, 1, true);
        newBALL.FirstFly();
        if(newBALL.GetHeight(120)>3 && Vector2.Distance(new Vector2(0, 0), newBALL.LandingLocations[1]) >= 120)
        {
            newBALL.HomeRun = true;
            Debug.Log("home run!");
        }
        do
        {
            newBALL.NerfPower(0.5f);
            newBALL.Bounding();
        } while (newBALL.GetLastMaxHeight()>0.1f);
        newBALL.GuLuneDaTilEnd(1.5f);
    }


    public int BallCatchPlayer()
    {
        //---------------변수-------------------
        float tempFullTime = 0;
        float tempDistance = 0;
        int tempNumber = -1;
        float ShortTime = 1000;
        float ShortTime2 = 1000;

        //---------------내용-------------------
        for (int i = 0; i <= 8; i++)
        {
            tempFullTime = 0;
            for(int j= 0; j < newBALL.LandingLocations.Count-1; j++)// -1 이유: LandingLocations는 타 리스트보다 크기가 1 더 크다
            {
                tempFullTime += newBALL.TimeList[j];
                tempDistance = Vector2.Distance(inGams[i].locations[0], newBALL.LandingLocations[j + 1]);
                if (tempDistance <= tempFullTime * inGams[i].RealSpeed)
                {
                    if(ShortTime > tempFullTime)
                    {
                        tempNumber = i; //선수 지정
                        ShortTime = tempFullTime;
                    }
                }

            }
        }
        if (tempNumber == -1) //공이 구르기 전까지 아무도 잡지 못할 경우
        {

            for(int i = 0; i < 9; i++)
            {
                tempDistance = Vector2.Distance(inGams[i].locations[0], newBALL.LandingLocations[newBALL.LandingLocations.Count - 1]);
                if(ShortTime2 > tempDistance / inGams[i].RealSpeed)
                {
                    ShortTime2 = tempDistance / inGams[i].RealSpeed;
                    tempNumber = i;
                }
            }
            return tempNumber;
        }
        else
        {
            return tempNumber;
        }
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
                G.setLocation(playerPlace); //플레이어 위치 추가

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
                G.setLocation(playerPlace);
            }
            else
            {
                playerPlace.x = newBALL.LandingLocations[i + 1].x;
                playerPlace.y = newBALL.LandingLocations[i + 1].y; //플레이어의 위치를 공의 위치로 변경
                G.setLocation(playerPlace);
            }
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
            inGams[i].setLocation((inGams[i].location));
            Rt.anchoredPosition = inGams[i].locations[0]*Magnification;
            Debug.Log(inGams[i].locations[0]);

            inGamMoves.Add(inGamObjs[i].GetComponent<RectTransform>());
        }

    }


    //MoveTo 함수 test
    void MoveTo(int playerNum, Vector2 start ,Vector2 goal ,float startTime) //startTime은 함수 호출 시간 , 이건 나중에 수비 매커니즘이 완성되면 제작 //수정 필요
    {
        //---------------변수-------------------
        int lastIndex = inGams[playerNum].playerCallTime;
        float time = inGams[playerNum].GetDistanceTime(Vector2.Distance(inGams[playerNum].locations[lastIndex - 1], inGams[playerNum].locations[lastIndex]));

        //---------------내용-------------------       
        

        if (RealTime >= startTime && (RealTime-startTime)<= time)
        {
         
            inGamMoves[playerNum].anchoredPosition = Vector2.Lerp(inGams[playerNum].location * Magnification, goal * Magnification, (RealTime - startTime) / time);
            //inGams[playerNum].BeforeSetLocation(goal);
        }
    }

    //수비 시뮬레이션
    void setDefPlayerLocations()
    {
        if(newBALL.BallPowerList[0].y>=30 && newBALL.BallPowerList[0].y <= 60) //중앙을 향하는 공
        {
            if(Vector2.Distance(newBALL.LandingLocations[0],newBALL.LandingLocations[1])<= 40) //내야 내부에 첫 공의 착지지점이 위치
            {

            }
            else {

                if (newBALL.BallPowerList[0].y>=45)
                {
                    inGams[0].setLocation(inGams[0].location);
                    inGams[1].setLocation(inGams[1].location);
                    inGams[2].setLocation(BaseLocation[0]);
                    inGams[3].setLocation(inGams[3].location);
                    inGams[4].setLocation(BaseLocation[1]);
                    inGams[6].setLocation(new Vector2(40, 50));

                    inGams[7].setLocation(inGams[7].location);
                    inGams[8].setLocation(BaseLocation[2]);
                }
                else
                {
                    inGams[0].setLocation(inGams[0].location);
                    inGams[1].setLocation(inGams[1].location);
                    inGams[2].setLocation(BaseLocation[0]);
                    inGams[3].setLocation(inGams[3].location);
                    inGams[4].setLocation(new Vector2(50, 40));
                    inGams[6].setLocation(BaseLocation[1]);
                    inGams[7].setLocation(inGams[7].location);
                    inGams[8].setLocation(BaseLocation[2]);
                }
            }
            
        }
        else if(newBALL.BallPowerList[0].y >= 60 && newBALL.BallPowerList[0].y <= 90) // 왼쪽을 향하는 공
        {
            if (Vector2.Distance(newBALL.LandingLocations[0], newBALL.LandingLocations[1]) <= 100) //외야와 내야 사이에 첫 공의 착지지점이 위치
            {

            }
            else
            {

            }
        }
        else if(newBALL.BallPowerList[0].y >= 0 && newBALL.BallPowerList[0].y <= 30) //오른쪽을 향하는 공
        {
            if (Vector2.Distance(newBALL.LandingLocations[0], newBALL.LandingLocations[1]) <= 100) //외야와 내야 사이에 첫 공의 착지지점이 위치
            {

            }
            else
            {

            }
        }
        else
        {
           // 이 부분은 파울
        }
    }


    // Start is called before the first frame update------------------------------------------------------------------------------
    void Start()
    {
        inGams = new List<inGamePlayer>();
        SetInGamePlayerList();//선수들을 리스트에 정렬
        Calculate();//구르기 전까지의 공의 위치 저장
        for (int i = 0; i < newBALL.GetListCount(); i++) //검사용
        {
            Debug.Log(i + " : " +newBALL.LandingLocations[i]  + " : " + newBALL.TimeList[i]+ " : " + newBALL.MaxHeightList[i]);
        }
      
        
        FirstCatchPlayerNumber = BallCatchPlayer(); //공 잡을 선수 계산

        CalculatePlayer(inGams[FirstCatchPlayerNumber]); //계산된 선수가 공을 잡음
        
        newBALL.RealLandingLocations.Add(newBALL.LandingLocations[CatchTime + 1]);//공이 잡힌 위치를 저장

        setDefPlayerLocations();
    }

    // Update is called once per frame------------------------------------------------------------------------------
    void Update()
    {
        RealTime += Time.deltaTime;

        if (newBALL.HomeRun)
        {
            RectTransform Rt = ballObj.GetComponent<RectTransform>();
            Rt.anchoredPosition = Vector2.Lerp(newBALL.LandingLocations[0] * Magnification, newBALL.LandingLocations[1] * Magnification, RealTime / newBALL.TimeList[0]);
            return;
        }

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
            Rt.anchoredPosition = Vector2.Lerp(newBALL.LandingLocations[level] * Magnification, newBALL.LandingLocations[level+1] * Magnification, BallRealtime / newBALL.TimeList[level]);

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

        inGamMoves[FirstCatchPlayerNumber] = inGamObjs[FirstCatchPlayerNumber].GetComponent<RectTransform>(); //에러나면 다 0번째 element로 바꿔라
        inGams[FirstCatchPlayerNumber].PlusDeltaTime(Time.deltaTime);  //level을 대체할 변수 만들어야 함

        

        ////inGams[FirstCatchPlayerNumber.getTime를 realtime으로 변환해봄
        if (RealTime <= inGams[FirstCatchPlayerNumber].GetDistanceTime(Vector2.Distance(newBALL.LandingLocations[CatchTime + 1], inGams[FirstCatchPlayerNumber].locations[0]))) //이 내부의 level인덱스를 CatchTime으로 변환
        {
            inGamMoves[FirstCatchPlayerNumber].anchoredPosition = Vector2.Lerp(inGams[FirstCatchPlayerNumber].locations[0] * Magnification, newBALL.LandingLocations[CatchTime + 1] * Magnification, RealTime / inGams[FirstCatchPlayerNumber].GetDistanceTime(Vector2.Distance(newBALL.LandingLocations[CatchTime + 1], inGams[FirstCatchPlayerNumber].locations[0])));
        }
        for(int i = 0; i < 9; i++) //선수 수만큼 반복
        {
            if(i != FirstCatchPlayerNumber) //처음에 공을 잡는 선수
            {
                for(int a = 0; a <inGams[i].startTimes.Count; a++)
                {
                    MoveTo(i, inGams[i].locations[a],inGams[i].locations[a+1], inGams[i].startTimes[a]);
                }
            }
            else
            {
                continue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------선수
    }
}
