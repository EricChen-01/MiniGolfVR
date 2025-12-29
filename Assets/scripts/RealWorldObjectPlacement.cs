using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;


public class RealWorldObjectPlacement : MonoBehaviour
{
    public GameObject cube; // the cube to be placed onto the plane

    public ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    ARAnchorManager anchorManager;
    PlaneSelection planeSelection;

    Dictionary<ARAnchor, GameObject> anchorCubeMap = new Dictionary<ARAnchor, GameObject>();
    int MinCubes = 2;
    int placedCounter = 0;


    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        anchorManager = GetComponent<ARAnchorManager>();
        planeSelection = GetComponent<PlaneSelection>();
    }


    GameObject placedCube;

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.CurrentGameState() == GameState.Build_RealWorldObjectPlacement && !IsPlacedCube() && Input.touchCount > 0)
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
                            // add anchor to plane
                            GameObject anchorObject = new GameObject("ARAnchor");
                            anchorObject.transform.SetParent(arPlane.transform);
                            anchorObject.transform.position = hitPose.position;
                            anchorObject.transform.rotation = hitPose.rotation;

                            ARAnchor anchor = anchorObject.gameObject.AddComponent<ARAnchor>();


                            // add cube to at the location
                            GameObject newCube = Instantiate(cube, hitPose.position, hitPose.rotation);

                            // add anchor into dictionary to be kept updated
                            anchorCubeMap[anchor] = newCube;

                            placedCube = newCube;
                            placedCounter++;
                        }
                    }
                }
            }
        }

        // update their positions for more precision as AR application gets more data
        foreach (var anchor in anchorManager.trackables)
        {
            if (anchorCubeMap.ContainsKey(anchor))
            {
                GameObject cube = anchorCubeMap[anchor];
                cube.transform.position = anchor.transform.position;
            }
        }
    }

    public bool IsPlacedCube()
    {
        return placedCube != null;
    }

    public GameObject GetCurrentPlacedCube()
    {
        return placedCube;
    }

    public void ClearPlacedCube()
    {
        placedCube = null;
    }

    public bool MinimumCubesPlacedAchieved()
    {
        return placedCounter >= MinCubes;
    }

}
