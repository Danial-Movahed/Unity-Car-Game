using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLib.Layers;
using System.Globalization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Client : MonoBehaviour
{
    private static EventBasedNetListener listener = new EventBasedNetListener();
    public NetManager client = new NetManager(listener);
    public string ip = "localhost";
    public int port = 3344;
    private NetPacketProcessor netProcessor;
    public string selfName = "";
    public string mapName = "";
    public bool isStarted = false;
    public bool connected = false;
    public bool finished = false;
    private Config config;
    public int totalConnected = 0;
    public Dictionary<string, string> scoreboard = new Dictionary<string, string>();
    public int num = 1;
    IEnumerator ShowAndHideSeconds(int seconds, GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(seconds);
        go.SetActive(false);
    }
    public void connect()
    {
        listener = new EventBasedNetListener();
        client = new NetManager(listener);
        selfName = "";
        mapName = "";
        isStarted = false;
        connected = false;
        finished = false;
        totalConnected = 0;
        num = 1;
        scoreboard.Clear();
        client.Start();
        client.Connect(ip, port, "SomeConnectionKey");
        connected = true;
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        listener.PeerDisconnectedEvent += (peer, dcInfo) =>
        {
            Debug.Log("Disconnected");
            connected = false;
            isStarted = false;
            SceneManager.LoadScene("JoinServer");
        };
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            string data = dataReader.GetString(400);
            if (data.Contains("DC"))
            {
                string[] dataSplit = data.Split(' ');
                Destroy(GameObject.Find(dataSplit[1]));
                totalConnected--;
            }
            else if (data.Contains("FN"))
            {
                Debug.Log(data);
                num++;
                string[] dataSplit = data.Split(' ');
                scoreboard.Add(dataSplit[1], dataSplit[2]);
            }
            else if (data.Contains("RM"))
            {
                Debug.Log("Remove Map");
                config.mapSelector = 0;
                mapName = "";
                SceneManager.LoadScene("ClientWaiting");
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
                StartCoroutine(ShowAndHideSeconds(10, GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().DirtyImage));
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

                    }
                    else
                    {
                        if (selfName == "")
                        {
                            selfName = data;
                            if (selfName != "")
                            {
                                GameObject.Find("StatusText").GetComponent<Text>().text = "We are Player" + selfName;
                                Debug.Log(selfName);
                                config.selfName = selfName;
                            }

                        }
                        else if (totalConnected == 0)
                        {
                            totalConnected = int.Parse(data);
                            Debug.Log(totalConnected);

                        }
                        else if (mapName == "")
                        {
                            mapName = data;
                            if (mapName != "")
                            {
                                GameObject.Find("StatusText").GetComponent<Text>().text = "Map is " + mapName;
                                Debug.Log(mapName);
                                if (mapName == "map1")
                                {
                                    config.mapSelector = 1;
                                }
                                else if (mapName == "map2")
                                {
                                    config.mapSelector = 2;
                                }
                                SceneManager.LoadScene("ClientCarSelector");
                            }

                        }
                        else
                        {
                            string tmp2 = data;
                            if (tmp2 != null)
                            {
                                if (tmp2 == "start")
                                {
                                    isStarted = true;
                                    if (mapName == "map1")
                                    {
                                        config.mapSelector = 1;
                                    }
                                    else if (mapName == "map2")
                                    {
                                        config.mapSelector = 2;
                                    }
                                    config.startGame();

                                }
                                else
                                {
                                    string[] tmp = tmp2.Split(' ');
                                    config.playerCars[int.Parse(tmp[0])] = int.Parse(tmp[1]);
                                    int knownCarNum = 0;
                                    for (int i = 0; i < totalConnected; i++)
                                    {
                                        if (config.playerCars[i] != 0)
                                        {
                                            knownCarNum++;
                                        }
                                    }
                                    if (knownCarNum == totalConnected)
                                    {
                                        Debug.Log("Yay!");
                                        isStarted = true;
                                        if (mapName == "map1")
                                        {
                                            config.mapSelector = 1;
                                        }
                                        else if (mapName == "map2")
                                        {
                                            config.mapSelector = 2;
                                        }
                                        config.startGame();
                                        sendData("start");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            dataReader.Recycle();
        };
    }
    void Update()
    {
        if (connected)
            client.PollEvents();
    }
    void LateUpdate()
    {
        if (!finished)
        {
            if (isStarted)
            {
                if (GameObject.Find(selfName).GetComponent<Lap>().lap == -1)
                {
                    isStarted = false;
                    finished = true;
                    totalConnected = -1;
                    sendData("FN " + selfName + " " + num);
                    sendData("DL " + selfName);
                    scoreboard.Add(selfName, num.ToString());
                    SceneManager.LoadScene("FinishServer");
                }
                else
                {
                    NetDataWriter writer = new NetDataWriter();
                    writer.Put(selfName + " " + GameObject.Find(selfName).transform.position.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.z.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.z.ToString(CultureInfo.InvariantCulture.NumberFormat));
                    client.SendToAll(writer, DeliveryMethod.Sequenced);
                }
            }
        }
    }
    private void OnApplicationQuit()
    {
        if (connected)
            client.Stop();
    }
    public void sendData(string data)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(data);
        client.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
}