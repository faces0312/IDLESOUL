using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public EnemyData enemyData;
    
    public GameObject target;
    Vector3 direction;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //target = GameManager.Instance.player;
        target = GameObject.Find("Player");


        Debug.Log(enemyData.ID);
        Debug.Log(enemyData.Name);
        Debug.Log(enemyData.MoveSpeed);
    }

    public override void Attack()
    {
    }

    public override void Move()
    {
        Vector3 targetVelocity = direction * enemyData.MoveSpeed * 5;
        rb.velocity = targetVelocity;

        /*float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > enemyData.distance)
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
