using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DontDestoryOnLoadScript : MonoBehaviour
{    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}