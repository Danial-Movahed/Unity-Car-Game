using UnityEngine;
using UnityEngine.SceneManagement;

public class Config : MonoBehaviour
{
    public int mapSelector = 1;
    public int carSelector = 1;
    public void startGame()
    {
        Debug.Log(mapSelector);
        Debug.Log(carSelector);
        if(mapSelector == 1)
        {
            SceneManager.LoadScene("Map1");
            Destroy(GameObject.Find("ConfigStart"));
            Destroy(GameObject.Find("Video"));
            Destroy(GameObject.Find("VideoMainCamera"));
            SceneManager.SetActiveScene( SceneManager.GetSceneByName( "Map1" ) );
        }
        else
        {
            SceneManager.LoadScene("Map2");
            Destroy(GameObject.Find("ConfigStart"));
            Destroy(GameObject.Find("Video"));
            Destroy(GameObject.Find("VideoMainCamera"));
            SceneManager.SetActiveScene( SceneManager.GetSceneByName( "Map2" ) );
        }
    }
}