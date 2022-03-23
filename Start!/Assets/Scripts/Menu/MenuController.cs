using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	public Button quitBtn;
    public Button settingBtn;
    public Button startBtn;
	void Start()
    {
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        settingBtn.onClick.AddListener( () => {
            Debug.Log("setting");
        });
        startBtn.onClick.AddListener( () => 
        {
            Debug.Log("Start");
            SceneManager.LoadScene("5");
        });
	}
}