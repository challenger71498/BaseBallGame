using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePanel : MonoBehaviour
{
    public GameObject leftPanel;

    private void OnEnable()
    {
        leftPanel.SetActive(true);
    }
}
