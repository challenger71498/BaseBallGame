using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float inclination;//기울기
    int high; //뜬 공의 높이

    //int hitterStrength,pitcherStrength; //투수, 타자의 힘 HS,PS
    //int hitterIq, pitcherIq; //투수, 타자의 지능 
    //bool rigthHand; //오른손잡이 왼손잡이 HR, PR
    //char ballChange; //변화구 종류 BC

    //이 밑부분은 구장에서의 수비수, 주자의 위치
    public Vector2 startbase = new Vector2(0, 0);
    public Vector2 ball = new Vector2(0,0);//이건 공의 위치
    public Vector2 firstBaseMan = new Vector2(50, 0);
    public Vector2 secondBaseMan = new Vector2(50, 30);
    public Vector2 shortStop = new Vector2(30, 50);
    public Vector2 thirdBaseman = new Vector2(0, 50);
    public Vector2 leftFielder = new Vector2(20,80);
    public Vector2 rightFielder = new Vector2(80,20);
    public Vector2 centerFielder = new Vector2(65,65);
    public Vector2 Pitcher = new Vector2(20,20);
    public Vector2 Catcher = new Vector2(0,0);
    public Vector2 base1= new Vector2(40,0);
    public Vector2 base2 = new Vector2(40, 40);
    public Vector2 base3 = new Vector2(0, 40);
    public Vector2 hitter = new Vector2(0, 0);

    public void BallMovement(int HS, int PS, bool HR, bool PR, char BC) //공을 쳤을 때 공의 이동
    {
        //인수들로 이루어진 식이 있어야 되는데 야알못이라 나중에 작성, 일단은 랜덤함수
        ball.x = Random.Range(0, 100);
        ball.y = Random.Range(0, 100);
        inclination =   (ball.y)/(ball.x);
        high = 50; // 여기서는 임의로 결정, 역시 나중에 타자와 투수의 힘에 의한 식으로 바뀔 예정
    }
    
    public void ground(int hight, int ballSpeed)
    {
        if (ball.x >=0 && ball.y>=0)
        {
            if (Vector2.Distance(startbase, ball) >= 100) //홈런
            { 
                ball.x = 0;
                ball.y = 0;// 공 위치 초기화

            }
            else if(Vector2.Distance(startbase, ball) >= 70) //외야수의 영역
            {
                firstBaseMan.x = 40;
                secondBaseMan.x = 40;
                secondBaseMan.y = 40;
                thirdBaseman.y = 40;   //1루수, 2루수, 3루수 각자 루로 이동
                float LD = Vector2.Distance(ball, leftFielder);
                float RD = Vector2.Distance(ball, rightFielder);
                float CD = Vector2.Distance(ball, centerFielder);

                if(CD<= LD && CD <= RD) //중견수 공, 중견수가 거리 3 안에 있는 공을 잡을수 있을때
                {
                    int centerFielderSpeed = 10; //중견수의 속도(선수정보에서 가져와야함, 여기서는 임의의 상수로 입력함)
                    if (CD == 4) //다이빙 캐치
                    {
                        //성공시
                        centerFielder.x = ball.x;
                        centerFielder.y = ball.y;
                    }
                    else if(CD <= 3) 
                    {
                        
                    }
                    else
                    {
                        
                    } //공 사이의 거리만을 계산하는 방법도 생각중<-이걸로 할거임
                }
                if (RD < CD && RD < LD) //우익수 공
                {

                }
                if (LD < CD && LD < RD) //좌익수 공
                {

                }
            }
            else //내야수의 영역
            {

            }
        }


        
    }



}
