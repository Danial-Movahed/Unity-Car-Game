using UnityEngine;
using UnityEngine.UI;

public class MenuController10 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button PracticeBtn;
    public Button TimeRacerBtn;
	void Start () {
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
        });
        PracticeBtn.onClick.AddListener( () => 
        {
            Debug.Log("Practice");
        });
        TimeRacerBtn.onClick.AddListener( () => 
        {
            Debug.Log("TimeRacer");
        });
	}
}