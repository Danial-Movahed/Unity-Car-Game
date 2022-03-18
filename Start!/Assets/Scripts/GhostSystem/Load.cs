using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load : MonoBehaviour
{
    public GameObject car;
    private StreamReader reader;
    private string line;
    
    // Start is called before the first frame update
    void Start()
    {
        reader = new StreamReader(Application.dataPath + "/" + "saved.ghost");
        InvokeRepeating("paste", 0, 0.05f);
    }

    void paste()
    {
        line = reader.ReadLine();
        if (line != null)
        {
            string[] values = line.Split('/');
            Debug.Log(float.Parse(values[0]));
            Debug.Log(float.Parse(values[1]));
            Debug.Log(float.Parse(values[2]));
            car.transform.position = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            car.transform.rotation = new Quaternion(float.Parse(values[3]), float.Parse(values[4]), float.Parse(values[5]), float.Parse(values[6]));
        }
    }
}
