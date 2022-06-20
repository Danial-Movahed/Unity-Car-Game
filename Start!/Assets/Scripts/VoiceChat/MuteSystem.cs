using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuteSystem : MonoBehaviour
{
    private bool isMuted = false;
    private bool isMutedAll = false;
    private bool isServer;
    public string[] allowedscenes;
    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        isServer = SceneManager.GetActiveScene().name == "ServerWaiting" ? true : false;
        Debug.Log(isServer);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            isMutedAll = !isMutedAll;
            if (isMutedAll)
            {
                if (isServer)
                {
                    GameObject.Find("VServer").GetComponent<VServer>().audioSource[0].volume = 0;
                    GameObject.Find("VServer").GetComponent<VServer>().audioSource[1].volume = 0;
                    GameObject.Find("VServer").GetComponent<VServer>().audioSource[2].volume = 0;
                    GameObject.Find("VServer").GetComponent<VServer>().audioSource[3].volume = 0;
                }
                else
                {
                    GameObject.Find("VClient").GetComponent<VClient>().audioSource[0].volume = 0;
                    GameObject.Find("VClient").GetComponent<VClient>().audioSource[1].volume = 0;
                    GameObject.Find("VClient").GetComponent<VClient>().audioSource[2].volume = 0;
                    GameObject.Find("VClient").GetComponent<VClient>().audioSource[3].volume = 0;
                }
            }
            else
            {
                if (isServer)
                {
                    GameObject.Find("VServer").GetComponent<VServer>().audioSource[0].volume = 1;
                    GameObject.Find("VServer").GetComponent<VServer>().audioSource[1].volume = 1;
                    GameObject.Find("VServer").GetComponent<VServer>().audioSource[2].volume = 1;
                    GameObject.Find("VServer").GetComponent<VServer>().audioSource[3].volume = 1;
                }
                else
                {
                    GameObject.Find("VClient").GetComponent<VClient>().audioSource[0].volume = 1;
                    GameObject.Find("VClient").GetComponent<VClient>().audioSource[1].volume = 1;
                    GameObject.Find("VClient").GetComponent<VClient>().audioSource[2].volume = 1;
                    GameObject.Find("VClient").GetComponent<VClient>().audioSource[3].volume = 1;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            isMuted = !isMuted;
            if (isMuted)
            {
                if (isServer)
                {
                    GameObject.Find("VServer").GetComponent<VServer>().isStreaming = false;
                }
                else
                {
                    GameObject.Find("VClient").GetComponent<VClient>().isStreaming = false;
                }
            }
            else
            {
                if (isServer)
                {
                    GameObject.Find("VServer").GetComponent<VServer>().isStreaming = true;
                }
                else
                {
                    GameObject.Find("VClient").GetComponent<VClient>().isStreaming = true;
                }
            }
        }
    }
    public void ChangedActiveScene(Scene current, Scene next)
    {
        int c = 0;
        for (int i = 0; i < allowedscenes.Length; i++)
        {
            if (next.name == allowedscenes[i])
            {
                c++;
            }
        }
        if (c == 0)
        {
            Destroy(gameObject);
        }
    }
}
