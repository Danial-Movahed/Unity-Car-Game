using UnityEngine;

public class NameSaver : MonoBehaviour
{
    public string ExcludeName = "";
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != ExcludeName)
        {
            Debug.Log(other.gameObject.name);
            if (GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp != -1)
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().angularVelocity.x, 20, other.gameObject.GetComponent<Rigidbody>().angularVelocity.z);
            else
            {
                GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().lastPowerUp = 4;
                GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp = 0;
                GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().PowerUpImage.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
}