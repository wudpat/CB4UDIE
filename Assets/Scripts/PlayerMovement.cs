using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float acceleration = 2.5f;
    private float verticalInput;
    private float horizontalInput;
    public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.up * verticalInput * acceleration * Time.deltaTime);
        transform.Translate(Vector2.right * horizontalInput * acceleration * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            acceleration = 5.0f;
            ani.SetBool("shift", true);
        }
        else
        {
            acceleration = 2.5f;
            ani.SetBool("shift", false);
        }
        Animation();
    }
    
    void Animation()
    {
        if (horizontalInput < 0 && horizontalInput != 0)
        {
            ani.SetBool("move", true);
            ani.SetBool("right", false);
        }
        else if (horizontalInput > 0 && horizontalInput != 0)
        {
            ani.SetBool("right", true);
            ani.SetBool("move", true);
        }
        else if (horizontalInput != 0 || horizontalInput != 0)
        {
            ani.SetBool("move", true);
        }
        else
        {
            ani.SetBool("move", false);
        }
    }
}
