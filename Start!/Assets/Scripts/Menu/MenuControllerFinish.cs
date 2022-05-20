using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerFinish : MonoBehaviour
{
    public Button okBtn;
    public Text statusText;
    private Config config;
    private Server server;
    private Client client;
    private bool isClient = false;
    private bool isServer = false;
    void Start()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        okBtn.onClick.RemoveAllListeners();
        server = GameObject.Find("Server").GetComponent<Server>();
        if(server.connected)
            isServer = true;
        client = GameObject.Find("Client").GetComponent<Client>();
        if(client.connected)
            isClient = true;
        if (config.selfName != "")
        {
            statusText.fontSize = 20;
        }
        statusText.text = "Finished\n" + config.message;
        okBtn.onClick.AddListener(() =>
        {
            Debug.Log("ok");
            Destroy(GameObject.Find("VideoMainCamera"));
            Destroy(GameObject.Find("ConfigStart"));
            Destroy(GameObject.Find("Config"));
            Destroy(GameObject.Find("Video"));
            if (isServer)
            {
                server.Key = "SomeConnectionKey";
                server.connected = false;
                server.isStarted = false;
                server.server.Stop();
            }
            if (isClient)
            {
                client.isStarted = false;
                client.connected = false;
                client.client.Stop();
            }
            SceneManager.LoadScene("Bootstrap");
        });
    }
    void FixedUpdate()
    {
        if (isServer)
        {
            statusText.text = "Finished\n";
            foreach (KeyValuePair<string, string> entry in server.scoreboard)
            {
                statusText.text += "Player #" + entry.Key + " is " + entry.Value + "\n";
            }
        }
        else if (isClient)
        {
            statusText.text = "Finished\n";
            foreach (KeyValuePair<string, string> entry in client.scoreboard)
            {
                statusText.text += "Player #" + entry.Key + " is " + entry.Value + "\n";
            }
        }
    }
}
