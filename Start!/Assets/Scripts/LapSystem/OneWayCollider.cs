using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCollider : MonoBehaviour
{
    void FixedUpdate()
    {
        if(ifStillColliding)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    private bool ifStillColliding = false;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision!");
        if(col.gameObject.name == "StartLapCollider" && GetComponent<Rigidbody>().velocity.z < 0)
        {
            ifStillColliding = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log("No Collision!");
        if(col.gameObject.name == "StartLapCollider")
        {
            ifStillColliding = false;
        }   
    }
}
