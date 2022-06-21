using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerFinish : MonoBehaviour
{
    public Button okBtn;
    private Config configscript;
    public GameObject textmsg;
    public GameObject defimg;
    void Start()
    {
        configscript = GameObject.Find("ConfigStart").GetComponent<Config>();
        if(configscript.message != "")
        {
            defimg.SetActive(false);
            textmsg.GetComponent<Text>().text = configscript.message;
            textmsg.SetActive(true);
        }
        okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.AddListener(() =>
        {
            Debug.Log("ok");
            Destroy(GameObject.Find("VideoMainCamera"));
            Destroy(GameObject.Find("ConfigStart"));
            Destroy(GameObject.Find("Video"));
            Destroy(GameObject.Find("VServer"));
            Destroy(GameObject.Find("VClient"));
            Destroy(GameObject.Find("Server"));
            Destroy(GameObject.Find("Client"));
            Destroy(GameObject.Find("Player1Voice"));
            Destroy(GameObject.Find("Player2Voice"));
            Destroy(GameObject.Find("Player3Voice"));
            Destroy(GameObject.Find("Player4Voice"));
            SceneManager.LoadScene("Bootstrap");
        });
    }
}
