using UnityEngine;
using UnityEngine.UI;

public class DestroySpawn : MonoBehaviour
{
    private int maxPowerUps = 9;
    private Config configscript;
    private GameObject PowerUpImage;
    void Start()
    {
        PowerUpImage = GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().PowerUpImage;
        configscript = GameObject.Find("ConfigStart").GetComponent<Config>();
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        Debug.Log("Collision with powerup. " + collisionInfo.gameObject.transform.parent.name);
        if(GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp != 0)
            return;
        if (collisionInfo.gameObject.name == "Collision")
        {
            if (collisionInfo.gameObject.transform.parent.name == configscript.selfName)
            {
                int random = Random.Range(1, maxPowerUps+1);
                while(random == GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().lastPowerUp)
                {
                    Debug.Log("Random is repeating.");
                    random = Random.Range(1, maxPowerUps+1);
                }
                PowerUpImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Maps/PowerUps/PowerUps/" + random);
                PowerUpImage.SetActive(true);
                GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp = random;
            }
        }

    }
}