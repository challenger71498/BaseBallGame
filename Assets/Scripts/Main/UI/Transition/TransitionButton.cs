using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionButton : MonoBehaviour
{
    //data members
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
            //Shows transition text, and apply text to it.
            MatchUpInitialization();
        }
        else
        {
            DateManagement.ProceedDate();
            //gameManager.SaveData();
            SceneManager.LoadScene(whereTo);
        }
        StopCoroutine("TransitionFinishedCheck");
    }

    public void MatchUpInitialization()
    {
        //gameManager.SaveData();
        //Shows transition text, and apply text to it.
        gameManager.transitionTextObject.SetActive(true);
        gameManager.transitionTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Processing other team\'s games...";
        Values.league.ProceedGame(Values.date);

        //Game initialization.
        InGameManager.game = GameManager.game;
        
        SceneManager.LoadScene("InGame");
    }
}
