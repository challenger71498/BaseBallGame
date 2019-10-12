using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldViewPanel : MonoBehaviour
{
    public GameObject leftPanel;

    private void OnEnable()
    {
        leftPanel.SetActive(false);
    }

    private void OnDisable()
    {
        leftPanel.SetActive(true);
    }
}
