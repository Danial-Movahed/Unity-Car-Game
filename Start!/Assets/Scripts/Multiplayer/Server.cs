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
            if (data.Contains("FN"))
            {
                num++;
                string[] dataSplit = data.Split(' ');
                scoreboard.Add(dataSplit[1], dataSplit[2]);
            }
            if(!finished)
            {
                if (isStarted)
                {
                    server.SendToAll(writer, DeliveryMethod.Sequenced, fromPeer);
                    string[] tmp = data.Split(' ');
                    GameObject.Find(tmp[0]).transform.position = new Vector3(float.Parse(tmp[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[2], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[3], CultureInfo.InvariantCulture.NumberFormat));
                    GameObject.Find(tmp[0]).transform.localEulerAngles = new Vector3(float.Parse(tmp[4], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[5], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[6], CultureInfo.InvariantCulture.NumberFormat));
                    dataReader.Recycle();
                }
                else
                {
                    server.SendToAll(writer, DeliveryMethod.ReliableOrdered, fromPeer);
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
        };
    }
    void Update()
    {
        if (connected)
            server.PollEvents();
    }
    void LateUpdate()
    {
        if (isStarted)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put(selfName + " " + GameObject.Find(selfName).transform.position.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.position.z.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.x.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.y.ToString(CultureInfo.InvariantCulture.NumberFormat) + " " + GameObject.Find(selfName).transform.localEulerAngles.z.ToString(CultureInfo.InvariantCulture.NumberFormat));
            server.SendToAll(writer, DeliveryMethod.Sequenced);
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