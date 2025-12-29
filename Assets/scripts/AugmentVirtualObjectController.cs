using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentVirtualObjectController : MonoBehaviour
{
    public AugmentVirtualObjectPlacement augmentVirtualObjectPlacement;
    public GameObject controller;
    public GameObject objectSelector;
    public GameObject[] currentSelectedObject;
    public GameObject nextStageButton;

    GameObject placedObject;
    float moveValue = 0.1f;
    float rotationDegree = 10;

    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.CurrentGameState() == GameState.Build_AugmentVirtualObjectPlacement)
        {
            objectSelector.SetActive(true);
            nextStageButton.SetActive(true);
        }
        else
        {
            objectSelector.SetActive(false);
            nextStageButton.SetActive(false);
        }
        if (GameStateManager.CurrentGameState() == GameState.Build_AugmentVirtualObjectPlacement && augmentVirtualObjectPlacement.IsPlacedObject())
        {
            // enable the controllers
            controller.SetActive(true);


            // grab the placedObject
            placedObject = augmentVirtualObjectPlacement.GetCurrentPlacedObject();
        }
        else
        {
            controller.SetActive(false);
            placedObject = null;
        }
    }

    public void ChangeSelectedObject(int index)
    {
        augmentVirtualObjectPlacement.setSelectedObject(index);
        hideAllSelectorArrows();
        currentSelectedObject[index].SetActive(true);
    }


    void hideAllSelectorArrows()
    {
        foreach(GameObject arrow in currentSelectedObject)
        {
            arrow.SetActive(false);
        }
    }

    public void MoveUp()
    {
        Debug.Log("moving up");
        Vector3 forwardOnPlane = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);


        placedObject.transform.position += forwardOnPlane * moveValue;
    }

    public void MoveDown()
    {
        Debug.Log("moving down");
        Vector3 forwardOnPlane = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);


        placedObject.transform.position -= forwardOnPlane * moveValue;
    }

    public void MoveLeft()
    {
        Debug.Log("moving left");
        Vector3 rightOnPlane = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);


        placedObject.transform.position -= rightOnPlane * moveValue;
    }

    public void MoveRight()
    {
        Debug.Log("moving right");
        Vector3 rightOnPlane = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);

        placedObject.transform.position += rightOnPlane * moveValue;
    }

    public void RotateRight()
    {
        placedObject.transform.Rotate(0, rotationDegree, 0);
    }

    public void RotateLeft()
    {
        placedObject.transform.Rotate(0, -rotationDegree, 0);
    }

    public void NextStage()
    {
        GameStateManager.ProgressToNextGameState();
        Done();
    }

    public void Done()
    {
        controller.SetActive(false);
        placedObject = null;
        augmentVirtualObjectPlacement.ClearPlacedObject();
    }
}
