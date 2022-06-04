using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UsePowerUp : MonoBehaviour
{
    public int currentPowerUp = 0;
    public int lastPowerUp = 0;
    public GameObject PowerUpImage;
    public GameObject DirtyImage;
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
    IEnumerator Turbo(int seconds)
    {
        GameObject.Find(configscript.selfName).GetComponent<VehicleControl>().carSetting.stiffness += 10;
        GameObject.Find(configscript.selfName).GetComponent<VehicleControl>().carSetting.LimitForwardSpeed += 1000;
        yield return new WaitForSeconds(seconds);
        GameObject.Find(configscript.selfName).GetComponent<VehicleControl>().carSetting.stiffness -= 10;
        GameObject.Find(configscript.selfName).GetComponent<VehicleControl>().carSetting.LimitForwardSpeed -= 1000;
    }
    IEnumerator Sheild(int seconds)
    {
        PowerUpImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Maps/PowerUps/PowerUps/SheildUp");
        currentPowerUp = -1;
        yield return new WaitForSeconds(seconds);
        PowerUpImage.SetActive(false);
        lastPowerUp = 4;
        currentPowerUp = 0;
    }
    IEnumerator Big(int seconds)
    {
        GameObject.Find(configscript.selfName).GetComponent<Transform>().localScale = new Vector3(GameObject.Find(configscript.selfName).GetComponent<Transform>().localScale.x * 1.5f, GameObject.Find(configscript.selfName).GetComponent<Transform>().localScale.y * 1.5f, GameObject.Find(configscript.selfName).GetComponent<Transform>().localScale.z * 1.5f);
        GameObject.Find(configscript.selfName).GetComponent<VehicleControl>().carSetting.stiffness += 15;
        if (mode)
        {
            server.sendData("Big " + configscript.selfName);
        }
        else
        {
            client.sendData("Big " + configscript.selfName);
        }
        yield return new WaitForSeconds(seconds);
        GameObject.Find(configscript.selfName).GetComponent<VehicleControl>().carSetting.stiffness -= 15;
        GameObject.Find(configscript.selfName).GetComponent<Transform>().localScale = new Vector3(GameObject.Find(configscript.selfName).GetComponent<Transform>().localScale.x / 1.5f, GameObject.Find(configscript.selfName).GetComponent<Transform>().localScale.y / 1.5f, GameObject.Find(configscript.selfName).GetComponent<Transform>().localScale.z / 1.5f);
        if (mode)
        {
            server.sendData("Unbig " + configscript.selfName);
        }
        else
        {
            client.sendData("Unbig " + configscript.selfName);
        }
    }
    IEnumerator AllStar(int seconds)
    {
        GameObject.Find(configscript.selfName).AddComponent<AllStar>();
        GameObject.Find(configscript.selfName).GetComponent<VehicleControl>().carSetting.stiffness += 15;
        for (int i = 0; i < 4; i++)
        {
            if (configscript.playerCars[i] != 0 && (i + 1).ToString() != configscript.selfName)
            {
                GameObject.Find((i + 1).ToString()).transform.Find("Collision").GetComponent<MeshCollider>().isTrigger = true;
            }
        }
        if(mode)
        {
            server.sendData("AllStar " + configscript.selfName);
        }
        else
        {
            client.sendData("AllStar " + configscript.selfName);
        }
        yield return new WaitForSeconds(seconds);
        Destroy(GameObject.Find(configscript.selfName).GetComponent<AllStar>());
        GameObject.Find(configscript.selfName).GetComponent<VehicleControl>().carSetting.stiffness -= 15;
        for (int i = 0; i < 4; i++)
        {
            if (configscript.playerCars[i] != 0 && (i + 1).ToString() != configscript.selfName)
            {
                GameObject.Find((i + 1).ToString()).transform.Find("Collision").GetComponent<MeshCollider>().isTrigger = false;
            }
        }
        if (mode)
        {
            server.sendData("UnallStar " + configscript.selfName);
        }
        else
        {
            client.sendData("UnallStar " + configscript.selfName);
        }
        GameObject tmp = null;
        if (configscript.mapSelector == 2)
        {
            tmp = configscript.cars2[configscript.carSelector - 1];
        }
        else
        {
            tmp = configscript.cars1[configscript.carSelector - 1];
        }
        for (int i = 0; i < GameObject.Find(configscript.selfName).GetComponentsInChildren<Renderer>().Length; i++)
        {
            GameObject.Find(configscript.selfName).GetComponentsInChildren<Renderer>()[i].material.color = tmp.GetComponentsInChildren<Renderer>()[i].sharedMaterial.color;
        }
    }
    void Update()
    {
        if (Input.GetKey("e"))
        {
            switch (currentPowerUp)
            {
                case 1:
                    Debug.Log("Banana!");
                    lastPowerUp = currentPowerUp;
                    if (mode)
                    {
                        server.sendData("Banana " + (GameObject.Find(configscript.selfName).transform.position.x).ToString() + " " + (GameObject.Find(configscript.selfName).transform.position.y + 1).ToString() + " " + (GameObject.Find(configscript.selfName).transform.position.z).ToString() + " " + (configscript.selfName).ToString());
                    }
                    else
                    {
                        client.sendData("Banana " + (GameObject.Find(configscript.selfName).transform.position.x).ToString() + " " + (GameObject.Find(configscript.selfName).transform.position.y + 1).ToString() + " " + (GameObject.Find(configscript.selfName).transform.position.z).ToString() + " " + (configscript.selfName).ToString());
                    }
                    GameObject tmp = Instantiate(Resources.Load("Maps/PowerUps/Prefabs/1"), new Vector3(GameObject.Find(configscript.selfName).transform.position.x, GameObject.Find(configscript.selfName).transform.position.y + 1, GameObject.Find(configscript.selfName).transform.position.z), Quaternion.Euler(-90, 0, 0)) as GameObject;
                    tmp.GetComponent<NameSaver>().ExcludeName = configscript.selfName;
                    tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                    break;
                case 2:
                    Debug.Log("Turbo!");
                    lastPowerUp = currentPowerUp;
                    StartCoroutine(Turbo(5));
                    break;
                case 3:
                    Debug.Log("Super turbo!");
                    lastPowerUp = currentPowerUp;
                    StartCoroutine(Turbo(20));
                    break;
                case 4:
                    Debug.Log("Sheild!");
                    lastPowerUp = currentPowerUp;
                    StartCoroutine(Sheild(20));
                    break;
                case 5:
                    Debug.Log("Time to make the screen dirty!");
                    lastPowerUp = currentPowerUp;
                    if (mode)
                    {
                        server.sendData("Dirty");
                    }
                    else
                    {
                        client.sendData("Dirty");
                    }
                    break;
                case 6:
                    Debug.Log("BIG!");
                    lastPowerUp = currentPowerUp;
                    StartCoroutine(Big(20));
                    break;
                case 7:
                    Debug.Log("All star!");
                    lastPowerUp = currentPowerUp;
                    StartCoroutine(AllStar(20));
                    break;
                case 8:
                    Debug.Log("Smoll!");
                    lastPowerUp = currentPowerUp;
                    if (mode)
                    {
                        server.sendData("Small");
                    }
                    else
                    {
                        client.sendData("Small");
                    }
                    break;
                case 9:
                    // Pow block
                    Debug.Log("Pow block!");
                    lastPowerUp = currentPowerUp;
                    if (mode)
                    {
                        server.sendData("PowBlock");
                    }
                    else
                    {
                        client.sendData("PowBlock");
                    }
                    break;
            }
            if (currentPowerUp != -1)
            {
                currentPowerUp = 0;
                PowerUpImage.SetActive(false);
            }
        }
    }
}