using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap : MonoBehaviour
{
    public int lap = 0;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "StartLapCounter")
        {
            if(lap < 3)
            {
                lap++;
                Debug.Log(lap);
            }
            else
            {
               Debug.Log("Done game");
            }
        }
    }
}
