using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all possible game states
public enum GameState
{
    Build_PlaneSelection,
    Build_MarkerPlacement,
    Build_RealWorldObjectPlacement,
    Build_AugmentVirtualObjectPlacement,
    Game,
    Win,
}


// all possible game modes
public enum GameMode
{
    Build,
    Game,
    Edit
}



public class GameStateManager : MonoBehaviour
{
    private static GameMode currentGameMode = GameMode.Build;
    private static int CurrentGameStateIndex = 0;

    // this is the game state pipeline
    private static GameState[] GameStateProgress = {
        GameState.Build_PlaneSelection,
        GameState.Build_MarkerPlacement,
        GameState.Build_RealWorldObjectPlacement,
        GameState.Build_AugmentVirtualObjectPlacement,
        GameState.Game,
        GameState.Win
    };


    // get current game mode
    public static GameMode CurrentGameMode()
    {
        return currentGameMode;
    }

    // sets the game mode
    public static GameMode SetGameMode(GameMode gameMode)
    {
        currentGameMode = gameMode;
        return currentGameMode;
    }

    // get current game state
    public static GameState CurrentGameState()
    {
        return GameStateProgress[CurrentGameStateIndex];
    }

    // progress to next game state
    public static GameState ProgressToNextGameState()
    {
        CurrentGameStateIndex++;
        if (CurrentGameStateIndex >= GameStateProgress.Length - 1)
        {
            CurrentGameStateIndex = GameStateProgress.Length - 1;
            SetGameMode(GameMode.Game);
            return GameState.Game;
        }else if (GameStateProgress[CurrentGameStateIndex] == GameState.Game)
        {
            SetGameMode(GameMode.Game);
            return GameState.Game;
        }
        
        SetGameMode(GameMode.Build);
        return GameStateProgress[CurrentGameStateIndex];
    }


    // might not need bc i can just reset scene lol
    // resets the game state to the beginning
    public static GameState ResetGameState()
    {
        CurrentGameStateIndex = 0;
        SetGameMode(GameMode.Build);
        return GameStateProgress[CurrentGameStateIndex];
    }
}
