using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                GameObject.Find("LapCount").GetComponent<Text>().text = lap.ToString();
                Debug.Log(lap);
            }
            else
            {
               Debug.Log("Done game");
            }
        }
    }
}
