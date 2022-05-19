using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerClientWaiting : MonoBehaviour {
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
            GameObject.Find("Client").GetComponent<Client>().client.DisconnectAll();
            Destroy(GameObject.Find("Client"));
            SceneManager.LoadScene("JoinServer");
        });
	}
}