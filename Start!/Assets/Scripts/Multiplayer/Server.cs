using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLib.Layers;
using System.Globalization;

public class Server : MonoBehaviour
{
    private static EventBasedNetListener listener = new EventBasedNetListener();
    private NetManager server = new NetManager(listener);
    public int connectionLimit = 4;
    public int port = 3344;
    private int fCount = 0;
    private bool isSend = true;
    public string selfCarName = "RallyCar";
    void Awake()
    {
        server.Start(port);
        Debug.Log("started server");
        listener.ConnectionRequestEvent += request =>
        {
            if (server.ConnectedPeersCount < connectionLimit)
                request.AcceptIfKey("SomeConnectionKey");
            else
                request.Reject();
        };

        listener.PeerConnectedEvent += peer =>
        {
            Debug.Log("We got connection: " + peer.EndPoint);
        };
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            string[] tmp = dataReader.GetString(400).Split(' ');
            GameObject.Find(tmp[0]).transform.position = new Vector3(float.Parse(tmp[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[2], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[3], CultureInfo.InvariantCulture.NumberFormat));
            GameObject.Find(tmp[0]).GetComponent<Rigidbody>().velocity = new Vector3(float.Parse(tmp[4], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[5], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[6], CultureInfo.InvariantCulture.NumberFormat));
            GameObject.Find(tmp[0]).GetComponent<Rigidbody>().angularVelocity = new Vector3(float.Parse(tmp[7], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[8], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[9], CultureInfo.InvariantCulture.NumberFormat));
            GameObject.Find(tmp[0]).transform.localEulerAngles = new Vector3(float.Parse(tmp[10], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[11], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[12], CultureInfo.InvariantCulture.NumberFormat));
            dataReader.Recycle();
        };
    }
    void LateUpdate()
    {
        server.PollEvents();
        NetDataWriter writer = new NetDataWriter();
        writer.Put(selfCarName+" "+GameObject.Find(selfCarName).transform.position.x.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).transform.position.y.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).transform.position.z.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).GetComponent<Rigidbody>().velocity.x.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).GetComponent<Rigidbody>().velocity.y.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).GetComponent<Rigidbody>().velocity.z.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).GetComponent<Rigidbody>().angularVelocity.x.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).GetComponent<Rigidbody>().angularVelocity.y.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).GetComponent<Rigidbody>().angularVelocity.z.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).transform.localEulerAngles.x.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).transform.localEulerAngles.y.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfCarName).transform.localEulerAngles.z.ToString(CultureInfo.InvariantCulture.NumberFormat));
        server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
    private void OnApplicationQuit()
    {
        server.Stop();
    }
}