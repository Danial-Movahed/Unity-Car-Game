using UnityEngine;
using UnityEngine.SceneManagement;

public class Config : MonoBehaviour
{
    public int modeSelector = 0;
    public int mapSelector = 1;
    public int carSelector = 1;
    public GameObject[] cars1;
    public GameObject[] cars2;
    public GameObject[] cars2bots;
    public GameObject[] cars1bots;
    public GameObject[] cars1ghost;
    public GameObject[] cars2ghost;
    public int[] playerCars = new int[4];
    public string selfName = "";
    public string message = "";
    public bool isStarted = false;
    public bool ifVideo = false;
    public string lastSceneSettings = "";
    public void startGame()
    {
        string maprunning = "Map";
        maprunning += mapSelector.ToString();
        isStarted = true;
        Debug.Log(mapSelector);
        Debug.Log(carSelector);
        if(ifVideo)
            maprunning += "Video";
        SceneManager.LoadScene(maprunning);
        Destroy(GameObject.Find("Video"));
        Destroy(GameObject.Find("VideoMainCamera"));
    }
}