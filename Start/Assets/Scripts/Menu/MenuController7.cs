using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController7 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button map1Btn;
    public Button map2Btn;
    private Config configScript;
	void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        quitBtn.onClick.RemoveAllListeners();
        backbtn.onClick.RemoveAllListeners();
        map1Btn.onClick.RemoveAllListeners();
        map2Btn.onClick.RemoveAllListeners();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("10");
        });
        map1Btn.onClick.AddListener( () => {
            Debug.Log("map1");
            configScript.mapSelector = 1;
            SceneManager.LoadScene("8");
        });
        map2Btn.onClick.AddListener( () => {
            Debug.Log("map2");
            configScript.mapSelector = 2;
            SceneManager.LoadScene("8");
        });
	}
}