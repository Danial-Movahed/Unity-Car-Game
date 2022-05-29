using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerServerWaiting2 : MonoBehaviour {
	public Button quitBtn;
    public Button backBtn;
    public Button settingsBtn;
    private Config configScript;
    private Server serverScript;
	void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        serverScript = GameObject.Find("Server").GetComponent<Server>();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backBtn.onClick.AddListener( () => 
        {
            Debug.Log("back");
            configScript.carSelector = 0;
            serverScript.sendData("0 0");
            configScript.playerCars[0] = 0;
            SceneManager.LoadScene("ServerCarSelector");
        });
        settingsBtn.onClick.AddListener( () => {
            Debug.Log("settings");
            configScript.lastSceneSettings = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        });
	}
}