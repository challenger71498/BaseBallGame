using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium
{
    public Stadium(string _name, Weather _weather = default)
    {
        name = _name;
        if(_weather == default)
        {
            currentWeather = new Weather();
        }
        else
        {
            currentWeather = _weather;
        }
    }

    public override string ToString()
    {
        return name;
    }

    public string name;
    public Weather currentWeather;
}
