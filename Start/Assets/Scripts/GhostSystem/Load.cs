using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load : MonoBehaviour
{
    public StreamReader reader;
    private string line;
    private Config config;
    public bool running = false;
    void Start()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        reader = new StreamReader(Application.dataPath + "/" + "saved" + config.mapSelector.ToString() + config.carSelector.ToString() + ".ghost");
        running = true;
    }
    void FixedUpdate()
    {
        if(running)
            paste();
    }
    void paste()
    {
        line = reader.ReadLine();
        if (line != null)
        {
            string[] values = line.Split('/');
            gameObject.transform.position = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            gameObject.transform.rotation = new Quaternion(float.Parse(values[3]), float.Parse(values[4]), float.Parse(values[5]), float.Parse(values[6]));
        }
        else
        {
            running = false;
            reader.Close();
        }
    }
    
}
