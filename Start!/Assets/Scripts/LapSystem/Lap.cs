using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lap : MonoBehaviour
{
    public int lap = 0;
    private Config config;
    void Start()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "StartLapCounter")
        {
            if (lap < 3)
            {
                lap++;
                GameObject.Find("lapCount").GetComponent<Text>().text = lap.ToString();
                Debug.Log(lap);
            }
            else
            {
                Debug.Log("Done game");
                lap = -1;
                if (config.modeSelector == 0 && config.selfName == "")
                    SceneManager.LoadScene("Finish");
            }
        }
    }
}
