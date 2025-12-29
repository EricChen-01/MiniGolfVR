using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveRigidBodyOnStart : MonoBehaviour
{

    public void Update()
    {
        if (GameStateManager.CurrentGameMode() == GameMode.Game)
        {
            ApplyRigidbody(gameObject);
        }
    }
    public void ApplyRigidbody(GameObject obj)
    {
        if (obj != null)
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.useGravity = true;
            Debug.Log("Rigidbody added to the object!");
        }
    }
}
