using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCleaning : MonoBehaviour
{
    public GameObject Player;
    private PlayerController playerControllerScript;
    private float offset = 0.22f;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.horizontalInput > 0)
        {
            transform.position = new Vector2(Player.transform.position.x + offset, Player.transform.position.y);
        }
        else if (playerControllerScript.horizontalInput < 0)
        {
            transform.position = new Vector2(Player.transform.position.x - offset, Player.transform.position.y);
        }
    }

    private void OnTriggerStay2D(Collider2D dust)
    {
        if (playerControllerScript.cleaningInput != 0 && dust.gameObject.tag == "Dusts")
        {
            Destroy(dust.gameObject);
        }
    }
}
