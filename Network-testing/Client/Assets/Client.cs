using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLib.Layers;
using System.Globalization;

public class Client : MonoBehaviour
{
    private static EventBasedNetListener listener = new EventBasedNetListener();
    private NetManager client = new NetManager(listener);
    public string ip = "localhost";
    public int port = 3344;
    void Start()
    {
        listener.PeerConnectedEvent += (server) => {
            Debug.LogError($"Connected to server: {server}");
        };
        client.Start();
        client.Connect(ip, port, "SomeConnectionKey");
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            string tmp = dataReader.GetString(400);
            Debug.Log($"Received: {tmp}");
        };
    }

    void LateUpdate()
    {
        client.PollEvents();
    }

}
