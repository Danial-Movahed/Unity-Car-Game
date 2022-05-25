using UnityEngine;

public class NameSaver : MonoBehaviour
{
    public string ExcludeName = "";
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != ExcludeName)
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().angularVelocity.x, 20, other.gameObject.GetComponent<Rigidbody>().angularVelocity.z);
            Destroy(gameObject);
        }
    }
}