using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DebuggingMode : MonoBehaviour
{
    public static string debugOutput = ""; 

    public void ForceNextProgress()
    {
        GameStateManager.ProgressToNextGameState();
        WhatProgressAmI();
    }

    
    public void ResetGame()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        // Check if a GameManager object was found
        if (gameManager != null)
        {
            // Call the ResetGame method on the found GameManager object
            gameManager.ResetGame();
        }
        else
        {
            Debug.Log("GameManager not found in the scene.");
        }
    }
    

    public void WhatProgressAmI()
    {
        Debug.Log("GameState is currently: " + GameStateManager.CurrentGameState());
    }

    public void WhatModeAmI()
    {
        Debug.Log("GameMode is currently: " + GameStateManager.CurrentGameMode());
    }
       

    public void test()
    {
        Debug.Log("Test");
    }
}
