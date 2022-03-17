using UnityEngine;
using UnityEngine.UI;

public class MenuController5 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button singleplayerBtn;
    public Button multiplayerBtn;
	void Start () {
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
        });
        singleplayerBtn.onClick.AddListener( () => 
        {
            Debug.Log("singleplayer");
        });
        multiplayerBtn.onClick.AddListener( () => 
        {
            Debug.Log("multiplayer");
        });
	}
}