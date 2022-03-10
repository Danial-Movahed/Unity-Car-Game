using UnityEngine;
using System.Collections;

public class scr_camera : MonoBehaviour {

    public float rotate_amount;
    float initialAngle;

	// Use this for initialization
	void Start () {
	initialAngle = transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
transform.rotation = Quaternion.Euler(transform.eulerAngles.x, (Mathf.Sin(Time.realtimeSinceStartup)/2 * rotate_amount) + initialAngle, transform.eulerAngles.z);
    }
}
