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
    private bool mode = false;
    void Start()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        okBtn.onClick.RemoveAllListeners();
        try
        {
            server = GameObject.Find("Server").GetComponent<Server>();
            client = GameObject.Find("Client").GetComponent<Client>();
        }
        catch
        {
            Debug.Log("hmmm");
        }
        if (server || client)
        {
            mode = true;
            if (server)
            {
                server.isStarted = false;
                server.connectedNow = -1;
                server.sendData("FN" + config.selfName + " " + server.num);
                server.scoreboard.Add(config.selfName, server.num.ToString());
            }
            else
            {
                client.isStarted = false;
                client.totalConnected = -1;
                client.sendData("FN" + config.selfName + " " + client.num);
                client.scoreboard.Add(config.selfName, client.num.ToString());
            }
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
            if(server)
            {
                server.Key = "SomeConnectionKey";
                server.connected = false;
                server.isStarted = false;
                server.server.Stop();
            }
            if(client)
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
        if (mode)
        {
            statusText.text = "Finished\n";
            foreach (KeyValuePair<string, string> entry in server.scoreboard)
            {
                statusText.text += "Player #" + entry.Key + " is" + entry.Value + "\n";
            }
        }
    }
}
