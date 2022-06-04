using UnityEngine;
using System.Collections;

public class straight : MonoBehaviour
{
    private float t = 0.0f;
    private bool is_moving = false;
    IEnumerator hmm(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        is_moving = true;
    }
    void Start()
    {
        StartCoroutine(hmm(0));
    }
    void Update()
    {
        if (is_moving)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Lerp(-10, -40, t), 0, 0);
            t += 0.5f * Time.deltaTime;          
        }
    }
}