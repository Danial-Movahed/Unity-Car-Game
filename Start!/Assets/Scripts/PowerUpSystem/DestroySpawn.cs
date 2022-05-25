using UnityEngine;
using UnityEngine.UI;

public class DestroySpawn : MonoBehaviour
{
    public Vector3[] spawnPoints;
    private int maxPowerUps = 1;
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
            Destroy(gameObject);
            if (collisionInfo.gameObject.transform.parent.name == configscript.selfName)
            {
                int random = Random.Range(1, maxPowerUps);
                PowerUpImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Maps/PowerUps/PowerUps/" + random);
                PowerUpImage.SetActive(true);
                GameObject.Find("UsePowerUp").GetComponent<UsePowerUp>().currentPowerUp = random;
            }
            int availableSpawnPoint = 0;
            foreach (Vector3 spawnPoint in spawnPoints)
            {
                if (Physics.CheckSphere(spawnPoint, 0.001f))
                {
                    availableSpawnPoint++;
                }
            }
            Debug.Log(availableSpawnPoint);
            if (availableSpawnPoint < 4)
            {
                int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                while (Physics.CheckSphere(spawnPoints[randomSpawnPoint], 1))
                {
                    randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                }
                GameObject obj = Instantiate(Resources.Load("Maps/PowerUps/PowerUp" + configscript.mapSelector.ToString()), spawnPoints[randomSpawnPoint], Quaternion.identity) as GameObject;
            }
        }

    }
}