using UnityEngine;
using System.Collections;
 
public class WindGeneratorRotator : MonoBehaviour
{
    // axis to rotate on
    public Vector3 axis = new Vector3(0, 1, 0);
    // the minimum and maximum speed the blade can rotate
    public Vector2 minMaxSpeed = new Vector2(100, 110);
    // how often the wind speed changes
    public Vector2 changeFrequency = new Vector2(10, 20);
    // how quickly the blade matches the new wind speed
    public float windChangeSpeed = 3;
 
    private float targetSpeed;
    private float currentSpeed;
 
    private void OnEnable()
    {
        currentSpeed = 0;
        StartCoroutine(Animate());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator Animate()
    {
        while (enabled)
        {          
            // set an initial targetSpeed
            targetSpeed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
            // determine when to change the wind speed next
            float timeout = Time.time + Random.Range(changeFrequency.x, changeFrequency.y);          
            while (Time.time < timeout)
            {
                // interpolate the currentSpeed to match the targetSpeed
                currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * windChangeSpeed);
                // rotate the blade every frame
                transform.Rotate(axis, currentSpeed * Time.deltaTime, Space.Self);
                yield return null;
            }
        }
    }
}
