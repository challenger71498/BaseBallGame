using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


//lastname 4508
//firstname 4945

public class randomName : MonoBehaviour
{
    //public List<string> FirstNames = new List<string>();
    //public List<string> LastNames = new List<string>();
    //public void setFirstNameList()
    //{
    //    Debug.Log("1");
    //    string firstline;
    //    string lastline;
    //    using (StreamReader rdr = new StreamReader(@"C:\Users\sh103\baseball\baseball\Assets\Resources\FirstName.txt"))
    //    {
    //        while ((firstline = rdr.ReadLine()) != null)
    //        {
    //            FirstNames.Add(firstline);

    //        }
    //    }
    //    using (StreamReader rdr = new StreamReader(@"C:\Users\sh103\baseball\baseball\Assets\Resources\LastName"))
    //    {
    //        while ((lastline = rdr.ReadLine()) != null)
    //        {
    //            LastNames.Add(lastline);
    //        }
    //    }
        //TextAsset FirstTxt = (TextAsset)Resources.Load("FirstName.txt");
        //var arrayFirst = FirstTxt.text.Split('\n');
        //foreach(var line in arrayFirst)
        //{
        //    FirstNames.Add(line);
        //}
        //TextAsset LastTxt = (TextAsset)Resources.Load("LastName.txt");
        //var arrayLast = LastTxt.text.Split('\n');
        //foreach (var line in arrayLast)
        //{
        //    LastNames.Add(line);
        //}
        //Debug.Log(LastNames[3]);
    



    // Start is called before the first frame update

    

    //이름과 성을 텍스트파일에서 리스트로 가져옴_20190803_윤
    public List<string> FirstNames = new List<string>();
    public List<string> LastNames = new List<string>();
    public void SetNameList()
    {
        string firstline;
        string lastline;
        using (StreamReader rdr = new StreamReader(@"C:\Users\sh103\baseball\baseball\Assets\Resources\FirstName.txt"))
        {
            while ((firstline = rdr.ReadLine()) != null)
            {
                FirstNames.Add(firstline);

            }

        }
        using (StreamReader rdr = new StreamReader(@"c:\users\sh103\baseball\baseball\assets\resources\lastname.txt"))
        {
            while ((lastline = rdr.ReadLine()) != null)
            {
                LastNames.Add(lastline);
            }

        }
    }

    //두 리스트를 이용해 랜덤으로 이름 생성_20190803_윤
    public string MakeName()
    {
        int r1 = UnityEngine.Random.Range(0, 4945);
        int r2 = UnityEngine.Random.Range(0, 4508);
        string firstName = FirstNames[r1];
        string lastName = LastNames[r2];

        Debug.Log(firstName + " " + lastName);
        return "";
    }
}

