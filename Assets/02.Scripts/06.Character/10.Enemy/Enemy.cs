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
    public float attack;
    public float maxHealth;
    public float currentHealth;

    public string health;

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
        //stateMachine = new EnemyStateMachine(this); //호출시점이 이상해서 Init으로 위치 이동 
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
        statHandler.CurrentStat.maxHealth = new BigInteger((long)(enemyDB.Health * StageManager.Instance.MainStageModifier * StageManager.Instance.Chapter));
        statHandler.CurrentStat.health = statHandler.CurrentStat.maxHealth;
        statHandler.CurrentStat.atk = new BigInteger((long)(enemyDB.Attack * StageManager.Instance.MainStageModifier * StageManager.Instance.Chapter));
        statHandler.CurrentStat.def = new BigInteger((long)(enemyDB.Defence * StageManager.Instance.MainStageModifier * StageManager.Instance.Chapter));
        statHandler.CurrentStat.moveSpeed = enemyDB.MoveSpeed;
        statHandler.CurrentStat.atkSpeed = enemyDB.AttackSpeed;
        //statHandler.CurrentStat.critChance = enemyDB.CritChance * StageManager.Instance.MainStageModifier * StageManager.Instance.Chapter;
        //statHandler.CurrentStat.critDamage = enemyDB.CritDamage * StageManager.Instance.MainStageModifier * StageManager.Instance.Chapter;

        //호출시점이 이상해서 Init으로 위치 이동완료,
        if (stateMachine == null)
        {
            stateMachine = new EnemyStateMachine(this);
        }
        stateMachine.Initialize();
        HpUpdate();
    }

    public virtual void BossAppear()
    {
        Debug.Log("Base BossAppear called");
    }

    public virtual void SetSkillCharging(bool charging)
    {
        Debug.Log("Base SkillCharging called");
    }

    public override void TakeDamage(BigInteger damage)
    {
        if (statHandler.CurrentStat.health <= 0)
            return;

        if (StatHandler.CurrentStat.def  > damage) //unsinged 자료형이기에 
        {
            damage = 0;
        }
        else
        {
            damage -= StatHandler.CurrentStat.def;//해당 Enemy의 방어력 계수 적용 
        }


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
        long maxHelth = BigInteger.ToInt64(statHandler.CurrentStat.maxHealth);
        long curHelth = BigInteger.ToInt64(statHandler.CurrentStat.health);

        // ulong을 double로 캐스팅하여 소수점 값을 계산
        double healthValue = (double)curHelth / maxHelth;

        healthBar.value = (float)healthValue;

        if (healthBar.value < 1 && healthBar.value > 0)
            healthBar.gameObject.SetActive(true);
        else
            healthBar.gameObject.SetActive(false);
    }

    public virtual void Die()
    {
        if (attackType == AttackType.Melee)
            slash.SetActive(false);
        gameObject.SetActive(false);
        AchieveEvent achieveEvent = new AchieveEvent(Enums.AchievementType.Kill, Enums.ActionType.Monster, 1);
        EventManager.Instance.Publish<AchieveEvent>(Enums.Channel.Achievement, achieveEvent);

    }

    public virtual void Update()
    {
        stateMachine.Update();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.FixedUpdateState();
    }

    public virtual void LateUpdate()
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
        return BigInteger.ToUInt64(statHandler.CurrentStat.atk);
    }

    public virtual void OnDisable()
    {
        if (attackType == AttackType.Melee)
            slash.SetActive(false);
    }
}
