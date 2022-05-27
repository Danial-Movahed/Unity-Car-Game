using UnityEngine;
using UnityEngine.UI;

public class DestroySpawn : MonoBehaviour
{
    public Vector3[] spawnPoints;
    private int maxPowerUps = 5;
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
                PowerUpImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Maps/PowerUps/PowerUps/" + random);
                PowerUpImage.SetActive(true);
                GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp = random;
            }
        }

    }
}