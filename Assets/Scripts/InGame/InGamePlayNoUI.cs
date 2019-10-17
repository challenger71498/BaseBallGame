using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePlayNoUI : InGameManager
{
    /// <summary>
    /// Hid base Start function intentionally to prevent auto start.
    /// </summary>
    public new void Start()
    {
        InGameObjects = null;
    }

    /// <summary>
    /// Proceeds game without UI elements.
    /// </summary>
    /// <param name="_game"></param>
    public void GamePlayWithoutUI(Game _game)
    {
        game = _game;
        InitializeGame(false);

        while (!isGameEnd)
        {
            Turn();
        }
    }
}
