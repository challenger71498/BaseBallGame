using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionButton : MonoBehaviour
{
    //data members
    public Game game;
    public Animator anim;
    public string whereTo = "Main";

    public GameManager gameManager;

    public void OnClick()
    {
        anim.SetBool("isTransition", true);
        StartCoroutine("TransitionFinishedCheck");
    }

    IEnumerator TransitionFinishedCheck()
    {
        //Change global value data, and reload scene after transition finishes.
        while(anim.GetBool("isTransition"))
        {
            yield return new WaitForSeconds(0.1f);
        }
        if(gameManager.isMatchUpToday)
        {
            //gameManager.SaveData();
            InGameManager.game = gameManager.game;
            SceneManager.LoadScene("InGame");
        }
        else
        {
            Values.date = Values.date.AddDays(1);
            //gameManager.SaveData();
            SceneManager.LoadScene(whereTo);
        }
        StopCoroutine("TransitionFinishedCheck");
    }
}
