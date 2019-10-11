using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather
{
    public enum SkyWeather
    {
        UNDEFINED, SUNNY, PARTLY_CLOUD, CLOUD, RAIN, SNOW
    }

    public static List<string> skyWeatherString = new List<string>()
    {
        "undefined", "Sunny", "Partly cloud", "Cloudy", "Rain", "Snow"
    };

    public Weather(bool isRandom = true, SkyWeather _weather = default, float _temp = 18f)
    {
        if(isRandom)
        {
            weather = (SkyWeather)UnityEngine.Random.Range(0, Enum.GetNames(typeof(SkyWeather)).Length);
            temperature = UnityEngine.Random.Range(3f, 30f);
        }
        else
        {
            weather = _weather;
            temperature = _temp;
        }
    }

    public override string ToString()
    {
        return skyWeatherString[(int)weather] + ", " + temperature.ToString("0.0") + "°C";
    }

    public SkyWeather weather;
    public float temperature;
}
