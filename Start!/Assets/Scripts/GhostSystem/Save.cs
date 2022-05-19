using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    private FileStream fs;
    private StreamWriter sw;
    private Config config;
    private Load load;
    private Lap lap;
    private bool running = false;
    void Start()
    {
        load = GameObject.Find("Ghost").GetComponent<Load>();
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        lap = GameObject.Find("Player").GetComponent<Lap>();
        DeleteFile("tmp.ghost");
        fs = new FileStream(Application.dataPath + "/" + "tmp.ghost", FileMode.OpenOrCreate, FileAccess.Write);
        sw = new StreamWriter(fs);
        running = true;
    }
    void FixedUpdate()
    {
        if(running)
        {
            if(lap.lap == -1)
            {
                running = false;
                if(load.running)
                    config.message = "You were faster than the ghost!";
                else
                    config.message = "You were slower than the ghost!";
                fixAndCopyFiles();
                SceneManager.LoadScene("Finish");
            }
            else
                copy();
        }
    }
    void copy()
    {
        sw.WriteLine(gameObject.transform.position.x + "/" + gameObject.transform.position.y + "/" + gameObject.transform.position.z + "/" + gameObject.transform.rotation.x + "/" + gameObject.transform.rotation.y + "/" + gameObject.transform.rotation.z + "/" + gameObject.transform.rotation.w);
    }
    void DeleteFile(string input)
    {
        string filePath = Application.dataPath + "/" + input;

        if (!File.Exists(filePath))
        {
            Debug.Log("no " + filePath + " file exists");
        }
        else
        {
            Debug.Log(filePath + " file exists, deleting...");

            File.Delete(filePath);
        }
    }
    void CopyFile()
    {
        string filePath = Application.dataPath + "/" + "tmp.ghost";
        string filePath2 = Application.dataPath + "/" + "saved" + config.mapSelector.ToString() + config.carSelector.ToString() + ".ghost";
        if (!File.Exists(filePath))
        {
            Debug.Log("no " + filePath + " file exists");
        }
        else
        {
            Debug.Log(filePath + " file exists, copying...");

            File.Copy(filePath, filePath2);
        }
    }
    void fixAndCopyFiles()
    {
        try
        {
            load.reader.Close();
        }
        catch
        {
            Debug.Log("no file to close");
        }
        load.running = false;
        sw.Flush();
        sw.Close();
        fs.Close();
        DeleteFile("saved" + config.mapSelector.ToString() + config.carSelector.ToString() + ".ghost");
        CopyFile();
        DeleteFile("tmp.ghost");
    }
}
