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
    void Awake()
    {
        client.Start();
        client.Connect(ip, port, "SomeConnectionKey");
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            string[] tmp=dataReader.GetString(400).Split(' ');
            GameObject.Find(tmp[0]).transform.position = new Vector3(float.Parse(tmp[1], CultureInfo.InvariantCulture.NumberFormat),float.Parse(tmp[2], CultureInfo.InvariantCulture.NumberFormat),float.Parse(tmp[3], CultureInfo.InvariantCulture.NumberFormat));
            GameObject.Find(tmp[0]).GetComponent<Rigidbody>().velocity = new Vector3(float.Parse(tmp[4], CultureInfo.InvariantCulture.NumberFormat),float.Parse(tmp[5], CultureInfo.InvariantCulture.NumberFormat),float.Parse(tmp[6], CultureInfo.InvariantCulture.NumberFormat));
            GameObject.Find(tmp[0]).GetComponent<Rigidbody>().angularVelocity = new Vector3(float.Parse(tmp[7], CultureInfo.InvariantCulture.NumberFormat),float.Parse(tmp[8], CultureInfo.InvariantCulture.NumberFormat),float.Parse(tmp[9], CultureInfo.InvariantCulture.NumberFormat));
            GameObject.Find(tmp[0]).transform.localEulerAngles = new Vector3(float.Parse(tmp[10], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[11], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[12], CultureInfo.InvariantCulture.NumberFormat));
            dataReader.Recycle();
        };
    }

    void LateUpdate()
    {
        client.PollEvents();
    }

    private void OnApplicationQuit()
    {
        client.Stop();
    }
}
