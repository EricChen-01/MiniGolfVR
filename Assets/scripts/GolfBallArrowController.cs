using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallArrowController : MonoBehaviour
{

    public Canvas arrowCanvas;
    public Camera mainCamera;
    public GameObject golfBall;
    public float distance = 1;

    // Update is called once per frame
    void Update()
    {
        // Set the canvas position to be the same as the golf ball's position
        arrowCanvas.transform.position = golfBall.transform.position;

        arrowCanvas.transform.rotation = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0);

        // run if the current game mode is Game
        if (GameStateManager.CurrentGameMode() == GameMode.Game)
        {
            float distanceBetween = Vector3.Distance(mainCamera.transform.position,golfBall.transform.position);

            if(distanceBetween <= distance)
            {
                arrowCanvas.gameObject.SetActive(true);
            }
            else
            {
                // disable the canvas
                arrowCanvas.gameObject.SetActive(false);
            }

        }
        else
        {
            // disable the canvas
            arrowCanvas.gameObject.SetActive(false);
        }
    }
}
