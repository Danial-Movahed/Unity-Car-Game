using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UsePowerUp : MonoBehaviour
{
    public int currentPowerUp = 0;
    public GameObject PowerUpImage;
    private Config configscript;
    private Client client;
    private Server server;
    private bool mode;
    void Start()
    {
        configscript = GameObject.Find("ConfigStart").GetComponent<Config>();
        client = GameObject.Find("Client").GetComponent<Client>();
        server = GameObject.Find("Server").GetComponent<Server>();
        if (server.isStarted)
        {
            mode = true;
        }
        else
        {
            mode = false;
        }
        PowerUpImage.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKey("e"))
        {
            switch (currentPowerUp)
            {
                case 1:
                    Debug.Log("Banana!");
                    if (mode)
                    {
                        server.sendData("Banana " + (GameObject.Find(configscript.selfName).transform.position.x).ToString() + " " + (GameObject.Find(configscript.selfName).transform.position.y+1).ToString() + " " + (GameObject.Find(configscript.selfName).transform.position.z).ToString() + " " + (configscript.selfName).ToString());
                    }
                    else
                    {
                        client.sendData("Banana " + (GameObject.Find(configscript.selfName).transform.position.x).ToString() + " " + (GameObject.Find(configscript.selfName).transform.position.y+1).ToString() + " " + (GameObject.Find(configscript.selfName).transform.position.z).ToString() + " " + (configscript.selfName).ToString());
                    }
                    GameObject tmp = Instantiate(Resources.Load("Maps/PowerUps/Prefabs/1"), new Vector3(GameObject.Find(configscript.selfName).transform.position.x, GameObject.Find(configscript.selfName).transform.position.y+1, GameObject.Find(configscript.selfName).transform.position.z), Quaternion.Euler(-90,0,0)) as GameObject;
                    tmp.GetComponent<NameSaver>().ExcludeName = configscript.selfName;
                    tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                    break;
            }
            currentPowerUp = 0;
            PowerUpImage.SetActive(false);
        }
    }
}