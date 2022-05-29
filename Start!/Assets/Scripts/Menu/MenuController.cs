using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	public Button quitBtn;
    public Button settingBtn;
    public Button startBtn;
    public Button videoBtn;
    private Config configScript;
	void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        quitBtn.onClick.RemoveAllListeners();
        settingBtn.onClick.RemoveAllListeners();
        startBtn.onClick.RemoveAllListeners();
        videoBtn.onClick.RemoveAllListeners();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        settingBtn.onClick.AddListener( () => {
            Debug.Log("setting");
            configScript.lastSceneSettings = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Settings");
        });
        startBtn.onClick.AddListener( () => 
        {
            Debug.Log("Start");
            SceneManager.LoadScene("5");
        });
        videoBtn.onClick.AddListener( () => 
        {
            Debug.Log("Video");
            configScript.ifVideo = true;
        });
	}
}