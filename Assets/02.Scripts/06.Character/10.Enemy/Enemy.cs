using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{    
    private GameObject target;
    private Vector3 direction;
   [SerializeField] private float distance;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //target = GameManager.Instance.player;
        target = GameObject.Find("Player");
    }

    public override void Attack()
    {
    }

    public override void Move()
    {
        /*Vector3 targetVelocity = direction * enemyData.MoveSpeed * 5;
        rb.velocity = targetVelocity;*/

        /*float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > enemyData.Distance)
        {
            Vector3 targetVelocity = direction * enemyData.MoveSpeed;
            rb.velocity = targetVelocity;
        }*/
    }


    private void FixedUpdate()
    {
        direction = (target.transform.position - transform.position).normalized;
        Move();
    }
}
