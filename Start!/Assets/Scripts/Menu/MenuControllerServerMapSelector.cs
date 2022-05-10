using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerServerMapSelector : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button map1Btn;
    public Button map2Btn;
    private Config configScript;
    private Server server;
	void Start()
    {
        server = GameObject.Find("Server").GetComponent<Server>();
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("ServerWaiting");
        });
        map1Btn.onClick.AddListener( () => {
            Debug.Log("map1");
            configScript.mapSelector = 1;
            server.sendData("map1");
            SceneManager.LoadScene("ServerCarSelector");
        });
        map2Btn.onClick.AddListener( () => {
            Debug.Log("map2");
            configScript.mapSelector = 2;
            server.sendData("map2");
            SceneManager.LoadScene("ServerCarSelector");
        });
	}
}