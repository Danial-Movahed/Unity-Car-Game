using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController10 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button PracticeBtn;
    public Button TimeRacerBtn;
    public Button settingsBtn;
    private Config config;
	void Start () {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        quitBtn.onClick.RemoveAllListeners();
        backbtn.onClick.RemoveAllListeners();
        PracticeBtn.onClick.RemoveAllListeners();
        TimeRacerBtn.onClick.RemoveAllListeners();
        settingsBtn.onClick.RemoveAllListeners();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("5");
        });
        PracticeBtn.onClick.AddListener( () => 
        {
            Debug.Log("Practice");
            config.modeSelector = 0;
            SceneManager.LoadScene("7");
        });
        TimeRacerBtn.onClick.AddListener( () => 
        {
            Debug.Log("TimeRacer");
            config.modeSelector = 1;
            SceneManager.LoadScene("7");
        });
        settingsBtn.onClick.AddListener( () => 
        {
            Debug.Log("Settings");
            config.lastSceneSettings = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        });
	}
}