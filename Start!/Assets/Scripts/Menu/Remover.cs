using UnityEngine;
using UnityEngine.SceneManagement;

public class Remover : MonoBehaviour
{
    public string[] allowedscenes;
    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    public void ChangedActiveScene(Scene current, Scene next)
    {
        int c = 0;
        for (int i = 0; i < allowedscenes.Length; i++)
        {
            if (next.name == allowedscenes[i])
            {
                c++;
            }
        }
        if (c == 0)
        {
            Destroy(gameObject);
        }
    }
}