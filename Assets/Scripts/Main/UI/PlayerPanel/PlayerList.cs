using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class PlayerList : MonoBehaviour {
    
    //prefabs
    public GameObject statComponent;
    public GameObject statSmallComponent;

    //public static variables
    public static GameObject focusedObject;
    public static GameObject focusedPrevObject;
    public static Player playerPrev;

    //player data
    public Player player;

    private void OnEnable()
    {
        StartCoroutine(Check());
    }

    private void OnDisable()
    {
        StopCoroutine(Check());
        //focusedObject = null;
    }

    IEnumerator Check()
    {
        yield return new WaitForFixedUpdate();
        if (focusedObject == null && !GameObject.Find("PitchersPanel"))
        {
            focusedObject = gameObject;
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (PlayerTabButton.focusedObject != null) PlayerTabButton.focusedObject.SetActive(true);
            else gameManager.playersPanel.SetActive(true);

            foreach(GameObject diamondBG in gameManager.PIP_diamondBGs)
            {
                diamondBG.SetActive(false);
                yield return new WaitForFixedUpdate();
                diamondBG.SetActive(true);
            }

            gameManager.RP_diamond.gameObject.SetActive(false);
            gameManager.RP_diamondSecond.gameObject.SetActive(false);
            yield return new WaitForFixedUpdate();
            gameManager.RP_diamond.gameObject.SetActive(true);
            gameManager.RP_diamondSecond.gameObject.SetActive(true);

            if(GameObject.Find("RoastersPanel"))
            {
                OnClick(gameManager, PlayerView.COMPARE);
            }
            else
            {
                OnClick(gameManager);
            }
        }
    }

    public enum PlayerView
    {
        UNDEFINED, SKILLS_STATISTICS_TRAININGS, ROASTER, COMPARE
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="GameManager"></param>
    /// <param name="playerView">Which view you have now.</param>
    public void OnClick(GameManager GameManager, PlayerView playerView = PlayerView.SKILLS_STATISTICS_TRAININGS, bool isFieldView = false) {
        //Toggle
        if(focusedObject != null) {
            focusedObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            GameManager.skillPanel.SetActive(true);
        }

        if (!isFieldView)
        {
            focusedObject = gameObject;
            GetComponent<Button>().interactable = false;
        }

        //Data application

        if(playerView == PlayerView.SKILLS_STATISTICS_TRAININGS)
        {
            GameManager.skillPanel.GetComponent<SkillsPanel>().RefreshByPlayer(player);
            GameManager.statisticsPanel.GetComponent<StatisticsPanel>().RefreshByPlayer(player);
            GameManager.trainingPanel.GetComponent<TrainingsPanel>().RefreshByPlayer(player);
        }   
        else if (playerView == PlayerView.ROASTER)
        {
            playerPrev = player;
            GameManager.roastersPanel.GetComponent<RoastersPanel>().RefreshByPlayer(player);
        }
        else if (playerView == PlayerView.COMPARE)
        {
            GameManager.roastersPanel.GetComponent<RoastersPanel>().CompareByPlayer(playerPrev, player);
        }
        else if (playerView == PlayerView.ORDER)
        {
            if(focusedPrevObject == null)
            {
                ChangeButton.playerFirst = player;
                ChangeButton.positionFirst = player.playerData.GetData(PlayerData.PP.POSITION);
                focusedPrevObject = gameObject;
                GetComponent<Image>().color = Colors.skyblue;
            }
            else
            {
                ChangeButton.playerSecond = player;
                ChangeButton.positionSecond = player.playerData.GetData(PlayerData.PP.POSITION);
                focusedPrevObject.GetComponent<Image>().color = Colors.primarySemiDark;
                GetComponent<Button>().interactable = true;
                ChangeButton.SwapPlayer(ChangeButton.playerFirst, ChangeButton.playerSecond, Values.myTeam);
                focusedPrevObject = null;
                RoastersPanel.Refresh(gameManager);
            }
        }
    }
}