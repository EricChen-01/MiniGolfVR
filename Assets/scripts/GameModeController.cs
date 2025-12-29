using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour
{

    public GameObject controller;
    public GolfBallController golfBall;


    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.CurrentGameState() == GameState.Game)
        {
            controller.SetActive(true);
        }
        else
        {
            controller.SetActive(false);
        }
    }


    public void IncreaseTheta(float angle)
    {
        golfBall.hitAngle += angle;
        if(golfBall.hitAngle > 45)
        {
            golfBall.hitAngle = 45;
        }

        Debug.Log("HitAngle is now: " + golfBall.hitAngle);
    }

    public void DereaseTheta(float angle)
    {
        golfBall.hitAngle -= angle;

        if(golfBall.hitAngle < 0)
        {
            golfBall.hitAngle = 0;
        }

        Debug.Log("HitAngle is now: " + golfBall.hitAngle);
    }

    public void Respawn()
    {
        if(golfBall.getSavedLocation() != null)
        {
            golfBall.TeleportBack();
        }
    }
}
