using UnityEngine;
using UnityEngine.UI;

public class MenuController678 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
	void Start () {
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
        });
	}
}