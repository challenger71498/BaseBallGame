using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inGamePlayer : MonoBehaviour
{
    public Vector2 location;
    public List<Vector2> locations;
    public Player player;
    public string PlayerPosition; //이건 플레이어의 좌표가 아니라 투수, 포수와 같은 역할을 나타냄
    public float AbsPower, AbsSpeed, Absaccuracy, RealPower, RealSpeed, RealAccuracy;
    float playerTime =0 ;
    float DistanceTime = 0; //특정 거리 이동시 걸리는 시간
    int playerCallTime = -1; //인덱스로 사용되며, 플레이어의 위치리스트에서 알맞은 위치를 가져오기 위해 사용됨
   
    public void setLocation(Vector2 loc)  //변경된 위치를 리스트에 저장
    {
        locations.Add(location);
    }

    //이건 안쓸수도 있겄다
    void setBaseLocation(Vector2 loc) //이건 전략(전방배치 등)으로 인해 처음 위치가 변한 경우 위치리스트에 추가하기 전 사용된다.(즉, 처음에 이걸 사용 후 setLocation 사용할것 
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
