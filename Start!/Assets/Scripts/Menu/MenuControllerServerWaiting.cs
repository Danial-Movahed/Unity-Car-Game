using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

public class MenuControllerServerWaiting : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Text text;
    private Server server;
    public GameObject StartBtn;
    public Button startbtn;
    private string strHostName = "";
	void Start() {
        StartBtn.SetActive(false);
        server = GameObject.Find("Server").GetComponent<Server>();
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            server.server.Stop();
            Destroy(GameObject.Find("Server"));
            SceneManager.LoadScene("6");
        });
        startbtn.onClick.AddListener( () => {
            Debug.Log("start");
            server.Key = "hmmm I wonder why can't you connect to server?!?";
            server.sendData((server.connectedNow).ToString());
            SceneManager.LoadScene("ServerMapSelector");
        });
        
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress[] addr = ipEntry.AddressList;
        for (int i = 0; i < addr.Length; i++)
        {
            strHostName += addr[i].ToString() + ", ";
        }
        strHostName = strHostName.Remove(strHostName.Length-2);
        text.text = "Waiting for players to join...\n Your ip is: " + strHostName;
	}
    void Update()
    {
        if(server.connectedNow > 1)
        {
            StartBtn.SetActive(true);
            text.text = server.connectedNow+" Players joined...\n Your ip is: " + strHostName;
        }
        else
        {
            StartBtn.SetActive(false);
            text.text = "Waiting for players to join...\n Your ip is: " + strHostName;
        }
    }
}