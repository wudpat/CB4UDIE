using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawningPoints;
    private PlayerController playerControllerScript;
    private int spawnIndex;
    public float cooldown;
    private bool canSpawn;
    private float currentCooldownTime;
    private float timeWithoutEnemies = -15;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        currentCooldownTime = timeWithoutEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.inRoom == "Egypt")
        {
            spawnIndex = Random.Range(1, 3);
        }
        else if(playerControllerScript.inRoom == "Eu")
        {
            spawnIndex = 0;
        }
        else if(playerControllerScript.inRoom == "Cn")
        {
            spawnIndex = Random.Range(0, 2);
        }
        else if(playerControllerScript.inRoom == "CommonSW")
        {
            spawnIndex = 0;
        }
        else if(playerControllerScript.inRoom == "CommonS")
        {
            spawnIndex = Random.Range(0, 2);
        }

        int randomIndex = Random.Range(0, enemies.Length);

        if (!canSpawn)
        {
            currentCooldownTime += Time.deltaTime;
            if(currentCooldownTime >= cooldown)
            {
                canSpawn = true;
                currentCooldownTime = 0;
            }
        }

        if (canSpawn)
        {
            Instantiate(enemies[randomIndex], spawningPoints[spawnIndex]);
            canSpawn = false;
        }
        
    }
}
