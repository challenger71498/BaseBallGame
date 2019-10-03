using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public enum Language
    {
        ENGLISH, KOREAN
    }
    
    /// <summary>
    /// Initialize GlobalScript instance.
    /// </summary>
    /// <param name="_strings">String data</param>
    public GlobalScript(Dictionary<Language, string> _strings = null)
    {
        strings = new Dictionary<Language, string>();

        if(_strings != null)
        {
            strings = _strings;
        }
    }

    /// <summary>
    /// Gets string data.
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public string GetString(Language language = Language.ENGLISH)
    {
        if(strings.ContainsKey(language))
        {
            return strings[language];
        }
        else
        {
            throw new System.NullReferenceException("There is no such language " + language.ToString() + " in string data. Please check language type again.");
        }
    }
    
    /// <summary>
    /// Sets string data.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="language"></param>
    /// <param name="makeNewIfNull"></param>
    public void SetString(string str, Language language = Language.ENGLISH, bool makeNewIfNull = true)
    {
        if(strings.ContainsKey(language))
        {
            strings[language] = str;
        }
        else
        {
            if(makeNewIfNull)
            {
                strings.Add(language, str);
            }
            else
            {
                throw new System.NullReferenceException("There is no such language " + language.ToString() + " in string data. Please check language type again.");
            }
        }
    }

    public Dictionary<Language, string> strings;
}
