using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerClientWaiting : MonoBehaviour
{
    public Button quitBtn;
    public Button backBtn;
    public Button settingsBtn;
    private Config configScript;
    void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        quitBtn.onClick.RemoveAllListeners();
        backBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener(() =>
        {
            Debug.Log("quit");
            Application.Quit();
        });
        backBtn.onClick.AddListener(() =>
        {
            Debug.Log("back");
            GameObject.Find("Client").GetComponent<Client>().isStarted = false;
            GameObject.Find("Client").GetComponent<Client>().connected = false;
            GameObject.Find("Client").GetComponent<Client>().client.Stop();
            SceneManager.LoadScene("JoinServer");
        });
        settingsBtn.onClick.AddListener(() =>
        {
            Debug.Log("settings");
            configScript.lastSceneSettings = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        });
    }
}