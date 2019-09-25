using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchersPanel : MonoBehaviour
{
    private void OnDisable()
    {
        PlayerList.focusedObject = null;
    }
}
