using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabPanel : MonoBehaviour
{
    public GameObject initialFocus;
    public GameManager gameManager;

    private void OnEnable()
    {
        initialFocus.GetComponent<PlayerTabButton>().OnClick();
        initialFocus.GetComponent<PlayerTabButton>().panel.SetActive(false);
        StartCoroutine("Active");
    }

    private void OnDisable()
    {
        StopCoroutine("Active");
    }

    IEnumerator Active()
    {
        yield return new WaitForFixedUpdate();
        initialFocus.GetComponent<PlayerTabButton>().panel.SetActive(true);
    }

}
