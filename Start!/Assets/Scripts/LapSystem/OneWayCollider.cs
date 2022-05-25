using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCollider : MonoBehaviour
{
    private Config config;
    void Start()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
    }
    void Update()
    {
        if (ifStillColliding)
        {
            if (config.mapSelector == 2)
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, Mathf.Abs(GetComponent<Rigidbody>().velocity.z) + 0.5f);
            else
                GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Abs(GetComponent<Rigidbody>().velocity.x)+0.5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }

    }
    private bool ifStillColliding = false;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision!");
        if (config.mapSelector == 2)
        {
            if (col.gameObject.name == "StartLapCollider" && GetComponent<Rigidbody>().velocity.z < 0)
            {
                ifStillColliding = true;
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (col.gameObject.name == "StartLapCollider" && GetComponent<Rigidbody>().velocity.x < 0)
            {
                ifStillColliding = true;
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log("No Collision!");
        if (col.gameObject.name == "StartLapCollider")
        {
            ifStillColliding = false;
        }
    }
}
