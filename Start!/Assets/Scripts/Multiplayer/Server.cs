using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLib.Layers;
using System.Globalization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour
{
    private static EventBasedNetListener listener = new EventBasedNetListener();
    public NetManager server = new NetManager(listener);
    public int connectionLimit = 3;
    public int port = 3344;
    public string selfName = "1";
    public int connectedNow = 1;
    public bool isStarted = false;
    public bool connected = false;
    public bool finished = false;
    public string Key = "SomeConnectionKey";
    private Config config;
    private Dictionary<string, string> peerNames = new Dictionary<string, string>();
    public Dictionary<string, string> scoreboard = new Dictionary<string, string>();
    public int num = 1;
    IEnumerator ShowAndHideSeconds(int seconds, GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(seconds);
        go.SetActive(false);
    }
    IEnumerator Small(int seconds)
    {
        float tmp = GameObject.Find(selfName).GetComponent<VehicleControl>().carSetting.stiffness;
        GameObject.Find(config.selfName).GetComponent<Transform>().localScale = new Vector3(GameObject.Find(config.selfName).GetComponent<Transform>().localScale.x / 2f, GameObject.Find(config.selfName).GetComponent<Transform>().localScale.y / 2f, GameObject.Find(config.selfName).GetComponent<Transform>().localScale.z / 2f);
        GameObject.Find(config.selfName).GetComponent<VehicleControl>().carSetting.stiffness = 1;
        sendData("scaleDown " + config.selfName);
        yield return new WaitForSeconds(seconds);
        GameObject.Find(config.selfName).GetComponent<VehicleControl>().carSetting.stiffness = tmp;
        GameObject.Find(config.selfName).GetComponent<Transform>().localScale = new Vector3(GameObject.Find(config.selfName).GetComponent<Transform>().localScale.x * 2f, GameObject.Find(config.selfName).GetComponent<Transform>().localScale.y * 2f, GameObject.Find(config.selfName).GetComponent<Transform>().localScale.z * 2f);
        sendData("scaleUp " + config.selfName);
    }
    public void connect()
    {
        scoreboard.Clear();
        peerNames.Clear();
        listener = new EventBasedNetListener();
        server = new NetManager(listener);
        connectionLimit = 3;
        connectedNow = 1;
        isStarted = false;
        connected = false;
        finished = false;
        Key = "SomeConnectionKey";
        num = 1;
        connected = true;
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        for(int i=0;i<4;i++)
            config.playerCars[i] = 0;
        connectedNow = 1;
        config.selfName = selfName;
        server.Start(port);
        Debug.Log("started server");
        listener.ConnectionRequestEvent += request =>
        {
            if (server.ConnectedPeersCount < connectionLimit)
                request.AcceptIfKey(Key);
            else
                request.Reject();
        };
        listener.PeerConnectedEvent += peer =>
        {
            Debug.Log("We got connection: " + peer.EndPoint);
            connectedNow++;
            NetDataWriter writer = new NetDataWriter();
            Debug.Log(connectedNow);
            writer.Put(connectedNow.ToString());
            peerNames.Add(peer.EndPoint.ToString(), connectedNow.ToString());
            peer.Send(writer, DeliveryMethod.ReliableOrdered);
        };
        listener.PeerDisconnectedEvent += (peer, dcInfo) =>
        {
            Debug.Log("Disconnected " + peer.EndPoint);
            sendData("DC" + peerNames[peer.EndPoint.ToString()]);
            Destroy(GameObject.Find(peerNames[peer.EndPoint.ToString()]));
            connectedNow--;
        };
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            string data = dataReader.GetString(400);
            NetDataWriter writer = new NetDataWriter();
            writer.Put(data);
            server.SendToAll(writer, deliveryMethod, fromPeer);
            if (data.Contains("FN"))
            {
                Debug.Log(data);
                num++;
                string[] dataSplit = data.Split(' ');
                scoreboard.Add(dataSplit[1], dataSplit[2]);
            }
            else if (data.Contains("DL"))
            {
                Destroy(GameObject.Find(data.Split(' ')[1]));
            }
            else if (data.Contains("Banana"))
            {
                string[] dataSplit = data.Split(' ');
                GameObject tmp = Instantiate(Resources.Load("Maps/PowerUps/Prefabs/1"), new Vector3(float.Parse(dataSplit[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(dataSplit[2], CultureInfo.InvariantCulture.NumberFormat), float.Parse(dataSplit[3], CultureInfo.InvariantCulture.NumberFormat)), Quaternion.Euler(-90, 0, 0)) as GameObject;
                tmp.GetComponent<NameSaver>().ExcludeName = dataSplit[4];
                tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            }
            else if (data.Contains("Dirty"))
            {
                StartCoroutine(ShowAndHideSeconds(15, GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().DirtyImage));
            }
            else if(data.Contains("Small"))
            {
                if (GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp != -1)
                    StartCoroutine(Small(30));
                else
                {
                    GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp = 0;
                    GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().PowerUpImage.SetActive(false);
                }
            }
            else if(data.Contains("Big"))
            {
                string[] dataSplit = data.Split(' ');
                GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale = new Vector3(GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.x * 1.5f, GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.y * 1.5f, GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.z * 1.5f);
            }
            else if(data.Contains("Unbig"))
            {
                string[] dataSplit = data.Split(' ');
                GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale = new Vector3(GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.x / 1.5f, GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.y / 1.5f, GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.z / 1.5f);
            }
            else if(data.Contains("PowBlock"))
            {
                GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity = new Vector3(GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity.x, 50, GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity.z);
            }
            else if (data.Contains("AllStar"))
            {
                string[] dataSplit = data.Split(' ');
                GameObject.Find(dataSplit[1]).AddComponent<AllStar>();
            }
            else if (data.Contains("UnallStar"))
            {
                Debug.Log("UnallStar");
                string[] dataSplit = data.Split(' ');
                Debug.Log(config.playerCars[int.Parse(dataSplit[1])-1]);
                GameObject tmp = null;
                if (config.mapSelector == 2)
                {
                    tmp = config.cars2[config.playerCars[int.Parse(dataSplit[1])-1]-1];
                }
                else
                {
                    tmp = config.cars1[config.playerCars[int.Parse(dataSplit[1])-1]-1];
                }
                for (int i = 0; i < GameObject.Find(dataSplit[1]).GetComponentsInChildren<Renderer>().Length; i++)
                {
                    try
                    {
                        GameObject.Find(dataSplit[1]).GetComponentsInChildren<Renderer>()[i].material.color = tmp.GetComponentsInChildren<Renderer>()[i].sharedMaterial.color;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else if(data.Contains("scaleDown"))
            {
                string[] dataSplit = data.Split(' ');
                GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale = new Vector3(GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.x / 2, GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.y / 2, GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.z / 2);
            }
            else if(data.Contains("scaleUp"))
            {
                string[] dataSplit = data.Split(' ');
                GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale = new Vector3(GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.x * 2, GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.y * 2, GameObject.Find(dataSplit[1]).GetComponent<Transform>().localScale.z * 2);
            }
            else
            {
                if (!finished)
                {
                    if (isStarted)
                    {
                        string[] tmp = data.Split(' ');
                        GameObject.Find(tmp[0]).transform.position = new Vector3(float.Parse(tmp[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[2], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[3], CultureInfo.InvariantCulture.NumberFormat));
                        GameObject.Find(tmp[0]).transform.localEulerAngles = new Vector3(float.Parse(tmp[4], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[5], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[6], CultureInfo.InvariantCulture.NumberFormat));
                        dataReader.Recycle();
                    }
                    else
                    {
                        if (data == "start")
                        {
                            isStarted = true;
                            config.startGame();
                            dataReader.Recycle();
                        }
                        else
                        {
                            string[] tmp = data.Split(' ');
                            config.playerCars[int.Parse(tmp[0])] = int.Parse(tmp[1]);
                            int knownCarNum = 0;
                            for (int i = 0; i < connectedNow; i++)
                            {
                                if (config.playerCars[i] != 0)
                                {
                                    knownCarNum++;
                                }
                            }
                            if (knownCarNum == connectedNow)
                            {
                                Debug.Log("Yay!");
                                isStarted = true;
                                config.startGame();
                                sendData("start");
                            }
                        }
                    }
                }
            }
        };
    }
    void Update()
    {
        if (connected)
            server.PollEvents();
    }
    void LateUpdate()
    {
        if (!finished)
        {
            if (isStarted && !config.ifVideo)
            {
                if (GameObject.Find(selfName).GetComponent<Lap>().lap == -1)
                {
                    finished = true;
                    isStarted = false;
                    finished = true;
                    connectedNow = -1;
                    sendData("FN " + selfName + " " + num);
                    sendData("DL " + selfName);
                    scoreboard.Add(selfName, num.ToString());
                    SceneManager.LoadScene("FinishServer");
                }
                else
                {
                    NetDataWriter writer = new NetDataWriter();
                    writer.Put(selfName + " " + GameObject.Find(selfName).transform.position.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.z.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.z.ToString(CultureInfo.InvariantCulture.NumberFormat));
                    server.SendToAll(writer, DeliveryMethod.Sequenced);
                }
            }
        }
    }
    private void OnApplicationQuit()
    {
        if (connected)
        {
            server.Stop();
            connected = false;
        }
    }

    public void sendData(string data)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(data);
        server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
}