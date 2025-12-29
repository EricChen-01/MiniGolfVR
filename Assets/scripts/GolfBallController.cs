using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class GolfBallController : MonoBehaviour
{
    public PlaneSelection planeSelection;

    public float hitForce = 1f;
    public float hitAngle = 0f;
    public float drag = 0.7f;
    public float angularDrag = 0.5f;


    public bool golfBallPlaced = false;

    public GameObject golfball;


    int currentSavedLocationindex = 0;
    List<Vector3> savedLocations = new List<Vector3>();



    private RaycastHit hit;
    private bool golfballHit = false;
    private int totalTeleports = 0;
    void Update()
    {

        // stop the golfball from falling through the plane
        if (GameStateManager.CurrentGameMode() == GameMode.Game && golfBallPlaced)
        {
            ARPlane plane = planeSelection.getCurrentSelectedPlane();

            if (golfball.transform.position.y < plane.transform.position.y) {
                totalTeleports++;
                Debug.Log("teleporting back the golf ball");
                golfball.transform.position = new Vector3(golfball.transform.position.x, plane.transform.position.y + 0.3f, golfball.transform.position.z);
            }

            // if the golf ball keeps falling through the plane or falls off the plane telelport it back to it's last safe position!
            if(totalTeleports > 10)
            {
                totalTeleports = 0;
                TeleportBack();
            }

        }

        // place golf ball ontop of the plane when game starts!
        if (GameStateManager.CurrentGameMode() == GameMode.Game && !golfBallPlaced)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                transform.position = hit.point;
            }

            golfball.AddComponent<Rigidbody>();
            golfball.GetComponent<Rigidbody>().useGravity = true;
            golfball.GetComponent<Rigidbody>().drag = drag;
            golfball.GetComponent<Rigidbody>().angularDrag = angularDrag;
            saveCurrentLocation();
            transform.parent = null;
            golfBallPlaced = true;
        }

        // Check for tap input
        if (GameStateManager.CurrentGameMode() == GameMode.Game && Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (PerformRayCast(touch))
                    {
                        saveCurrentLocation();
                        golfballHit = true;
                    }
                    break;
                case TouchPhase.Ended:
                    if (golfballHit)
                    {
                        PerformGolfHit(golfball.GetComponent<Rigidbody>());
                        golfballHit = false;
                    }
                    break;
            }
        }
    }

    public List<Vector3> getSavedLocation()
    {
        return savedLocations;
    }

    public void saveCurrentLocation()
    {
        // save the current location of the golf ball just in case user wants to go back.
        savedLocations.Insert(0, golfball.transform.position);
    }

    public void TeleportBack()
    {
        if (savedLocations.Count != 0)
        {
            golfball.transform.position = savedLocations[currentSavedLocationindex++];
            golfball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            golfball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            if(currentSavedLocationindex >= savedLocations.Count)
            {
                currentSavedLocationindex = savedLocations.Count - 1;
            }
        }
    }

    public bool PerformRayCast(Touch touch)
    {
        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            int ignoreRaycastLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreRaycastLayerMask))
            {
                Debug.Log("This is a " + hit.collider.tag);
                // Check if the tapped object is this golf ball
                if (hit.collider.CompareTag("GolfBall"))
                {
                    return true;
                }

            }
        }

        return false;
    }

    public void PerformGolfHit(Rigidbody rb)
    { 
        
        Debug.Log("HitAngle is: " + hitAngle);
        Vector3 forwardDirection = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        Vector3 upwardDirection = Vector3.up * Mathf.Sin(hitAngle * Mathf.Deg2Rad);
        Vector3 hitDirection = (forwardDirection + upwardDirection).normalized;

        rb.AddForce(hitDirection * hitForce, ForceMode.Impulse);
       
    }
}

