using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    public InGameObjects InGameObjects;

    public void UpdateLayout()
    {
        InGameObjects.leftScore.text = InGameManager.game.homeScoreBoard.R.ToString();
        InGameObjects.rightScore.text = InGameManager.game.awayScoreBoard.R.ToString();
    }
}
