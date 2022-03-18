using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController8 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
	void Start () {
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("7");
        });
	}
}