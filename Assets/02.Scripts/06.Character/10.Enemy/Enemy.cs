using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
    public Slider healthBar;
    public float currentHealth;

    public StatHandler StatHandler { get => base.statHandler; set => base.statHandler = value; }

    public event Action OnDieEvent;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        animatorHashData = new AnimatorHashData();
        animatorHashData.Initialize();
        //target = GameManager.Instance.player;
        stateMachine = new EnemyStateMachine(this);
        target = GameObject.Find("Player");
        enemyDB.Distance = 5f;

        stateMachine.Initialize();
    }
    private void Start()
    {
        Initialize();
        Debug.Log(statHandler.CurrentStat.health);
    }

    public void Initialize()
    {
        statHandler = new StatHandler(StatType.Enemy, enemyDB.key);

        statHandler.CurrentStat.iD = enemyDB.key;
        statHandler.CurrentStat.health = new ScottGarland.BigInteger((long)enemyDB.Health);
        statHandler.CurrentStat.maxHealth = new ScottGarland.BigInteger((long)enemyDB.Health);
        statHandler.CurrentStat.atk = new ScottGarland.BigInteger((long)enemyDB.Attack);
        statHandler.CurrentStat.def = new ScottGarland.BigInteger((long)enemyDB.Defence);
        statHandler.CurrentStat.moveSpeed = enemyDB.MoveSpeed;
        statHandler.CurrentStat.atkSpeed = enemyDB.AttackSpeed;
        statHandler.CurrentStat.critChance = enemyDB.CritChance;
        statHandler.CurrentStat.critDamage = enemyDB.CritDamage;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
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
