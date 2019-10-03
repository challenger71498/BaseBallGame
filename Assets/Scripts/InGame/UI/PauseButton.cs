using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public InGameObjects InGameObjects;
    public Animator animator;

    public void OnClick()
    {
        if(InGameManager.isPaused)
        {
            InGameObjects.pausePanel.SetActive(false);
            InGameObjects.optionPanel.SetActive(false);
            InGameManager.isPaused = false;
            animator.Play("HTE Hamburger");
        }
        else
        {
            InGameObjects.pausePanel.SetActive(true);
            InGameObjects.optionPanel.SetActive(true);
            InGameManager.isPaused = true;
            animator.Play("HTE Exit");
        }
    }
}
