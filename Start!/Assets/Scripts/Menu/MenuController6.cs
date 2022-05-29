using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController6 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button Createbtn;
    public Button Joinbtn;
    public Button settingsBtn;
    private Config configScript;
	void Start () {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        quitBtn.onClick.RemoveAllListeners();
        backbtn.onClick.RemoveAllListeners();
        Createbtn.onClick.RemoveAllListeners();
        Joinbtn.onClick.RemoveAllListeners();
        settingsBtn.onClick.RemoveAllListeners();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("5");
        });
        Createbtn.onClick.AddListener( () => {
            Debug.Log("create");
            SceneManager.LoadScene("ServerWaiting");
        });
        Joinbtn.onClick.AddListener( () => {
            Debug.Log("join");
            SceneManager.LoadScene("JoinServer");
        });
        settingsBtn.onClick.AddListener( () => {
            Debug.Log("settings");
            configScript.lastSceneSettings = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        });
	}
}