using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController8 : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button startBtn;
    public Button nextCarBtn;
    public Button prevCarBtn;
    public int carIndex;
    public Image carImage;
    public Image carImageThingy;
    private Config configScript;
	void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        updateCarSelector();
        quitBtn.onClick.RemoveAllListeners();
        backbtn.onClick.RemoveAllListeners();
        startBtn.onClick.RemoveAllListeners();
        nextCarBtn.onClick.RemoveAllListeners();
        prevCarBtn.onClick.RemoveAllListeners();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("7");
        });
        startBtn.onClick.AddListener( () => {
            Debug.Log("start");
            configScript.carSelector = carIndex;
            configScript.startGame();
        });
        nextCarBtn.onClick.AddListener( () => {
            Debug.Log("next");
            carIndex++;
            updateCarSelector();
        });
        prevCarBtn.onClick.AddListener( () => {
            Debug.Log("prev");
            carIndex--;
            updateCarSelector();
        });
	}
    void updateCarSelector()
    {
        if (carIndex > 5)
        {
            carIndex = 1;
        }
        if (carIndex < 1)
        {
            carIndex = 5;
        }
        carImage.sprite = Resources.Load<Sprite>("Car/Pics/" + carIndex);
        carImageThingy.sprite = Resources.Load<Sprite>("Car/Pics/Thingy" + carIndex);
    }
}