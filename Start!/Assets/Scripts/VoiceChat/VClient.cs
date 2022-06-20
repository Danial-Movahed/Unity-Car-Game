using UnityEngine;
using System.Collections;
using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLib.Layers;
using System.Globalization;

public class VClient : MonoBehaviour
{
    public delegate void MicCallbackDelegate(float[] buf);
    public MicCallbackDelegate floatsInDelegate;
    private AudioClip ac;
    private int readHead = 0;
    public bool isStreaming = true;
    private static EventBasedNetListener listener = new EventBasedNetListener();
    public NetManager client = new NetManager(listener);
    public int port = 7777;
    public string Key = "SomeConnectionKey";
    public string ip = "";
    private string selfName;
    private Config config;
    public bool connected = false;
    public AudioSource[] audioSource;
    private float[] zero = new float[8000];
    public void connect()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        selfName = config.selfName;
        SetupBuffers();
        ac = Microphone.Start(null, true, 1, 8000);
        //#####
        listener = new EventBasedNetListener();
        client = new NetManager(listener);
        client.Start();
        client.Connect(ip, port, Key);
        Debug.Log("Connected");
        connected = true;
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            float[] data = dataReader.GetFloatArray();
            int nameAudio = (int)data[data.Length - 2];
            int writeHead = (int)data[data.Length - 1];
            data[data.Length - 1] = 0;
            data[data.Length - 2] = 0;
            audioSource[nameAudio - 1].clip.SetData(data, writeHead);
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
            client.PollEvents();
    }

    private void OnApplicationQuit()
    {
        client.Stop();
    }

    public void sendData(float[] data)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.PutArray(data);
        client.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
    public void sendData(int data)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(data);
        client.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
    // - - -

    void FlushToListeners()
    {
        int writeHead = Microphone.GetPosition(null);

        if (readHead == writeHead || potBuffers == null)
            return;
        if (isStreaming)
        {
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
                    float[] newData = new float[n + 2];
                    ac.GetData(newData, readHead);
                    //###############################################
                    newData[n] = int.Parse(selfName);
                    newData[n + 1] = writeHead;
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
        else
        {
            zero[7998] = int.Parse(selfName);
            zero[7999] = writeHead;
            sendData(zero);
        }
    }
}