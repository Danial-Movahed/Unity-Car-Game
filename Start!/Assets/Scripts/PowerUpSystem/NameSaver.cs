using UnityEngine;

public class NameSaver : MonoBehaviour
{
    public string ExcludeName = "";
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != ExcludeName)
        {
            Debug.Log(other.gameObject.name);
            if(!other.gameObject.GetComponent<SheildEnabler>())
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().angularVelocity.x, 20, other.gameObject.GetComponent<Rigidbody>().angularVelocity.z);
            else
            {
                other.gameObject.GetComponent<SheildEnabler>().ifEnabled = false;
                GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp = 0;
                GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().PowerUpImage.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
}