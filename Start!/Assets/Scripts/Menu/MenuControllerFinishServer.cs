using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerFinishServer : MonoBehaviour
{
    public Button okBtn;
    private Config config;
    private Server server;
    private Client client;
    private bool isClient = false;
    private bool isServer = false;
    public GameObject[] Data;
    public GameObject[] Name;
    public GameObject[] Map;
    void Start()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        okBtn.onClick.RemoveAllListeners();
        server = GameObject.Find("Server").GetComponent<Server>();
        if (server.connected)
            isServer = true;
        client = GameObject.Find("Client").GetComponent<Client>();
        if (client.connected)
            isClient = true;
        okBtn.onClick.AddListener(() =>
        {
            Debug.Log("ok");
            Destroy(GameObject.Find("VideoMainCamera"));
            Destroy(GameObject.Find("ConfigStart"));
            Destroy(GameObject.Find("Video"));
            Destroy(GameObject.Find("VServer"));
            Destroy(GameObject.Find("VClient"));
            Destroy(GameObject.Find("Server"));
            Destroy(GameObject.Find("Client"));
            Destroy(GameObject.Find("Player1Voice"));
            Destroy(GameObject.Find("Player2Voice"));
            Destroy(GameObject.Find("Player3Voice"));
            Destroy(GameObject.Find("Player4Voice"));
            SceneManager.LoadScene("Bootstrap");
        });
        for (int i = 0; i < 4; i++)
            Data[i].SetActive(false);
    }
    void FixedUpdate()
    {
        if (isServer)
        {
            foreach (KeyValuePair<string, string> entry in server.scoreboard)
            {
                Data[int.Parse(entry.Value)-1].SetActive(true);
                Name[int.Parse(entry.Value)-1].GetComponent<Text>().text = "Player "+entry.Key;
                if (config.mapSelector == 1)
                    Map[int.Parse(entry.Value)-1].GetComponent<Text>().text = "Map1";
                else
                    Map[int.Parse(entry.Value)-1].GetComponent<Text>().text = "Map2";
            }
        }
        else if (isClient)
        {
            foreach (KeyValuePair<string, string> entry in client.scoreboard)
            {
                Data[int.Parse(entry.Value)-1].SetActive(true);
                Name[int.Parse(entry.Value)-1].GetComponent<Text>().text = "Player "+entry.Key;
                if (config.mapSelector == 1)
                    Map[int.Parse(entry.Value)-1].GetComponent<Text>().text = "Map1";
                else
                    Map[int.Parse(entry.Value)-1].GetComponent<Text>().text = "Map2";
            }
        }
    }
}
