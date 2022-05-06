using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

public class MenuControllerServerWaiting : MonoBehaviour {
	public Button quitBtn;
    public Button backbtn;
    public Text text;
	void Start() {
		quitBtn.onClick.AddListener( () => {
            Debug.Log("quit");
            Application.Quit();
        });
        backbtn.onClick.AddListener( () => {
            Debug.Log("back");
            SceneManager.LoadScene("5");
        });
        string strHostName = "";
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress[] addr = ipEntry.AddressList;

        for (int i = 0; i < addr.Length; i++)
        {
            strHostName += addr[i].ToString() + ", ";
        }
        strHostName = strHostName.Remove(strHostName.Length-2);
        text.text = "Waiting for players to join...\n Your ip is: " + strHostName;
	}
}