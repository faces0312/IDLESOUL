using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public EnemyDB enemyDB;
    private GameObject target;
    private Vector3 direction;

    private Rigidbody rb;

    private float attackSpeedTmp;
    public GameObject bulletTest;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //target = GameManager.Instance.player;
        target = GameObject.Find("Player");
        attackSpeedTmp = enemyDB.AttackSpeed;
        //���Ƿ� �ۼ�
        enemyDB.Distance = 7f;
    }

    public override void Attack()
    {
        GameObject bulletInstance = Instantiate(bulletTest, transform.position, Quaternion.Euler(Vector3.zero));
        BulletTest monsterBullet = bulletInstance.GetComponent<BulletTest>();
        monsterBullet.attack = enemyDB.Attack;
        monsterBullet.knockbackPower = enemyDB.KnockBackPower;

        Vector3 playerProjection = new Vector3(target.transform.position.x, target.transform.position.y, 0.0f);
        Vector3 selfProjection = new Vector3(transform.position.x, transform.position.y, 0.0f);

        monsterBullet.direction = (playerProjection - selfProjection).normalized;
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
            Debug.Log("���� ����");
            Attack();
        }
    }

    public override void Move()
    {
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

    /*//���� ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ITakeDamageAble damageable = collision.gameObject.GetComponent<ITakeDamageAble>();
            //TODO :: �����ð��� �ƴҶ����� ���ǿ� �߰��ؾߵ�
            if (damageable != null)
            {
                damageable.TakeDamage(enemyDB.Attack);
                Vector3 directionKnockBack = collision.gameObject.transform.position - transform.position;
                damageable.TakeKnockBack(directionKnockBack, enemyDB.KnockBackPower);
            }
        }
    }*/
}
