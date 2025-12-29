using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneSelection : MonoBehaviour
{
    public Material selectedMaterial;    // it's color when selected.
    public Material orignalMaterial;  // the default material of a plane

    ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    ARPlane selectedPlane;     // currently selected plane



    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        HideNoneSelectedPlanes();

        // if the current gamestate is build plane selection
        if (GameStateManager.CurrentGameState() == GameState.Build_PlaneSelection)
        {
            // if the user touches the screen
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    List<ARRaycastHit> hits = new List<ARRaycastHit>();
                    if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                    {
                        ARRaycastHit hit = hits[0];
                        ARPlane plane = hit.trackable as ARPlane;
                        Debug.Log("Touched object: " + hit.trackable.gameObject.name);

                        if(plane != null && plane.isActiveAndEnabled)
                        {
                            SetMainPlane(plane); // sets this as selected plane
                            GameStateManager.ProgressToNextGameState();
                        }
                    }
                }
            }
        }
    }

    // sets the main plane
    void SetMainPlane(ARPlane plane)
    {
        // set as main plain
        selectedPlane = plane;

        // change the color of the selected plane
        selectedPlane.GetComponent<Renderer>().material = selectedMaterial;
    }

    // hides all planes that have been detected and is not the selected one
    void HideNoneSelectedPlanes()
    {
        // hides new detected planes thats not the selected one.
        if (selectedPlane != null)
        {
            foreach (ARPlane plane in planeManager.trackables)
            {
                if (plane != selectedPlane)
                {
                    plane.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (ARPlane plane in planeManager.trackables)
            {
                if (plane.gameObject.activeSelf == false)
                {
                    plane.gameObject.SetActive(true);
                }
            }
        }
    }

    public ARPlane getCurrentSelectedPlane()
    {
        return selectedPlane;
    }
}
