using UnityEngine;
using System.Collections;
using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLib.Layers;
using System.Globalization;

public class VServer : MonoBehaviour
{
    public delegate void MicCallbackDelegate(float[] buf);
    public MicCallbackDelegate floatsInDelegate;
    private AudioClip ac;
    private int readHead = 0;
    private bool isStreaming = true;
    private static EventBasedNetListener listener = new EventBasedNetListener();
    public NetManager server = new NetManager(listener);
    public int port = 7777;
    public int connectionLimit = 3;
    public string Key = "SomeConnectionKey";
    private string selfName;
    private Config config;
    public bool connected = false;
    public AudioSource[] audioSource;
    public void connect()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        selfName = config.selfName;
        SetupBuffers();
        ac = Microphone.Start(null, true, 1, 8000);
        //#####
        listener = new EventBasedNetListener();
        server = new NetManager(listener);
        server.Start(port);
        Debug.Log("started server");
        connected = true;
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
        };
        listener.PeerDisconnectedEvent += (peer, dcInfo) =>
        {
            Debug.Log("Disconnected " + peer.EndPoint);
        };
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            float[] data = dataReader.GetFloatArray();
            sendData(data, fromPeer);
            int nameAudio = (int)data[data.Length - 1];
            data[data.Length - 1] = 0;
            audioSource[nameAudio-1].clip.SetData(data, 0);
            audioSource[nameAudio-1].time = 0;
        };
    }
    class POTBuf
    {
        public const int POT_min = 6;      // 2^6 = 64
        public const int POT_max = 10;      // 2^10 = 1024

        const int redundancy = 8;
        int index = 0;

        float[][] internalBuffers = new float[redundancy][];

        public float[] buf
        {
            get
            {
                return internalBuffers[index];
            }
        }

        public void Cycle()
        {
            index = (index + 1) % redundancy;
        }

        public POTBuf(int POT)
        {
            for (int r = 0; r < redundancy; r++)
            {
                internalBuffers[r] = new float[1 << POT];
            }
        }
    }

    POTBuf[] potBuffers = new POTBuf[POTBuf.POT_max + 1];

    void SetupBuffers()
    {
        for (int k = POTBuf.POT_min; k <= POTBuf.POT_max; k++)
            potBuffers[k] = new POTBuf(k);
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    void FixedUpdate()
    {
        if (connected)
            FlushToListeners();
    }

    void Update()
    {
        if (connected)
            server.PollEvents();
    }

    private void OnApplicationQuit()
    {
        server.Stop();
    }

    public void sendData(float[] data)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.PutArray(data);
        server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
    public void sendData(float[] data, NetPeer peer)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.PutArray(data);
        server.SendToAll(writer, DeliveryMethod.ReliableOrdered, peer);
    }
    public void sendData(int data)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(data);
        server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
    public void sendData(int data, NetPeer peer)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(data);
        server.SendToAll(writer, DeliveryMethod.ReliableOrdered, peer);
    }
    // - - -

    void FlushToListeners()
    {
        int writeHead = Microphone.GetPosition(null);

        if (readHead == writeHead || potBuffers == null || !isStreaming)
            return;

        int nFloatsToGet = (ac.samples + writeHead - readHead) % ac.samples;

        for (int k = POTBuf.POT_max; k >= POTBuf.POT_min; k--)
        {
            POTBuf B = potBuffers[k];

            int n = B.buf.Length; // i.e.  1 << k;

            while (nFloatsToGet >= n)
            {

                // If the read length from the offset is longer than the clip length,
                //   the read will wrap around and read the remaining samples
                //   from the start of the clip.
                float[] newData = new float[n + 1];
                ac.GetData(newData, readHead);
                //###############################################
                newData[n] = int.Parse(selfName);
                sendData(newData);
                //###############################################
                readHead = (readHead + n) % ac.samples;

                if (floatsInDelegate != null)
                    floatsInDelegate(B.buf);

                B.Cycle();
                nFloatsToGet -= n;
            }
        }
    }
}