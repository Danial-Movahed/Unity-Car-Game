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
    public void startGame()
    {
        Debug.Log(mapSelector);
        Debug.Log(carSelector);
        if (modeSelector == 0)
        {
            if (mapSelector == 1)
            {
                SceneManager.LoadScene("Map1");
                Destroy(GameObject.Find("Video"));
                Destroy(GameObject.Find("VideoMainCamera"));
            }
            else
            {
                SceneManager.LoadScene("Map2");
                Destroy(GameObject.Find("Video"));
                Destroy(GameObject.Find("VideoMainCamera"));
            }
        }
        else
        {
            if (mapSelector == 1)
            {
                SceneManager.LoadScene("Map1Ghost");
                Destroy(GameObject.Find("Video"));
                Destroy(GameObject.Find("VideoMainCamera"));
            }
            else
            {
                SceneManager.LoadScene("Map2Ghost");
                Destroy(GameObject.Find("Video"));
                Destroy(GameObject.Find("VideoMainCamera"));
            }
        }
    }
}