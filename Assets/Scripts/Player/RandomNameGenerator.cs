using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RandomNameGenerator : MonoBehaviour
{
    public static List<string> FirstNames = new List<string>();
    public static List<string> LastNames = new List<string>();

    public static void SetNameList()
    {
        string firstline;
        string lastline;

        TextAsset firstLineAsset = Resources.Load<TextAsset>("FirstName");
        
        MemoryStream firstLineMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(firstLineAsset.text));
        using (StreamReader rdr = new StreamReader(firstLineMemoryStream))
        {
            while ((firstline = rdr.ReadLine()) != null)
            {
                FirstNames.Add(firstline);
            }

        }

        TextAsset lastLineAsset = Resources.Load<TextAsset>("LastName");
        MemoryStream lastLineMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(lastLineAsset.text));
        using (StreamReader rdr = new StreamReader(lastLineMemoryStream))
        {
            while ((lastline = rdr.ReadLine()) != null)
            {
                LastNames.Add(lastline);
            }

        }
    }

    public static string MakeName()
    {
        int r1 = UnityEngine.Random.Range(0, 1219);
        int r2 = UnityEngine.Random.Range(0, 4508);
        string firstName = FirstNames[r1];
        string lastName = LastNames[r2];

        return firstName + " " + lastName;
    }
}
