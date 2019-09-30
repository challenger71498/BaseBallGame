using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//호출 순서 주의!
//반드시 FirstFly -> Bounding -> GuLuneDaTilEnd 순서대로 해야만 함

public class newBALL : MonoBehaviour
{
    
    static Vector3 PowerVector  = new Vector3 (0,0,0); //공이 받은 힘을 구면좌표로 표현
    static Vector2 Location = new Vector2(0,0); //공의 현재 위치로 FirstFly, Bounding, GuLuneDaTilEnd가 호출되면 setLocation을 통해 값을 바꿔야함
    public static List<Vector2> LandingLocations  = new List<Vector2>() { new Vector2(0,0)}; //가장 처음에 (0,0)추가해야 함
    //LandingLocations 추가설명 : 공이 착륙하거나 구른 결과의 좌표를 가지고 있으며 굴러서 나온 좌표는 항상 마지막 요소여야 한다.
    static float HittingPoint = 0.7f;
    public static List<float> TimeList = new List<float>(); //공이 땅에 닿을때 각 텀에 대한 시간
    public static List<float> MaxHeightList = new List<float>(); //최대높이 모음
    public static List<Vector3> BallPowerList = new List<Vector3>(); //공의 힘벡터 모음 -> 이후 특정지점의 높이 구할때 사용


    public static void Hit(float HitterStrong, float Accuracy, bool isRight) //공의 구면좌표 설정(타자의 힘, 정확도, 오른손잡이순서)
    {
        //---------------변수-------------------
        float inclination;
        float power = HitterStrong; //타자의 힘에 따라 공이 받는 힘 설정 (m/s)
        float hightAngle;

        //---------------내용-------------------
        if (isRight) //우선 오른손잡이면 공이 오른쪽으로, 왼손잡이면 공이 왼쪽으로 향하게 함. 나중에 변경 예정
        {
            if (Accuracy >= 0.8) //정확도가 높음 -> 플라이볼, 파울이 아님, 여기서는 장타만을 고려
            {
                inclination = Random.Range(30, 45); //기울기를 구장의 오른쪽을 향하도록 설정
                hightAngle = Random.Range(30,40); //높이 설정(장타)
            }
            else //파울
            {
                inclination = Random.Range(225, 360); //기울기가 오른쪽 파울영역으로 향하도록 설정
                hightAngle = Random.Range(0, 90);
            }
        }
        else
        {
            if (Accuracy >= 0.8) //정확도가 높음 -> 플라이볼, 파울이 아님, 여기서는 장타만을 고려
            {
                inclination = Random.Range((float)45, (float)90); //기울기를 구장의 왼쪽을 향하도록 설정
                hightAngle = Random.Range(20, 30); //높이 설정(장타)
            }
            else //파울
            {
                inclination = Random.Range((float)(90), (float)135); //기울기가 왼쪽 파울영역으로 향하도록 설정
                hightAngle = Random.Range(0, 90);
            }
        }
        SetBall( new Vector3(power, inclination, hightAngle));
    }


    public static void FirstFly() //공이 처음 날아갈 때 처음 위치를 고려해야 하므로 따로 만듦, 이거 호출 전 setBall 반드시 호출
    {
        //---------------변수-------------------
        float time, zSpeed, xySpeed, maxHeight, distance, tempTime, newAngle; //tempTime은 최대높이를구하기 위한 임시시간
        Vector2 v1, v2, v3;

        //---------------내용-------------------
        BallPowerList.Add(PowerVector);

        zSpeed = PowerVector.x * Mathf.Sin((Mathf.PI / 180) * PowerVector.z);
        xySpeed = PowerVector.x * Mathf.Cos((Mathf.PI / 180) * PowerVector.z);

        //근의공식이용 시간 구하기
        float a, b, c;
        a = 4.9f;
        b = (-1) * zSpeed;
        c = (-1) * HittingPoint; //선수가 공을 치는 위치
        time = ((-1) * b + Mathf.Pow((b * b - 4 * a * c), 0.5f))/(2*a);
        TimeList.Add(time);

        ////공의 최대 높이 구하기
        if (zSpeed <= 0) //공이 처음부터 땅을 향할 경우(z축 속도가 마이너스)
        {
            maxHeight = HittingPoint;
            MaxHeightList.Add(maxHeight);
        }
        else
        {
            tempTime = zSpeed / 9.8f;
            maxHeight = zSpeed * tempTime - 0.5f * 9.8f * Mathf.Pow(tempTime, 2) + HittingPoint;
            MaxHeightList.Add(maxHeight);
        }



        distance = xySpeed * time;
        Location = LandingLocations[LandingLocations.Count - 1];
        Location.x += distance * Mathf.Cos((Mathf.PI / 180) * PowerVector.y);
        Location.y += distance * Mathf.Sin((Mathf.PI / 180) * PowerVector.y);
        LandingLocations.Add(Location);

        //공의 착지시 각도 수정
        v1 = new  Vector2(distance,0);
        v2 = new Vector2(xySpeed * (time -0.05f), GetHeight(xySpeed * (time -0.05f)));
        v3 = v1 - v2;
        newAngle = Vector2.Angle(v1, v3);
        ChangeAngle(newAngle); //각도 변경
    }

    public static void Bounding() //공이 땅에 닿은 후의 공의 정보 , 1번 땅에 닿은것만 구하므로 여러번 호출해야 함
    {
        //---------------변수-------------------
        float time, zSpeed, xySpeed,maxHeight, distance;

        //---------------내용-------------------
        BallPowerList.Add(PowerVector);

        zSpeed = PowerVector.x * Mathf.Sin((Mathf.PI / 180) * PowerVector.z);
        xySpeed = PowerVector.x * Mathf.Cos((Mathf.PI / 180) * PowerVector.z);
        time = zSpeed / 9.8f * 2;
        TimeList.Add(time);
        distance = xySpeed * time;
        maxHeight = Mathf.Pow(time, 2) * 9.8f / 8;
        MaxHeightList.Add(maxHeight);
        Location = LandingLocations[LandingLocations.Count - 1];
        Location.x += distance * Mathf.Cos((Mathf.PI / 180) * PowerVector.y);
        Location.y += distance * Mathf.Sin((Mathf.PI / 180) * PowerVector.y);
        LandingLocations.Add(Location);
    }

    public static void GuLuneDaTilEnd(float r) //끝까지 구른다
    {
        //---------------변수-------------------
        float time, zSpeed, xySpeed, maxHeight, distance, register;

        //---------------내용-------------------
        BallPowerList.Add(PowerVector);

        maxHeight = 0.05f;
        MaxHeightList.Add(maxHeight);
        register = r;
        zSpeed = PowerVector.x * Mathf.Sin((Mathf.PI / 180) * PowerVector.z);
        xySpeed = PowerVector.x * Mathf.Cos((Mathf.PI / 180) * PowerVector.z);

        time = xySpeed / register;
        TimeList.Add(time);
        distance = xySpeed * time - (0.5f * register * Mathf.Pow(time, 2)); //이거 계산해보니 xySpeed*0.5f*t 로 변경가능

        Location = LandingLocations[LandingLocations.Count - 1];
        Location.x += distance * Mathf.Cos((Mathf.PI / 180) * PowerVector.y);
        Location.y += distance * Mathf.Sin((Mathf.PI / 180) * PowerVector.y);
        LandingLocations.Add(Location);
    }

    public static void GuLuneDa(Vector2 PlayerLocation, float PlayerSpeed, float r) //GuLuneDaTilEnd까지 다 호출한 후에 사용가능!
    {
        //---------------변수-------------------
        float time, saigag, xySpeed, distanceOfBP, register, distance;
        Vector2 BalltoPlayer;
        //---------------내용-------------------
        xySpeed= PowerVector.x *Mathf.Cos((Mathf.PI / 180) * PowerVector.z);
        register = r;

        //사잇각 구하기
        BalltoPlayer = PlayerLocation - LandingLocations[LandingLocations.Count - 2];
        saigag = Vector2.Angle(LandingLocations[LandingLocations.Count-2], PlayerLocation); //공이 구르기 전 좌표를 가져옴

        distanceOfBP = Vector2.Distance(LandingLocations[LandingLocations.Count - 2], PlayerLocation); //구르기 전 공과 이동하기 전 선수 사이의 거리

        //근의 공식 이용한 시간 구하기
        float a, b, c;
        a = Mathf.Pow(xySpeed, 2) - Mathf.Pow(PlayerSpeed, 2);
        b = (-1) * 2 * xySpeed * distanceOfBP * Mathf.Cos((Mathf.PI / 180) * saigag);
        c = Mathf.Pow(distanceOfBP, 2);
        time = ((-1) * b + Mathf.Pow((b * b - 4 * a * c), 0.5f))/(2*a);

        distance = time * xySpeed - 0.5f*register*Mathf.Pow(time,2);

        Location = LandingLocations[LandingLocations.Count - 2];
        Location.x += distance * Mathf.Cos((Mathf.PI / 180) * PowerVector.y);
        Location.y += distance * Mathf.Sin((Mathf.PI / 180) * PowerVector.y);
        //LandingLocations.Add(Location);


    }
    public static float GetHeight(float someDistance)
    {
        //---------------변수-------------------
        float TempTime, xySpeed, zSpeed;

        //---------------내용-------------------

        for(int i = 0; i < (LandingLocations.Count-1); i++)
        {

            if (Vector2.Distance(LandingLocations[0], LandingLocations[i + 1]) > someDistance) //LandingLocation범위에 있는 경우
            {
 
                zSpeed = BallPowerList[i].x * Mathf.Sin((Mathf.PI / 180) * BallPowerList[i].z);
                xySpeed = BallPowerList[i].x * Mathf.Cos((Mathf.PI / 180) * BallPowerList[i].z);
              
                TempTime = someDistance / xySpeed;

                if (i == 0)
                {
                    return (zSpeed * TempTime - 0.5f * 9.8f * Mathf.Pow(TempTime, 2) + HittingPoint);
                }
                else
                {
                    return (zSpeed * TempTime - 0.5f * 9.8f * Mathf.Pow(TempTime, 2));
                }              
            }
            else if(Vector2.Distance(LandingLocations[0], LandingLocations[i + 1]) == someDistance) //착지지점과 거리가 같은 경우
            {
                return 0;
            }
        }
        //여까지 오면 걍 구른거임

        return 0.05f;
    }
    
    public static void SetBall(Vector3 v)
    {
        PowerVector = v;
    }

    public static void NerfPower(float i) //i는 0~1 사이
    {
        PowerVector.x = PowerVector.x * i; //공 힘 감소
    }
    public static void SetLocation(Vector2 vec2) //공의 좌표 변경
    {
        Location = vec2;
    }
    public static void ChangeAngle(float a) //각도 변경
    {
        PowerVector.z = a;
    }
    public static float GetLastMaxHeight()
    {
        return MaxHeightList[MaxHeightList.Count - 1];
    }
    public static int GetListCount()
    {
        return TimeList.Count;
    }
    public static Vector2 GetLocation()
    {
        return Location;
    }
}
