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
            if (GameObject.Find("Server"))
            {
                GameObject.Find("Server").GetComponent<Server>().isStarted = false;
                GameObject.Find("Server").GetComponent<Server>().server.Stop();
            }
            if (GameObject.Find("Client"))
            {
                GameObject.Find("Client").GetComponent<Client>().isStarted = false;
                GameObject.Find("Client").GetComponent<Client>().client.Stop();
            }
            Destroy(GameObject.Find("VideoMainCamera"));
            Destroy(GameObject.Find("ConfigStart"));
            Destroy(GameObject.Find("Config"));
            Destroy(GameObject.Find("Video"));
            Destroy(GameObject.Find("Server"));
            Destroy(GameObject.Find("Client"));
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
