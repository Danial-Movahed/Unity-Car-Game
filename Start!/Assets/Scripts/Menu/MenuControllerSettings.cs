using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerSettings : MonoBehaviour
{
    public Button backBtn;
    private Config configScript;
    void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(configScript.lastSceneSettings);
        });
    }
    public void savePrefs()
    {
        PlayerPrefs.SetInt("isFullscreen", GameObject.Find("Fullscreen Switch").GetComponent<Toggle>().isOn ? 1 : 0);
        if (PlayerPrefs.GetInt("isFullscreen") == 1)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        PlayerPrefs.SetFloat("volume", GameObject.Find("Sound Slider").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("cameraSmooth2", GameObject.Find("Smooth Slider2").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("cameraDistance2", GameObject.Find("Distance Slider2").GetComponent<Slider>().value * 23.34f);
        PlayerPrefs.SetFloat("cameraHeight2", GameObject.Find("Height Slider2").GetComponent<Slider>().value * 5f);
        PlayerPrefs.SetFloat("cameraAngle2", GameObject.Find("Angle Slider2").GetComponent<Slider>().value * 8f);
        PlayerPrefs.SetFloat("cameraSmooth1", GameObject.Find("Smooth Slider").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("cameraDistance1", GameObject.Find("Distance Slider").GetComponent<Slider>().value * 54);
        PlayerPrefs.SetFloat("cameraHeight1", GameObject.Find("Height Slider").GetComponent<Slider>().value * 10.2f);
        PlayerPrefs.SetFloat("cameraAngle1", GameObject.Find("Angle Slider").GetComponent<Slider>().value * 23.34f);
    }
}
