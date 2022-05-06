using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerJoinServer : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button Createbtn;
    public Button Joinbtn;
	void Start () {
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
	}
}