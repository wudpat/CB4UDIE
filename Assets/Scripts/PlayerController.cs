using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerController : MonoBehaviour
{
    private float acceleration = 2.5f;
    private float verticalInput;
    public float horizontalInput;
    private Animator ani;
    public float cleaningInput;
    public float attackingInput;
    public bool death;
    public string inRoom;
    private bool freezed;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Cleaning();
        Attacking();
    }

    void Movement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.up * verticalInput * acceleration * Time.deltaTime);
        transform.Translate(Vector2.right * horizontalInput * acceleration * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && freezed == false)
        {
            acceleration = 5.0f;
            if (horizontalInput > 0)
            {
                ani.SetFloat("horiX", 1.5f);
            }
            else if (horizontalInput < 0)
            {
                ani.SetFloat("horiX", -1.5f);
            }
        }
        else if(freezed == false)
        {
            acceleration = 2.5f;
            ani.SetFloat("horiX", horizontalInput);
        }

        if (verticalInput != 0)
        {
            ani.SetFloat("vertX", 1);
        }
        else
        {
            ani.SetFloat("vertX", 0);
        }
        
        // Adjusting onRight parameter for playing attacking/cleaning animation properly
        if(horizontalInput > 0)
        {
            ani.SetFloat("onRight", 1);
        }
        else if(horizontalInput < 0)
        {
            ani.SetFloat("onRight", -1);
        }

        // Applying onRight to horiX for idle direction
        if(ani.GetFloat("onRight") == 1 && horizontalInput == 0)
        {
            ani.SetFloat("horiX", 0.5f);
        }
        else if(ani.GetFloat("onRight") == -1 && horizontalInput == 0)
        {
            ani.SetFloat("horiX", -0.5f);
        }
    }

    void Cleaning()
    {
        if(attackingInput == 0)
        {
            cleaningInput = Input.GetAxis("RightClick");

            // Animation
            if (cleaningInput != 0)
            {
                acceleration = 0;
                freezed = true;
                ani.SetBool("cleaning", true);
            }
            else
            {
                if(acceleration == 0)
                {
                    acceleration = 2.5f;
                }
                freezed = false;
                ani.SetBool("cleaning", false);
            }
        }
    }

    void Attacking()
    {
        if(cleaningInput == 0)
        {
            attackingInput = Input.GetAxis("LeftClick");

            if (attackingInput != 0)
            {
                ani.SetBool("attacking", true);
            }
            else
            {
                ani.SetBool("attacking", false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D enemy)
    {
        if(enemy.gameObject.tag == "Enemies")
        {
            ani.SetTrigger("killed");
            death = true;
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D room)
    {
        if(room.name == "EgyptianRoom")
        {
            inRoom = "Egypt";
        }
        else if(room.name == "EuropeanRoom")
        {
            inRoom = "Eu";
        }
        else if(room.name == "ChineseRoom")
        {
            inRoom = "Cn";
        }
        else if(room.name == "CommonRoomSouthWest")
        {
            inRoom = "CommonSW";
        }
        else if(room.name == "CommonRoomSouth")
        {
            inRoom = "CommonS";
        }
    }
}
