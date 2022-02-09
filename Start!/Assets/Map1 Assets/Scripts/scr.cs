using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    public float rotate_amount;
    float initialAngle;

    void Start() {
        initialAngle = transform.rotation.eulerAngles.y;
    }

    void Update() {
        transform.rotation = Quaternion.Euler((Mathf.Sin(Time.realtimeSinceStartup)/2 * rotate_amount) + initialAngle,transform.eulerAngles.y,transform.eulerAngles.z);
    }
}
