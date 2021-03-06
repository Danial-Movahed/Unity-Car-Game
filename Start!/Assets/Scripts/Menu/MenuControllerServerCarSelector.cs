using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerServerCarSelector : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button startBtn;
    public Button nextCarBtn;
    public Button prevCarBtn;
    public int carIndex;
    public Image carImage;
    public Image carImageThingy;
    private Config configScript;
    private Server serverScript;
	void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        serverScript = GameObject.Find("Server").GetComponent<Server>();
        updateCarSelector();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            serverScript.sendData("RM");
            SceneManager.LoadScene("ServerMapSelector");
        });
        startBtn.onClick.AddListener( () => {
            Debug.Log("start");
            configScript.carSelector = carIndex;
            serverScript.sendData("0 " + carIndex);
            configScript.playerCars[0] = carIndex;
            SceneManager.LoadScene("ServerWaiting2");
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