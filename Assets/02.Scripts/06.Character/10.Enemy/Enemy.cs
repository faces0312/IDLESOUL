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
    public Animator animator;
    public GameObject bulletTest;

    [Header("State Machine")]
    public EnemyStateMachine stateMachine;

    [Header("CurrentStats")]
    public float currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        animatorHashData = new AnimatorHashData();
        animatorHashData.Initialize();
        //target = GameManager.Instance.player;
        stateMachine = new EnemyStateMachine(this);
        target = GameObject.Find("Player");
        enemyDB.Distance = 1f;

        stateMachine.Initialize();
    }
    private void Start()
    {
        Debug.Log(enemyDB.Health);
        currentHealth = enemyDB.Health;
    }

    public override void TakeDamage(float damage)
    {
    
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
    }
    private void AttackDelay()
    {
    }

    public override void Move()
    {
    }
}
