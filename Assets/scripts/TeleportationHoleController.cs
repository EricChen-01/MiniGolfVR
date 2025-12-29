using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHoleController : MonoBehaviour
{
    public Transform teleportaionHole;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            other.gameObject.transform.position = teleportaionHole.position;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
