using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerClientCarSelector : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button startBtn;
    public Button nextCarBtn;
    public Button prevCarBtn;
    public int carIndex;
    public Image carImage;
    public Image carImageThingy;
    private Config configScript;
    private Client clientScript;
	void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        clientScript = GameObject.Find("Client").GetComponent<Client>();
        updateCarSelector();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            clientScript.client.DisconnectAll();
            Destroy(GameObject.Find("Client"));
            SceneManager.LoadScene("JoinServer");
        });
        startBtn.onClick.AddListener( () => {
            Debug.Log("start");
            configScript.carSelector = carIndex;
            clientScript.sendData((int.Parse(clientScript.selfName)-1).ToString()+ " " + carIndex.ToString());
            configScript.playerCars[(int.Parse(clientScript.selfName)-1)] = carIndex;
            SceneManager.LoadScene("ClientWaiting2");
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