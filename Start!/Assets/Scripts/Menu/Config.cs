using UnityEngine;
using UnityEngine.SceneManagement;

public class Config : MonoBehaviour
{
    public int mapSelector = 1;
    public int carSelector = 1;
    public GameObject[] cars1;
    public GameObject[] cars2;
    public void startGame()
    {
        Debug.Log(mapSelector);
        Debug.Log(carSelector);
        if(mapSelector == 1)
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
}