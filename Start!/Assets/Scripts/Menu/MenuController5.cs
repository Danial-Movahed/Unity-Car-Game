using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController5 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button singleplayerBtn;
    public Button multiplayerBtn;
	void Start () {
        quitBtn.onClick.RemoveAllListeners();
        backbtn.onClick.RemoveAllListeners();
        singleplayerBtn.onClick.RemoveAllListeners();
        multiplayerBtn.onClick.RemoveAllListeners();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("1");
        });
        singleplayerBtn.onClick.AddListener( () => 
        {
            Debug.Log("singleplayer");
            SceneManager.LoadScene("10");
        });
        multiplayerBtn.onClick.AddListener( () => 
        {
            Debug.Log("multiplayer");
            SceneManager.LoadScene("6");
        });
	}
}