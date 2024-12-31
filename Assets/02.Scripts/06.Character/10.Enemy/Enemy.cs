using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ScottGarland;
using UnityEngine.UI;
using Spine;

public enum AttackType
{
    Melee,
    Ranged
}

public abstract class Enemy : BaseCharacter
{
    [Header("Data")]
    public AttackType attackType;
    public GameObject slash;
    public EnemyDB enemyDB;

    [Header("References")]
    public GameObject target;
    public CapsuleCollider collider;
    //public Rigidbody rb;
    public AnimatorHashData animatorHashData;
    public Animator animator;

    [Header("State Machine")]
    public EnemyStateMachine stateMachine;

    [Header("CurrentStats")]
    public Slider healthBar;
    public float maxHealth;
    public float currentHealth;

    //public StatHandler StatHandler { get => base.statHandler; set => base.statHandler = value; }

    public event Action OnDieEvent;
    public static event Action OnEventTargetRemove;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        animatorHashData = new AnimatorHashData();
        stateMachine = new EnemyStateMachine(this);
        OnDieEvent += StageManager.Instance.StageProgressModel.AddCurEnemyCount;
        //HP 게임
    }

    public virtual void Initialize()
    {
        animatorHashData.Initialize();
        collider.enabled = true;
        OnEventTargetRemove += GameManager.Instance.player.targetSearch.TargetClear;

        statHandler = new StatHandler(StatType.Enemy, enemyDB.key);

        float increaseStat = StageManager.Instance.CurStageData.CurStageModifier;
        int chapter = StageManager.Instance.CurStageData.ChapterNum;

        //현재스테이지에 따른 스텟 증가량을 적용 받음 
        statHandler.CurrentStat.iD = enemyDB.key;
        statHandler.CurrentStat.health = new BigInteger((long)(enemyDB.Health * increaseStat * chapter));
        statHandler.CurrentStat.maxHealth = new BigInteger((long)(enemyDB.Health * increaseStat * chapter));
        statHandler.CurrentStat.atk = new BigInteger((long)(enemyDB.Attack * increaseStat * chapter));
        statHandler.CurrentStat.def = new BigInteger((long)(enemyDB.Defence * increaseStat * chapter));
        statHandler.CurrentStat.moveSpeed = enemyDB.MoveSpeed;
        statHandler.CurrentStat.atkSpeed = enemyDB.AttackSpeed;
        statHandler.CurrentStat.critChance = enemyDB.CritChance * increaseStat * chapter;
        statHandler.CurrentStat.critDamage = enemyDB.CritDamage * increaseStat * chapter;
        stateMachine.Initialize();
        HpUpdate();

        maxHealth = BigInteger.ToInt32(statHandler.CurrentStat.maxHealth);
        currentHealth = BigInteger.ToInt32(statHandler.CurrentStat.health);
    }

    public virtual void BossAppear()
    {
        Debug.Log("Base BossAppear called");
    }

    public override void TakeDamage(BigInteger damage)
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
        };

        // 데미지 폰트를 적용하는 부분
        // TODO : 크리티컬 데미지 시, 변화를 준다
        var dmgFont = ObjectPoolManager.Instance.GetPool(Const.DAMAGE_FONT_KEY, Const.DAMAGE_FONT_POOL_KEY).GetObject();
        dmgFont.SetActive(true);
        dmgFont.transform.position = transform.position;
        dmgFont.transform.rotation = Quaternion.identity;
        dmgFont.GetComponent<DamageFont>().SetDamage(Owner.Enemy, damage);
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
        if (attackType == AttackType.Melee)
            slash.SetActive(false);
        gameObject.SetActive(false);
        AchieveEvent achieveEvent = new AchieveEvent(Enums.AchievementType.KillMonster,Enums.ActionType.Kill, 1);
        EventManager.Instance.Publish<AchieveEvent>(Enums.Channel.Achievement, achieveEvent);

    }

    public void Update()
    {
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdateState();
    }

    private void LateUpdate()
    {
        if (target.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (target.transform.position.x - transform.position.x > 0) // 플레이어가 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    public float GetAttackPower()
    {
        return BigInteger.ToInt32(statHandler.CurrentStat.atk);
    }

    private void OnDisable()
    {
        if (attackType == AttackType.Melee)
            slash.SetActive(false);
    }
}
