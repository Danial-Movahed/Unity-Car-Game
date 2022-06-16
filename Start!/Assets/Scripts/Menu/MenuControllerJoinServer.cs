using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerJoinServer : MonoBehaviour
{
    public Button quitBtn;
    public Button backbtn;
    public Button Joinbtn;
    public Button settingsBtn;
    public InputField ipInput;
    private Client client;
    private VClient vclient;
    private Config configScript;
    void Start()
    {
        client = GameObject.Find("Client").GetComponent<Client>();
        vclient = GameObject.Find("VClient").GetComponent<VClient>();
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        quitBtn.onClick.RemoveAllListeners();
        backbtn.onClick.RemoveAllListeners();
        Joinbtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener(() =>
        {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener(() =>
        {
            Debug.Log("back");
            client.isStarted = false;
            client.connected = false;
            client.client.Stop();
            vclient.connected = false;
            vclient.client.Stop();
            SceneManager.LoadScene("6");
        });
        Joinbtn.onClick.AddListener(() =>
        {
            Debug.Log("join");
            client.ip = ipInput.text;
            client.connect();
            SceneManager.LoadScene("ClientWaiting");
        });
        settingsBtn.onClick.AddListener(() =>
        {
            Debug.Log("settings");
            configScript.lastSceneSettings = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        });
    }
}