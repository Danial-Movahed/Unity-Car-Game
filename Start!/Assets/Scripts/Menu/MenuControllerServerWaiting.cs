using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

public class MenuControllerServerWaiting : MonoBehaviour
{
    public Button quitBtn;
    public Button backbtn;
    public Button settingsBtn;
    public Text text;
    private Server server;
    private VServer vserver;
    public GameObject StartBtn;
    public Button startbtn;
    private string strHostName = "";
    private Config configScript;
    void Start()
    {
        StartBtn.SetActive(false);
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        server = GameObject.Find("Server").GetComponent<Server>();
        server.connect();
        vserver = GameObject.Find("VServer").GetComponent<VServer>();
        vserver.connect();
        quitBtn.onClick.RemoveAllListeners();
        backbtn.onClick.RemoveAllListeners();
        startbtn.onClick.RemoveAllListeners();
        settingsBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener(() =>
        {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener(() =>
        {
            Debug.Log("back");
            server.Key = "SomeConnectionKey";
            server.connected = false;
            server.isStarted = false;
            server.server.Stop();
            SceneManager.LoadScene("6");
        });
        startbtn.onClick.AddListener(() =>
        {
            Debug.Log("start");
            server.Key = "hmmm I wonder why can't you connect to server?!?";
            server.sendData((server.connectedNow).ToString());
            SceneManager.LoadScene("ServerMapSelector");
        });
        settingsBtn.onClick.AddListener(() =>
        {
            Debug.Log("settings");
            configScript.lastSceneSettings = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        });
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress[] addr = ipEntry.AddressList;
        for (int i = 0; i < addr.Length; i++)
        {
            strHostName += addr[i].ToString() + ", ";
        }
        strHostName = strHostName.Remove(strHostName.Length - 2);
        text.text = "Waiting for players to join...\n Your ip is: " + strHostName;
    }
    void Update()
    {
        if (server.connectedNow > 1)
        {
            StartBtn.SetActive(true);
            text.text = server.connectedNow + " Players joined...\n Your ip is: " + strHostName;
        }
        else
        {
            StartBtn.SetActive(false);
            text.text = "Waiting for players to join...\n Your ip is: " + strHostName;
        }
    }
}