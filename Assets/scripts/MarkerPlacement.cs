using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Marker
{
    teleportation_1,
    teleportation_2,
    par,
    start,
}


public class MarkerPlacement : MonoBehaviour
{

    bool teleportation_1 = false;
    bool teleportation_2 = false;
    bool par = false;
    bool start = false;

    void Update()
    {
        if(GameStateManager.CurrentGameState() == GameState.Build_MarkerPlacement &&
            teleportation_1 && teleportation_2 && par && start)
        {
            GameStateManager.ProgressToNextGameState();
        }
    }

    // Called when an image is discovered
    public void OnImageDiscovered(int marker)
    {

        switch (marker)
        {
            case (int)Marker.par:
                par = true;
                break;
            case (int)Marker.start:
                start = true;
                break;
            case (int)Marker.teleportation_1:
                teleportation_1 = true;
                break;
            case (int)Marker.teleportation_2:
                teleportation_2 = true;
                break;
        }
    }

}
