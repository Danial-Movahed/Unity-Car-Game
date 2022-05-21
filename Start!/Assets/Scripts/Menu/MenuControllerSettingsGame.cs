using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerSettingsGame : MonoBehaviour
{
    public Button backBtn;
    public GameObject defaultMenu;
    public GameObject settingsMenu;
    private Config configScript;
    void Start()
    {
        configScript = GameObject.Find("ConfigStart").GetComponent<Config>();
        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(() =>
        {
            settingsMenu.SetActive(false);
            defaultMenu.SetActive(true);
        });
    }
    public void savePrefs()
    {
        PlayerPrefs.SetInt("isFullscreen", GameObject.Find("Fullscreen Switch").GetComponent<Toggle>().isOn ? 1 : 0);
        PlayerPrefs.SetFloat("volume", GameObject.Find("Sound Slider").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("cameraSmooth2", GameObject.Find("Smooth Slider2").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("cameraDistance2", GameObject.Find("Distance Slider2").GetComponent<Slider>().value * 23.34f);
        PlayerPrefs.SetFloat("cameraHeight2", GameObject.Find("Height Slider2").GetComponent<Slider>().value * 5f);
        PlayerPrefs.SetFloat("cameraAngle2", GameObject.Find("Angle Slider2").GetComponent<Slider>().value * 8f);
        PlayerPrefs.SetFloat("cameraSmooth1", GameObject.Find("Smooth Slider").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("cameraDistance1", GameObject.Find("Distance Slider").GetComponent<Slider>().value * 54);
        PlayerPrefs.SetFloat("cameraHeight1", GameObject.Find("Height Slider").GetComponent<Slider>().value * 10.2f);
        PlayerPrefs.SetFloat("cameraAngle1", GameObject.Find("Angle Slider").GetComponent<Slider>().value * 23.34f);
        if (configScript.mapSelector == 2)
        {
            GameObject.Find("camera").GetComponent<VehicleCamera>().smooth = PlayerPrefs.GetFloat("cameraSmooth2", 0.3f);
            GameObject.Find("camera").GetComponent<VehicleCamera>().distance = PlayerPrefs.GetFloat("cameraDistance2", 7f);
            GameObject.Find("camera").GetComponent<VehicleCamera>().height = PlayerPrefs.GetFloat("cameraHeight2", 2.5f);
            GameObject.Find("camera").GetComponent<VehicleCamera>().Angle = PlayerPrefs.GetFloat("cameraAngle2", 4f);
        }
        else
        {
            GameObject.Find("camera").GetComponent<VehicleCamera>().smooth = PlayerPrefs.GetFloat("cameraSmooth1", 0.3f);
            GameObject.Find("camera").GetComponent<VehicleCamera>().distance = PlayerPrefs.GetFloat("cameraDistance1", 16.2f);
            GameObject.Find("camera").GetComponent<VehicleCamera>().height = PlayerPrefs.GetFloat("cameraHeight1", 5.1f);
            GameObject.Find("camera").GetComponent<VehicleCamera>().Angle = PlayerPrefs.GetFloat("cameraAngle1", 11.67f);
        }
    }
}
