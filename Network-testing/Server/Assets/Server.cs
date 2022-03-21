using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLib.Layers;
public class Server : MonoBehaviour
{
    private static EventBasedNetListener listener = new EventBasedNetListener();
    private NetManager server = new NetManager(listener);
    private NetPeer[] players = new NetPeer[4];
    private int onlineCount = 0;
    public int connectionLimit = 4;
    public int port = 3344;
    private int fCount = 0;
    private bool isSend = true;
    void Awake()
    {
        server.Start(port);
        Debug.Log("started server");
        listener.ConnectionRequestEvent += request =>
        {
            if(server.ConnectedPeersCount < connectionLimit)
                request.AcceptIfKey("SomeConnectionKey");
            else
                request.Reject();
        };

        listener.PeerDisconnectedEvent += (peer, dcInfo) =>
        {
            Debug.Log("Disconnected "+onlineCount.ToString());
            onlineCount-=1;
            for(int j=0;j<=onlineCount;j++)
            {
                if(players[j].Id == peer.Id)
                {
                    if(j != 3)
                    {
                        for(int k=j;k<onlineCount;k++)
                        {
                            players[k]=players[k+1];
                        }
                        players[onlineCount]=null;
                    }
                    else
                    {
                        players[j]=null;
                    }
                    break;
                }
            }
            Debug.Log(onlineCount);
        };
        listener.PeerConnectedEvent += peer =>
        {
            Debug.Log("We got connection: "+peer.EndPoint);
            players[onlineCount]=peer;
            onlineCount++;
            Debug.Log(onlineCount);
        };
    }
    void LateUpdate()
    {
        server.PollEvents();
        if(onlineCount>0 && isSend)
        {
            for(int i=0;i<onlineCount;i++)
            {
                Debug.Log(players[i].Id);
                NetDataWriter msg = new NetDataWriter();
                msg.Put("Blue "+GameObject.Find("Blue").transform.position.x+" "+GameObject.Find("Blue").transform.position.y+" "+GameObject.Find("Blue").transform.position.z+" "+GameObject.Find("Blue").GetComponent<Rigidbody>().velocity.x+" "+GameObject.Find("Blue").GetComponent<Rigidbody>().velocity.y+" "+GameObject.Find("Blue").GetComponent<Rigidbody>().velocity.z+" "+GameObject.Find("Blue").GetComponent<Rigidbody>().angularVelocity.x+" "+GameObject.Find("Blue").GetComponent<Rigidbody>().angularVelocity.y+" "+GameObject.Find("Blue").GetComponent<Rigidbody>().angularVelocity.z+" "+GameObject.Find("Blue").transform.localEulerAngles.x+" "+GameObject.Find("Blue").transform.localEulerAngles.y+" "+GameObject.Find("Blue").transform.localEulerAngles.z);
                players[i].Send(msg, DeliveryMethod.ReliableSequenced);
            }
            fCount++;
            if(fCount>=3)
                isSend=false;
            return;
        }
        fCount--;
        if(fCount<=0)
            isSend=true;
    }
    private void OnApplicationQuit()
    {
        server.Stop();
    }
}