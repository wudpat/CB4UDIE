using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveAttacking : MonoBehaviour
{
    private GameObject attackingTargetObject;
    private AttackingPointAdjustment attackingAdjustmentScript;
    private Rigidbody2D rb2d;
    private Vector3 mousePos;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        attackingTargetObject = GameObject.Find("AttackingAdjustment");
        attackingAdjustmentScript = attackingTargetObject.GetComponent<AttackingPointAdjustment>();
        rb2d = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb2d.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ + 180);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "Enemies")
        {
            enemy.gameObject.GetComponent<Animator>().SetTrigger("death");
            Destroy(gameObject);
        }

        else if (enemy.gameObject.tag != "Player" && enemy.gameObject.tag != "Ignored")
        {
            Destroy(gameObject);
        }
    }
}
