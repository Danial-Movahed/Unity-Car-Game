using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapController : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("isFullscreen",1) == 1)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        Application.targetFrameRate = 500;
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1f);
        SceneManager.LoadScene("1");
    }
}