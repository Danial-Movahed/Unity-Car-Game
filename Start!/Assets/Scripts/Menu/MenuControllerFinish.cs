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
