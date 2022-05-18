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
    private NetManager client = new NetManager(listener);
    public string ip = "localhost";
    public int port = 3344;
    private NetPacketProcessor netProcessor;
    public string selfName = "";
    public string mapName = "";
    public bool isStarted = false;
    private bool connected = false;
    private Config config;
    private int totalConnected = 0;
    public void connect()
    {
        client.Start();
        client.Connect(ip, port, "SomeConnectionKey");
        connected = true;
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            if (isStarted)
            {
                string[] tmp = dataReader.GetString(400).Split(' ');
                GameObject.Find(tmp[0]).transform.position = new Vector3(float.Parse(tmp[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[2], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[3], CultureInfo.InvariantCulture.NumberFormat));
                GameObject.Find(tmp[0]).transform.localEulerAngles = new Vector3(float.Parse(tmp[4], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[5], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[6], CultureInfo.InvariantCulture.NumberFormat));
                dataReader.Recycle();
            }
            else
            {
                if (selfName == "")
                {
                    selfName = dataReader.GetString(400);
                    if (selfName != "")
                    {
                        GameObject.Find("StatusText").GetComponent<Text>().text = "We are Player" + selfName;
                        Debug.Log(selfName);
                        config.selfName = selfName;
                    }
                    dataReader.Recycle();
                }
                else if (totalConnected == 0)
                {
                    totalConnected = int.Parse(dataReader.GetString(400));
                    Debug.Log(totalConnected);
                    dataReader.Recycle();
                }
                else if (mapName == "")
                {
                    mapName = dataReader.GetString(400);
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
                    dataReader.Recycle();
                }
                else
                {
                    string tmp2 = dataReader.GetString(400);
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
                            dataReader.Recycle();
                        }
                        else
                        {
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
                            else
                            {
                                Debug.Log(tmp2);
                                string[] tmp = tmp2.Split(' ');
                                config.playerCars[int.Parse(tmp[0])] = int.Parse(tmp[1]);
                                dataReader.Recycle();
                                knownCarNum++;
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
        };
    }
    void Update()
    {
        client.PollEvents();
    }
    void LateUpdate()
    {
        if (isStarted)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put(selfName + " " + GameObject.Find(selfName).transform.position.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.z.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " +  GameObject.Find(selfName).transform.localEulerAngles.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.z.ToString(CultureInfo.InvariantCulture.NumberFormat));
            client.SendToAll(writer, DeliveryMethod.Sequenced);
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