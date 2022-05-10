using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerServerWaiting2 : MonoBehaviour {
	public Button quitBtn;
    public Button backBtn;
	void Start()
    {
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backBtn.onClick.AddListener( () => 
        {
            Debug.Log("back");
            SceneManager.LoadScene("5");
        });
	}
}