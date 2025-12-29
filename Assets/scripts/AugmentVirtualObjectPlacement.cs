using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class AugmentVirtualObjectPlacement : MonoBehaviour
{


    // 0 tree
    // 1 house
    // cart
    int selectedObject = -1;

    public GameObject[] Objects; // the object to be placed onto the plane

    public ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    ARAnchorManager anchorManager;
    PlaneSelection planeSelection;

    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        anchorManager = GetComponent<ARAnchorManager>();
        planeSelection = GetComponent<PlaneSelection>();
    }


    GameObject placedObject;

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.CurrentGameState() == GameState.Build_AugmentVirtualObjectPlacement && selectedObject >= 0 && !IsPlacedObject() && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {

                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    List<ARRaycastHit> hits = new List<ARRaycastHit>(); // to keep track of the planes that are hit.
                    raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon); // send a raycast in the direction of user's tap

                    if (hits.Count > 0)
                    {
                        ARRaycastHit hit = hits[0];

                        Pose hitPose = hit.pose;

                        // Get the plane
                        ARPlane arPlane = planeManager.GetPlane(hit.trackableId);

                        if (arPlane.trackableId == planeSelection.getCurrentSelectedPlane().trackableId)
                        {
                            // add object to the location
                            GameObject newObject = Instantiate(Objects[selectedObject], hitPose.position, hitPose.rotation);
                            placedObject = newObject;
                        }
                    }
                }
            }
        }
    }

    public bool IsPlacedObject()
    {
        return placedObject != null;
    }

    public GameObject GetCurrentPlacedObject()
    {
        return placedObject;
    }

    public void ClearPlacedObject()
    {
        placedObject = null;
    }

    public void setSelectedObject(int index)
    {
        selectedObject = index;
    }
}
