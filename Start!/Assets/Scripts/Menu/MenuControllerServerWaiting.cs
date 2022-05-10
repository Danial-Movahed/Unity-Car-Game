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
            SceneManager.LoadScene("5");
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
        if(server.connectedNow > 0)
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