using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ballmovement : MonoBehaviour
{

    public float realtime = 0;
    public int level = 0;
    static public Vector3 Ball;
    public GameObject ballObj;
    public GameObject ballPlayer; //이건 선수 이동용
    void Calculate()
    {
        ballObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1, 0);
        //TextMeshProUGUI text = ballObj.GetComponent<TextMeshProUGUI>();
        //text.text = "hello";
        //text.text = "hello2";

        Ball = ball.Hit(25, 1, true);
        //Debug.Log("처음 기울기 : " + firstXY + " and " + firstZ);


        Vector2 L = ball.LandingPlace(0, Ball);
        //Debug.Log("처음 착륙 : " + L.x + " and " + L.y + " 최대 높이 " + maxHight);

        //ball.x = 95;
        //Vector2 L2 = LandingPlace((float)0, ball);
        //Debug.Log("처음 착륙 : " + L2.x + " and " + L2.y);


        //ball.x = 90;
        //Vector2 L3 = LandingPlace((float)0, ball);
        //Debug.Log("처음 착륙 : " + L3.x + " and " + L3.y);


        //ball.x = 105;
        //Vector2 L4 = LandingPlace((float)0, ball);
        //Debug.Log("처음 착륙 : " + L4.x + " and " + L4.y);

        L = ball.Bounding(0.5f, L, ref Ball);
        //Debug.Log("최종결과 : " + L.x + " and " + L.y);
        //Debug.Log("총 시간 : " + balltime);

        //Debug.Log("구른 후 수비수가 잡은 위치 : " + ball.GuLuneDa(ref Ball, L, new Vector2(700, 700), 70));
        ball.GuLuneDa(Ball, L, new Vector2(70, 70), 7);

        float h = ball.GetHeight(60);
       // Debug.Log("펜스에 도달했을때의 높이"+h);

        //for (int i = 0; i < ball.times.Count; i++)
        //{
        //    Debug.Log(ball.times[i]);
        //}
        for (int i = 0; i < ball.landingPlaces.Count; i++)
        {
            //Debug.Log(ball.landingPlaces[i]);
        }

        //선수위치와 공 위치 비교
        for(int i = 0; i < ball.times.Count; i++)
        {
            
        }

    }

    void Start()
    {
        Calculate();
        //Debug.Log(ball.realBounceConter);
    }

    // Update is called once per frame
    void Update()
    {
        if (realtime>=1)
        {
            realtime = 0;
            level++;
        }
        if(level < ball.times.Count)
        {
            realtime += Time.deltaTime / ball.times[level];
            RectTransform Rt = ballObj.GetComponent<RectTransform>();
            Rt.anchoredPosition = Vector2.Lerp(ball.landingPlaces[level] * 11, ball.landingPlaces[level + 1] * 11, realtime);
            float a = 4 * ball.maxHeights[level];
            float z = 0.04f * ((-1 * a) * Mathf.Pow((realtime - 0.5f), 2) + ball.maxHeights[level]);
            Rt.localScale = new Vector3(1 + z, 1 + z, 0);
        }
    }
}
