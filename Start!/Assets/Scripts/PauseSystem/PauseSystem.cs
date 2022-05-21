using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject defaultMenu;
    public GameObject settingsMenu;
    public Button resumeBtn;
    public Button quitBtn;
    public Button settingsBtn;
    void Start()
    {
        resumeBtn.onClick.RemoveAllListeners();
        resumeBtn.onClick.AddListener(() =>
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        });
        quitBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        settingsBtn.onClick.RemoveAllListeners();
        settingsBtn.onClick.AddListener(() =>
        {
            defaultMenu.SetActive(false);
            settingsMenu.SetActive(true);
        });
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }
}
