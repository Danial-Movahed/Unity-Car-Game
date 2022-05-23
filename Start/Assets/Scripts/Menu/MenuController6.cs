using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController6 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button Createbtn;
    public Button Joinbtn;
	void Start () {
        quitBtn.onClick.RemoveAllListeners();
        backbtn.onClick.RemoveAllListeners();
        Createbtn.onClick.RemoveAllListeners();
        Joinbtn.onClick.RemoveAllListeners();
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