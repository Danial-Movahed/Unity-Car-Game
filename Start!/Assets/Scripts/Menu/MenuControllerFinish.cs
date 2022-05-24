using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerFinish : MonoBehaviour
{
    public Button okBtn;
    public Text statusText;
    private Config config;
    void Start()
    {
        config = GameObject.Find("ConfigStart").GetComponent<Config>();
        okBtn.onClick.RemoveAllListeners();
        if (config.selfName != "")
        {
            statusText.fontSize = 20;
        }
        statusText.text = "Finished\n" + config.message;
        okBtn.onClick.AddListener(() =>
        {
            Debug.Log("ok");
            Destroy(GameObject.Find("VideoMainCamera"));
            Destroy(GameObject.Find("ConfigStart"));
            Destroy(GameObject.Find("Config"));
            Destroy(GameObject.Find("Video"));
            SceneManager.LoadScene("Bootstrap");
        });
    }
}
