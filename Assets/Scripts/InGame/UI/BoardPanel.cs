using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPanel : MonoBehaviour
{
    public InGameObjects InGameObjects;

    public void OnClick()
    {
        gameObject.SetActive(false);
    }
}
