using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public InGameObjects InGameObjects;
    
    public void UpdateLayout()
    {
        for(int i = 0; i < 3; ++i)
        {
            if(InGameManager.runnerInBases[i+1] != null)
            {
                InGameObjects.bases[i].color = InGameManager.currentAttack.teamData.GetData(TeamData.TP.COLOR);
            }
            else
            {
                InGameObjects.bases[i].color = Colors.primary;
            }
        }
    }

    public void UpdateStealing()
    {
        for(int i = 0; i < 3; ++i)
        {
            if(InGameManager.stealingAttempts[i+1])
            {
                InGameObjects.baseStealingAttepts[i].gameObject.SetActive(true);
            }
            else
            {
                InGameObjects.baseStealingAttepts[i].gameObject.SetActive(false);
            }
        }
    }
}
