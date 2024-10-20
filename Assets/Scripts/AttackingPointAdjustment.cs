using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPointAdjustment : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerControllerScript;
    public GameObject wave;
    public float cooldown;
    private bool canFire = true;
    private float currentCooldownTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        targetShooting();
    }

    void targetShooting()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (!canFire)
        {
            currentCooldownTime += Time.deltaTime;
            if(currentCooldownTime >= cooldown)
            {
                canFire = true;
                currentCooldownTime = 0;
            }
        }

        if(playerControllerScript.attackingInput != 0 && canFire)
        {
            Instantiate(wave, transform.position, Quaternion.identity);
            canFire = false;
        }
    }
}
