using UnityEngine;

public class AllStar : MonoBehaviour
{
    void Update()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.HSVToRGB(Mathf.PingPong(Time.time * 1, 1), 1, 1);
        }
    }
}