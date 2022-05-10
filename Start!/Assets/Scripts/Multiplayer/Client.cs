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
    public bool isSend = false;
    private bool connected = false;
    private Config config;
    public int[] playerCars = new int[4];
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
                GameObject.Find(tmp[0]).GetComponent<Rigidbody>().velocity = new Vector3(float.Parse(tmp[4], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[5], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[6], CultureInfo.InvariantCulture.NumberFormat));
                GameObject.Find(tmp[0]).GetComponent<Rigidbody>().angularVelocity = new Vector3(float.Parse(tmp[7], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[8], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[9], CultureInfo.InvariantCulture.NumberFormat));
                GameObject.Find(tmp[0]).transform.localEulerAngles = new Vector3(float.Parse(tmp[10], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[11], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[12], CultureInfo.InvariantCulture.NumberFormat));
                dataReader.Recycle();
            }
            else if (selfName == "")
            {
                selfName = dataReader.GetString(400);
                if (selfName != "")
                {
                    GameObject.Find("StatusText").GetComponent<Text>().text = "We are Player" + selfName;
                    Debug.Log(selfName);
                }
                dataReader.Recycle();
            }
            else if (totalConnected == 0)
            {
                totalConnected = int.Parse(dataReader.GetString(400));
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
                int knownCarNum = 0;
                for (int i = 0; i < totalConnected; i++)
                {
                    if (playerCars[i] != 0)
                    {
                        knownCarNum++;
                    }
                }
                if (knownCarNum == totalConnected)
                {
                    Debug.Log("Yay!");
                }
                else
                {
                    string[] tmp = dataReader.GetString(400).Split(' ');
                    playerCars[int.Parse(tmp[0])] = int.Parse(tmp[1]);
                    dataReader.Recycle();
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
        if (isSend)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put(selfName + " " + GameObject.Find(selfName).transform.position.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.z.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).GetComponent<Rigidbody>().velocity.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).GetComponent<Rigidbody>().velocity.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).GetComponent<Rigidbody>().velocity.z.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity.z.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.z.ToString(CultureInfo.InvariantCulture.NumberFormat));
            client.SendToAll(writer, DeliveryMethod.ReliableOrdered);
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
