using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerFinish : MonoBehaviour
{
    public Button okBtn;
	void Start () {
		okBtn.onClick.AddListener( () => {
            Debug.Log("ok");
            if(GameObject.Find("Server"))
            {
                GameObject.Find("Server").GetComponent<Server>().isStarted = false;
                GameObject.Find("Server").GetComponent<Server>().server.Stop();
            }
            if(GameObject.Find("Client"))
            {
                GameObject.Find("Client").GetComponent<Client>().isStarted = false;
                GameObject.Find("Client").GetComponent<Client>().client.Stop();
            }
            Destroy(GameObject.Find("VideoMainCamera"));
            Destroy(GameObject.Find("ConfigStart"));
            Destroy(GameObject.Find("Config"));
            Destroy(GameObject.Find("Video"));
            Destroy(GameObject.Find("Server"));
            Destroy(GameObject.Find("Client"));
            SceneManager.LoadScene("Bootstrap");
        });
	}
}
