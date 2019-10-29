using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldViewPanel : MonoBehaviour
{
    public InGameObjects InGameObjects;
    private void OnEnable()
    {
        InGameObjects.pauseButton.SetActive(false);
        InGameObjects.speedButton.SetActive(false);
    }

    private void OnDisable()
    {
        InGameObjects.pauseButton.SetActive(true);
        InGameObjects.speedButton.SetActive(true);
    }
}
