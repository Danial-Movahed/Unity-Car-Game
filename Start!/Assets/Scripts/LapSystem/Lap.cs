using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap : MonoBehaviour
{
    public GameObject self;
    public int lap = 0;
    void OnTriggerEnter(Collider col)
    {
        Rigidbody rb = self.GetComponent<Rigidbody>();
        if(rb.velocity.z > 0 && lap < 3)
        {
            lap++;
            Debug.Log(lap);
        }
    }
}
