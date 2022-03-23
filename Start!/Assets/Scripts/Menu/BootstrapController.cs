using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BootstrapController : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("1");
    }
}