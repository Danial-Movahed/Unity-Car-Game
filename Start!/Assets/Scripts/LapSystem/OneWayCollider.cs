using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCollider : MonoBehaviour
{
    void Update()
    {
        if(ifStillColliding)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, Mathf.Abs(GetComponent<Rigidbody>().velocity.z)+0.5f);
        }
    }
    private bool ifStillColliding = false;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision!");
        if(col.gameObject.name == "StartLapCollider" && GetComponent<Rigidbody>().velocity.z < 0)
        {
            ifStillColliding = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
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
