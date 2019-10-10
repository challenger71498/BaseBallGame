using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPanel : MonoBehaviour
{
    public enum Speed
    {
        UNDEFINED, NORMAL, FAST, FASTER
    }

    public static List<float> speedValue = new List<float>()
    {
        0f, 0.4f, 0.2f, 0.1f
    };

    public InGameObjects InGameObjects;
    Image image;

    public Sprite[] sprites;
    public static Speed spd;

    public void Start()
    {
        image = InGameObjects.speedButton.transform.GetChild(0).GetComponent<Image>();
        if (spd == default)
        {
            spd = Speed.NORMAL;
            image.sprite = sprites[0];
        }
    }

    public void OnClick()
    {
        //1 ~ length of enum Speed
        spd++;
        spd = (Speed)((int)spd % Enum.GetNames(typeof(Speed)).Length);
        if (spd == 0) spd++;
        
        image.sprite = sprites[(int)spd - 1];
    }
}
