using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningDisplay : MonoBehaviour
{
    public GameObject controller;

    void Update()
    {
        GameState mode = GameStateManager.CurrentGameState();


        if (mode == GameState.Win)
        {
            controller.SetActive(true);
        }
        else
        {
            controller.SetActive(false);
        }
        
    }
}
