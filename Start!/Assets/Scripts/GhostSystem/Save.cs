using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour
{
    public GameObject car;
    private FileStream fs;
    private StreamWriter sw;
    void Start()
    {
        fs = new FileStream(Application.dataPath + "/" + "tmp.ghost", FileMode.OpenOrCreate, FileAccess.Write);
        sw = new StreamWriter(fs);
        InvokeRepeating("copy", 0, 0.05f);
    }
    void copy()
    {
        sw.WriteLine(car.transform.position.x + "/" + car.transform.position.y + "/" + car.transform.position.z + "/" + car.transform.rotation.x + "/" + car.transform.rotation.y + "/" + car.transform.rotation.z + "/" + car.transform.rotation.w);
    }
    void DeleteFile(string input)
    {
        string filePath = Application.dataPath + "/" + input;

        // check if file exists
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
        string filePath2 = Application.dataPath + "/" + "saved.ghost";

        // check if file exists
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
    void OnDisable()
    {
        sw.Flush();
        sw.Close();
        fs.Close();
        DeleteFile("saved.ghost");
        CopyFile();
        DeleteFile("tmp.ghost");
    }
}
