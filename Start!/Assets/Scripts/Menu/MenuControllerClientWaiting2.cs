using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerClientWaiting2 : MonoBehaviour {
	public Button quitBtn;
    public Button backBtn;
    public Button settingsBtn;
    private Config configScript;
    private Client clientScript;
	void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        clientScript = GameObject.Find("Client").GetComponent<Client>();
        quitBtn.onClick.RemoveAllListeners();
        backBtn.onClick.RemoveAllListeners();
        settingsBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backBtn.onClick.AddListener( () => 
        {
            Debug.Log("back");
            configScript.carSelector = 0;
            clientScript.sendData((int.Parse(clientScript.selfName)-1).ToString()+ " 0");
            configScript.playerCars[(int.Parse(clientScript.selfName)-1)] = 0;
            SceneManager.LoadScene("ClientCarSelector");
        });
        settingsBtn.onClick.AddListener( () => 
        {
            Debug.Log("settings");
            configScript.lastSceneSettings = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        });
	}
}