using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerJoinServer : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Button Joinbtn;
    public InputField ipInput;
    public Client client;
	void Start () {
        client = GameObject.Find("Client").GetComponent<Client>();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            Destroy(GameObject.Find("Client"));
            SceneManager.LoadScene("6");
        });
        Joinbtn.onClick.AddListener( () => {
            Debug.Log("join");
            client.ip = ipInput.text;
            client.connect();
            SceneManager.LoadScene("ClientWaiting");
        });
	}
}