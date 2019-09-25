using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//단위는 1이 10cm
public static class ball
{
    //static Vector3 F = new Vector3(0, 0, 0); // 힘, xy기울기, z기울기 순서
    static int bounceStat = 0;
    static bool isGuLum = false; //공이 구르는가 여부확인
    static float ballFullTime = 0;
    static float maxHight = 0;
    public static float realTime = 0; //실제 공을 잡았을 때까지의 시간(ballfulltime은 공이 최대한 안잡혔을 때의 시간) <-이건 DefplayerMovemet에서 값이 정해짐
    public static int realBounceConter = 0; //실제 수비가 잡기 전 공이 튕겨진 횟수+1;

    public static List<float> times = new List<float>();
    public static List<Vector2> landingPlaces = new List<Vector2>();
    public static List<float> maxHeights = new List<float>();
    public static List<Vector3> ballPowers = new List<Vector3>();
    
    public static Vector3 Hit(float hitterStrong, float accuracy, bool isRight) //공의 구면좌표 설정(타자의 힘, 정확도, 오른손잡이순서)
    {
        landingPlaces.Add(new Vector2(0, 0));//처음 위치 저장
        float inclination;
        float power = hitterStrong; //타자의 힘에 따라 공이 받는 힘 설정 (m/s)
        float hightAngle;

        if (isRight) //우선 오른손잡이면 공이 오른쪽으로, 왼손잡이면 공이 왼쪽으로 향하게 함. 나중에 변경 예정
        {

            if (accuracy >= 0.8) //정확도가 높음 -> 플라이볼, 파울이 아님, 여기서는 장타만을 고려
            {
                inclination = Random.Range(30, 45); //기울기를 구장의 오른쪽을 향하도록 설정
                hightAngle = Random.Range(20, 30); //높이 설정(장타)
                //Debug.Log("높이 기울기 : " + hightAngle);

            }
            else //파울
            {
                inclination = Random.Range(225, 360); //기울기가 오른쪽 파울영역으로 향하도록 설정
                hightAngle = Random.Range(0, 90);
            }
        }
        else
        {
            if (accuracy >= 0.8) //정확도가 높음 -> 플라이볼, 파울이 아님, 여기서는 장타만을 고려
            {
                inclination = Random.Range((float)45, (float)90); //기울기를 구장의 왼쪽을 향하도록 설정
                hightAngle = Random.Range(20, 30); //높이 설정(장타)
                //Debug.Log("높이 기울기 : " + hightAngle);

            }
            else //파울
            {
                inclination = Random.Range((float)(90), (float)135); //기울기가 왼쪽 파울영역으로 향하도록 설정
                hightAngle = Random.Range(0, 90);

            }
        }

        return new Vector3(power, inclination, hightAngle);
    }

    public static Vector2 LandingPlace(float airR, Vector3 B)//순서대로 공기저항, 지형저항, 공
    {
        ballPowers.Add(B);

        float zSpeed = B.x * Mathf.Sin((Mathf.PI / 180) * B.z);
        float xySpeed = B.x * Mathf.Cos((Mathf.PI / 180) * B.z);
        float time = (zSpeed / (float)9.8) * 2;
        ballFullTime += time;
        times.Add(time);
        maxHight = time * 0.25f * (zSpeed); //최대 높이
        maxHeights.Add(maxHight);
        float S = (xySpeed + (xySpeed - time * airR)) * time / 2; //xy평면에서 이동한 거리
        float X = S * Mathf.Cos((Mathf.PI / 180) * B.y); //x좌표
        float Y = S * Mathf.Sin((Mathf.PI / 180) * B.y); //y좌표
        Debug.Log("첫 착륙 "+X + " "+Y + " 최대 높이 " + maxHight);
        Debug.Log("Time : " + time);
        //Debug.Log("zS : " + zSpeed);
        //Debug.Log("xyS : " + xySpeed);
        //Debug.Log("S : " + S);
        Vector2 V2 = new Vector2(X, Y);
        landingPlaces.Add(V2);
        return V2;
    }


    public static Vector2 Bounding(float R, Vector2 First, ref Vector3 ball) //땅에 바운스 된 후의 좌표 출력(땅의 저항, 처음 위치, 공 (공은 참조))
    {


        float ballX = 0; //공의 x좌표
        float ballY = 0; //공의 y좌표

        Vector2 nowPlace = First; //n번째 튕길때 사용할 n-1번째 주소 기억

        int i = 0;
        while (maxHight >= 1) //직전 공의 최대 높이가 10cm이상이면 bounce
        {
            bounceStat++; //공이 몇번 튕겼는가
            i++;
            ball.x = ball.x * (1 - R); //땅에 부딪혀 감소한 힘
            float zSpeed = ball.x * Mathf.Sin((Mathf.PI / 180) * ball.z);
            float xySpeed = ball.x * Mathf.Cos((Mathf.PI / 180) * ball.z);
            float time = (zSpeed / (float)9.8) * 2;
            maxHight = time * 0.25f * (zSpeed); //최대 높이
            maxHeights.Add(maxHight); //공의 크기 변동에 쓰일 최대높이 리스트
            ballFullTime += time; //총 시간 계산
            times.Add(time); //바운드 간격마다 소요된 시간
            float S = xySpeed * time; //이동한 거리
            ballX = nowPlace.x + S * Mathf.Cos((Mathf.PI / 180) * ball.y);
            ballY = nowPlace.y + S * Mathf.Sin((Mathf.PI / 180) * ball.y);
            nowPlace.x = ballX;
            nowPlace.y = ballY;
            ball.z *= 0.8f;
            landingPlaces.Add(new Vector2(nowPlace.x, nowPlace.y));
            Debug.Log(i + "번째" + nowPlace.x + " and " + nowPlace.y + " 최대 높이 " + maxHight + " 시간 "+ballFullTime );

            ballPowers.Add(ball); //공의 힘 저장 -> 나중에 특정 좌표의 높이 계산할때 사용
        }
        Vector2 V2 = new Vector2(ballX, ballY);
        landingPlaces.Add(V2);
        return V2;

        //구른다
    }

    public static Vector2 GuLuneDa(Vector3 weakBall, Vector2 ballPlace, Vector2 playerPlace, float playerSpeed)//구른다, playerPlace 와 playerSpeed는 나중에 생길 player객체의 변수
    {
        isGuLum = true;
        float time, S, A, B, C; //S는 거리, ABC는 근의공식 이용때 쓰임
        S = Vector2.Distance(ballPlace, playerPlace)-ballFullTime*playerSpeed;
        float xySpeed = weakBall.x * Mathf.Cos((Mathf.PI / 180) * weakBall.z);
        Vector2 fromBallToPlayer = playerPlace - ballPlace;
        float saigag = Vector2.Angle(ballPlace, fromBallToPlayer);
        A = (Mathf.Pow(playerSpeed, 2) - Mathf.Pow(xySpeed, 2));
        B = (2 * xySpeed * S * Mathf.Abs( Mathf.Cos((Mathf.PI / 180) * saigag)));
        C = (-1) * Mathf.Pow(S, 2);
        time = ((-1 * B) + Mathf.Pow((Mathf.Pow(B, 2) - 4 * A * C), 0.5f)) / 2 / A;
        //Debug.Log("B: " + B + "time: " + time);
        ballFullTime += time;
        times.Add(time);
        maxHeights.Add(0);
        float ballMove = time * weakBall.x;
        //Debug.Log(Mathf.Cos((Mathf.PI / 180) * weakBall.y) + " " + Mathf.Sin((Mathf.PI / 180) * weakBall.y));

        Vector2 V2 = new Vector2(ballPlace.x + ballMove * Mathf.Cos((Mathf.PI / 180) * weakBall.y), ballPlace.y + ballMove * Mathf.Sin((Mathf.PI / 180) * weakBall.y));
        landingPlaces.Add(V2);
        return V2;

    }

    public static float GetHeight(float someDistance)
    {       
            for(int i = 0; i<times.Count-1; i++) //구르기 전까지 반복
            {
                if (Vector2.Distance(landingPlaces[0], landingPlaces[i + 1]) >= someDistance)
                {
                    //Debug.Log("횟수: "+ (i+1));
                    float tempTime;
                    float tempMaxHeight = maxHeights[i];
                    tempTime =(someDistance-(Vector2.Distance(landingPlaces[0],landingPlaces[i]))) / (ballPowers[i].x * Mathf.Cos((Mathf.PI / 180) * ballPowers[i].z));
                    float alpha = 4 * tempMaxHeight / Mathf.Pow(times[i], 2);
                    return -1 * (alpha * Mathf.Pow((tempTime - times[i] / 2), 2))+tempMaxHeight;
                }
               
            }
        //Debug.Log("구른다");
        return 0;   //굴렀을 때
    }


    //class field
    //{
    //    Vector2 baseOne = new Vector2(270, 0);
    //    Vector2 baseTwo = new Vector2(270, 270);
    //    Vector2 baseThree = new Vector2(0, 270);
    //    Vector2 homeBase = new Vector2(0, 0);

    //    Vector2 RightFielder = new Vector2(140, 700);
    //    Vector2 CenterFielder = new Vector2(700, 700);
    //    Vector2 LeftFielder = new Vector2(700, 140);
    //    Vector2 FirstBaseMan = new Vector2(350, 0);
    //    Vector2 SecondBaseMan = new Vector2(350, 270);
    //    Vector2 ShortFielder = new Vector2(270, 350);
    //    Vector2 ThirdBaseMan = new Vector2(0, 350);

    //    void setFront()
    //    {
    //        RightFielder.y = 600;
    //        CenterFielder.x = 600;
    //        CenterFielder.y = 600;
    //        LeftFielder.y = 600;
    //    }


    //}

}
