using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class AiBot : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Animator ani;
    private PlayerController playerControllerScript;
    private float direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        playerControllerScript = player.GetComponent<PlayerController>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        FollowingPlayer();
        AnimationBot();
    }

    void FollowingPlayer()
    {
        agent.SetDestination(player.transform.position);

        if (playerControllerScript.death)
        {
            agent.isStopped = true;
        }
    }

    void AnimationBot()
    {
        Vector2 desired = new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.z).normalized;
        Vector2 right = new Vector2(transform.right.x, transform.right.z);
        direction = Vector2.Dot(right, desired);
        ani.SetFloat("horiX", direction);
    }

    private void OnCollisionEnter2D(Collision2D player)
    {
        if(player.gameObject.tag == "Player")
        {
            ani.SetTrigger("attacking");
        }
    }

    void death()
    {
        Destroy(gameObject);
    }
}
