using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ScottGarland;
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
    private CapsuleCollider collider;
    //public Rigidbody rb;
    public AnimatorHashData animatorHashData;
    public Animator animator;

    [Header("State Machine")]
    public EnemyStateMachine stateMachine;

    [Header("CurrentStats")]
    public Slider healthBar;
    public float currentHealth;

    public StatHandler StatHandler { get => base.statHandler; set => base.statHandler = value; }

    public event Action OnDieEvent;
    public static event Action OnEventTargetRemove;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        animatorHashData = new AnimatorHashData();
        animatorHashData.Initialize();
        stateMachine = new EnemyStateMachine(this);
        target = GameManager.Instance.player.gameObject;

        //HP ∞‘¿”
    }
    protected virtual void Start()
    {
        Initialize();
        stateMachine.Initialize();
        HpUpdate();
        //Debug.Log(statHandler.CurrentStat.health);
    }

    public void Initialize()
    {
        collider.enabled = true;
        OnEventTargetRemove += GameManager.Instance.player.targetSearch.TargetClear;

        statHandler = new StatHandler(StatType.Enemy, enemyDB.key);

        statHandler.CurrentStat.iD = enemyDB.key;
        statHandler.CurrentStat.health = new  BigInteger((long)enemyDB.Health);
        statHandler.CurrentStat.maxHealth = new BigInteger((long)enemyDB.Health);
        statHandler.CurrentStat.atk = new BigInteger((long)enemyDB.Attack);
        statHandler.CurrentStat.def = new BigInteger((long)enemyDB.Defence);
        statHandler.CurrentStat.moveSpeed = enemyDB.MoveSpeed;
        statHandler.CurrentStat.atkSpeed = enemyDB.AttackSpeed;
        statHandler.CurrentStat.critChance = enemyDB.CritChance;
        statHandler.CurrentStat.critDamage = enemyDB.CritDamage;
    }

    public override void TakeDamage(float damage)
    {
        if (statHandler.CurrentStat.health <= 0)
            return;

        base.TakeDamage(damage);
        HpUpdate();
        if (statHandler.CurrentStat.health <= 0)
        {
            GameManager.Instance.enemies.Remove(gameObject);
            OnEventTargetRemove?.Invoke();
            OnDieEvent?.Invoke();
            collider.enabled = false;
            animator.SetTrigger("Die");
        }
    }

    public void HpUpdate()
    {
        float maxHelth = BigInteger.ToInt32(statHandler.CurrentStat.maxHealth);
        float curHelth = BigInteger.ToInt32(statHandler.CurrentStat.health);
        healthBar.value = curHelth/maxHelth;

        if (healthBar.value < 1 && healthBar.value > 0)
            healthBar.gameObject.SetActive(true);
        else
            healthBar.gameObject.SetActive(false);
    }

    public void Die()
    {
        //OnEventTargetRemove?.Invoke();
        //OnDieEvent?.Invoke();
        gameObject.SetActive(false);
        Debug.Log($"{gameObject.name} ªÁ∏¡!!");
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
