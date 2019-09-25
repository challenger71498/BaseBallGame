using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Date
{

    //member functions
    public Date(DateTime _date) {
        date = _date;
        SetOutput();
    }

    public Date(string _string)
    {
        output = _string;
    }

    public Date DeepCopy()
    {
        return new Date(date);
    }
    
    public void SetOutput()
    {
        TimeSpan t = date - Values.date;
        if (t.Days == 0)
        {
            output = "Today";
        }
        else if (t.Days == 1)
        {
            output = "Tomorrow";
        }
        else if (t.Days <= 7)
        {
            output = t.Days + " days later";
        }
        else
        {
            output = date.ToString("MMM", CultureInfo.CreateSpecificCulture("en-US")) + " " + date.ToString("dd", CultureInfo.CreateSpecificCulture("en-US")) + ", " + date.Year;
        }
    }

    public override string ToString()
    {
        return date.ToString("MMM", CultureInfo.CreateSpecificCulture("en-US")) + " " + date.ToString("dd", CultureInfo.CreateSpecificCulture("en-US")) + ", " + date.Year;
    }

    //data members
    public DateTime date;
    public string output;

}
