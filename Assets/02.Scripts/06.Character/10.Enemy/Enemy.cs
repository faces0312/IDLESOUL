using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public EnemyDB enemyDB;
    private GameObject target;
    private Vector3 direction;

    private float distance;
    private float attackSpeedTmp;
    private Rigidbody rb;

    private LayerMask layerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //target = GameManager.Instance.player;
        target = GameObject.Find("Player");
        distance = 1f;
        attackSpeedTmp = enemyDB.AttackSpeed;
    }

    public override void Attack()
    {
    }
    private void AttackDelay()
    {
        if (attackSpeedTmp > 0)
        {
            attackSpeedTmp -= Time.deltaTime;
        }
        else
        {
            attackSpeedTmp = enemyDB.AttackSpeed;
            Debug.Log("공격 실행");
            Attack();
        }
    }

    public override void Move()
    {
        /*Vector3 targetVelocity = direction * enemyDB.MoveSpeed * 5;
        rb.velocity = targetVelocity;*/

        float distanceTmp = Vector3.Distance(transform.position, target.transform.position);
        if (distanceTmp > enemyDB.Distance)
        {
            Vector3 targetVelocity = direction * enemyDB.MoveSpeed;
            rb.velocity = targetVelocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
            AttackDelay();
        }
    }


    private void FixedUpdate()
    {
        direction = (target.transform.position - transform.position).normalized;
        Move();
    }

    /*//공격 로직
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ITakeDamageAble damageable = collision.gameObject.GetComponent<ITakeDamageAble>();
            //TODO :: 무적시간이 아닐때에도 조건에 추가해야됨
            if (damageable != null)
            {
                damageable.TakeDamage(enemyDB.Attack);
                Vector3 directionKnockBack = collision.gameObject.transform.position - transform.position;
                damageable.TakeKnockBack(directionKnockBack, enemyDB.KnockBackPower);
            }
        }
    }*/
}
