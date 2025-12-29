using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    PlaneSelection planeSelection;
    MarkerPlacement markerPlacement;

    // Start is called before the first frame update
    void Start()
    {
        planeSelection = GetComponent<PlaneSelection>();
        markerPlacement = GetComponent<MarkerPlacement>();
    }

    // force reset the game
    public void ResetGame()
    {
        GameStateManager.ResetGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
