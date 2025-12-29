using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RealWorldObjectController : MonoBehaviour
{

    public RealWorldObjectPlacement realWorldObjectPlacement;
    public GameObject controller;
    public GameObject nextStageButton;

    GameObject placedCube;
    float scaleFactor = 0.2f;
    float rotationDegree = 10; 

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.CurrentGameState() == GameState.Build_RealWorldObjectPlacement && realWorldObjectPlacement.IsPlacedCube())
        {
            // enable the controllers
            controller.SetActive(true);


            // grab the placedCube
            placedCube = realWorldObjectPlacement.GetCurrentPlacedCube();
        }

        if (GameStateManager.CurrentGameState() == GameState.Build_RealWorldObjectPlacement && realWorldObjectPlacement.MinimumCubesPlacedAchieved())
        {
            nextStageButton.SetActive(true);
        }
        else
        {
            nextStageButton.SetActive(false);
        }
    }


    public void ScaleUp()
    {
        placedCube.transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    public void ScaleDown()
    {
        placedCube.transform.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    public void RotateRight()
    {
        placedCube.transform.Rotate(0, rotationDegree, 0);
    }

    public void RotateLeft()
    {
        placedCube.transform.Rotate(0, -rotationDegree, 0);
    }

    public void NextStage()
    {
        GameStateManager.ProgressToNextGameState();
        Done();
    }

    public void Done()
    {
        controller.SetActive(false);
        placedCube = null;
        realWorldObjectPlacement.ClearPlacedCube();
    }
}
