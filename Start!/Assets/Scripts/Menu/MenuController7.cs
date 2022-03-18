using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController7 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button map1Btn;
    public Button map2Btn;
	void Start () {
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
            SceneManager.LoadScene("Map1");
        });
        map2Btn.onClick.AddListener( () => {
            Debug.Log("map2");
            SceneManager.LoadScene("Map2");
        });
	}
}