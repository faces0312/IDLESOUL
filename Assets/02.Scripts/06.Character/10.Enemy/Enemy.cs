using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged
}

public abstract class Enemy : BaseCharacter
{
    [Header("Data")]
    public AttackType attackType;
    public EnemyDB enemyDB;

    [Header("References")]
    public GameObject target;
    public Rigidbody rb;
    public AnimatorHashData animatorHashData;
    //public Animator animator;
    public GameObject bulletTest;

    [Header("State Machine")]
    public EnemyStateMachine stateMachine;

    [Header("CurrentStats")]
    private float currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stateMachine = new EnemyStateMachine(this);
        animatorHashData = new AnimatorHashData();
        //animator = GetComponent<Animator>();
        //target = GameManager.Instance.player;
        currentHealth = enemyDB.Health;
        target = GameObject.Find("Player");
        stateMachine.Initialize();
        enemyDB.Distance = 7f;
    }

    public virtual void Update()
    {
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdateState();
    }

    public override void Attack()
    {
        /*GameObject bulletInstance = Instantiate(bulletTest, transform.position, Quaternion.Euler(Vector3.zero));
        BulletTest monsterBullet = bulletInstance.GetComponent<BulletTest>();
        monsterBullet.attack = enemyDB.Attack;
        monsterBullet.knockbackPower = enemyDB.KnockBackPower;

        Vector3 playerProjection = new Vector3(target.transform.position.x, target.transform.position.y, 0.0f);
        Vector3 selfProjection = new Vector3(transform.position.x, transform.position.y, 0.0f);

        monsterBullet.direction = (playerProjection - selfProjection).normalized;*/
    }
    private void AttackDelay()
    {
        /*if (attackSpeedTmp > 0)
        {
            attackSpeedTmp -= Time.deltaTime;
        }
        else
        {
            attackSpeedTmp = enemyDB.AttackSpeed;
            Debug.Log("공격 실행");
            Attack();
        }*/
    }

    public override void Move()
    {
        /*float distanceTmp = Vector3.Distance(transform.position, target.transform.position);
        if (distanceTmp > enemyDB.Distance)
        {
            Vector3 targetVelocity = direction * enemyDB.MoveSpeed;
            rb.velocity = targetVelocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
            AttackDelay();
        }*/
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
