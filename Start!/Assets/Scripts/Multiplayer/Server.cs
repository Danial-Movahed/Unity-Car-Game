using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Globalization;

public class Server : MonoBehaviour
{
    private static EventBasedNetListener listener = new EventBasedNetListener();
    private NetManager server = new NetManager(listener);
    public int connectionLimit = 3;
    public int port = 3344;
    public bool isSend = false;
    public string selfName = "";
    public int connectedNow = 0;
    public bool isStarted = false;
    public string Key = "SomeConnectionKey";
    public int[] playerCars = new int[4];
    void Awake()
    {
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
            writer.Put(connectedNow+1);
            peer.Send(writer, DeliveryMethod.ReliableOrdered);
        };

        listener.PeerDisconnectedEvent += (peer, dcInfo) =>
        {
            Debug.Log("Disconnected "+peer.EndPoint);
            connectedNow--;
        };
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            if(isStarted)
            {
                string[] tmp = dataReader.GetString(400).Split(' ');
                GameObject.Find(tmp[0]).transform.position = new Vector3(float.Parse(tmp[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[2], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[3], CultureInfo.InvariantCulture.NumberFormat));
                GameObject.Find(tmp[0]).GetComponent<Rigidbody>().velocity = new Vector3(float.Parse(tmp[4], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[5], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[6], CultureInfo.InvariantCulture.NumberFormat));
                GameObject.Find(tmp[0]).GetComponent<Rigidbody>().angularVelocity = new Vector3(float.Parse(tmp[7], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[8], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[9], CultureInfo.InvariantCulture.NumberFormat));
                GameObject.Find(tmp[0]).transform.localEulerAngles = new Vector3(float.Parse(tmp[10], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[11], CultureInfo.InvariantCulture.NumberFormat), float.Parse(tmp[12], CultureInfo.InvariantCulture.NumberFormat));
                dataReader.Recycle();
            }
            else
            {
                int knownCarNum = 0;
                for (int i = 0; i < connectedNow+1; i++)
                {
                    if (playerCars[i] != 0)
                    {
                        knownCarNum++;
                    }
                }
                if (knownCarNum == connectedNow+1)
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
        server.PollEvents();
    }
    void LateUpdate()
    {
        if(isSend)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put(selfName+" "+GameObject.Find(selfName).transform.position.x.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).transform.position.y.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).transform.position.z.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).GetComponent<Rigidbody>().velocity.x.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).GetComponent<Rigidbody>().velocity.y.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).GetComponent<Rigidbody>().velocity.z.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity.x.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity.y.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).GetComponent<Rigidbody>().angularVelocity.z.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).transform.localEulerAngles.x.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).transform.localEulerAngles.y.ToString(CultureInfo.InvariantCulture.NumberFormat)+" "+GameObject.Find(selfName).transform.localEulerAngles.z.ToString(CultureInfo.InvariantCulture.NumberFormat));
            server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
        }
    }
    private void OnApplicationQuit()
    {
        server.Stop();
    }

    public void sendData(string data)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(data);
        server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
}