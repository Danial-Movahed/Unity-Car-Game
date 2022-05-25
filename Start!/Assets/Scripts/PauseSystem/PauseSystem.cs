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
    public GameObject lastlap;
    private bool isLastLapShown = false;
    private Server server;
    private Client client;
    void Start()
    {
        server = GameObject.Find("Server").GetComponent<Server>();
        client = GameObject.Find("Client").GetComponent<Client>();
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
        lastlap.SetActive(false);
    }
    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseShowMenu();
        }
        if(server.isStarted)
        {
            if(GameObject.Find(server.selfName).GetComponent<Lap>().lap == 3 && !isLastLapShown)
            {
                lastlap.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(3, lastlap));
                isLastLapShown = true;
            }
        }
        else if(client.isStarted)
        {
            if (GameObject.Find(client.selfName).GetComponent<Lap>().lap == 3 && !isLastLapShown)
            {
                lastlap.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(3, lastlap));
                isLastLapShown = true;
            }
        }
        else if(GameObject.Find("Player").GetComponent<Lap>().lap == 3 && !isLastLapShown)
        {
            lastlap.SetActive(true);
            StartCoroutine(RemoveAfterSeconds(3, lastlap));
            isLastLapShown = true;
        }
    }
    public void pauseShowMenu()
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
