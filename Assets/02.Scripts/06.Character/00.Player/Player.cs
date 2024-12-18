    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Spine.Unity;
using System;
using UnityEditorInternal;
using ScottGarland;

public class UserData
{
    public int UID;
    public string NickName;
    public int Gold;
    public int Diamonds;
    public int PlayTimeInSeconds;
    public int Level;
    public int Exp;
    public int MaxExp;

    public Stat stat;
    public UserData(UserDB userDB)
    {
        UID = userDB.key;
        NickName = userDB.Nickname;
        Level = userDB.Level;
        Gold = userDB.Gold;
        Diamonds = userDB.Diamonds;
        PlayTimeInSeconds = userDB.PlayTimeInSeconds;

        stat = new Stat();
        stat.iD = UID;

        stat.health = userDB.Health;
        stat.maxHealth = userDB.MaxHealth;
        stat.atk = userDB.Atk;
        stat.def = userDB.Def;

        stat.moveSpeed = userDB.moveSpeed;
        stat.atkSpeed = userDB.atkSpeed;

        stat.reduceDamage = userDB.ReduceDamage;

        stat.critDamage = userDB.CriticalDamage;
        stat.critChance = userDB.CriticalRate;
        stat.coolDown = userDB.coolDown;
    }

}

public class Player : BaseCharacter
{
    private readonly int TestID = 12345678;

    [Header("Debuh : ����(true) , ���Ÿ�(false)")]
    public bool TestDefaultAttackType;

    [Header("Data")]
    private UserData userData;

    [Header("References")]
    public TargetSearch targetSearch;
    private PlayerAnimationController playerAnimationController;
    private PlayerSouls playerSouls;
    public GameObject CamarePivot;
    public InventoryModel Inventory; //�÷��̾� �κ��丮 ������

    [Header("State Machine")]
    public PlayerStateMachine playerStateMachine;

    [Header("EquipData")]
    private Item equipItem; //���� ������ ���� 

    [Header("Auto")]
    public bool isAuto;//�����ư�� Ȱ��ȭ�ƴ���
    public bool isJoyStick;//���̽�ƽ���� ���� ������

    public PlayerAnimationController PlayerAnimationController { get => playerAnimationController; }
    public PlayerSouls PlayerSouls { get => playerSouls; }
    public StatHandler StatHandler { get => base.statHandler; set => base.statHandler = value; }
    public UserData UserData { get => userData;  }
    public Item IsEquipItem { get => equipItem;  }

    public Action OnUpdateSoulStats;

    public void EquipItem(Item item)
    {
        if(equipItem != null)
        {
            equipItem.equip = false;
            StatHandler.UnEquipItem(equipItem.ItemStat);
        }
        equipItem = item;
        item.equip = true;

        StatHandler.EquipItem(item.ItemStat);

    }

    public void DisEquipItem()
    {
        equipItem.equip = false;
        StatHandler.UnEquipItem(equipItem.ItemStat);
        equipItem = null;
    }

    protected override void Awake()
    {
        base.Awake();

        if (targetSearch == null)
        {
            targetSearch = GetComponent<TargetSearch>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if(playerAnimationController == null)
        {
            playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
            playerAnimationController.Initialize();
        }
        if (playerSouls == null)
        {
            playerSouls = GetComponent<PlayerSouls>();
        }
        //FSM �ʱ� ���� ���� (Idle)
        playerStateMachine = new PlayerStateMachine(this);

        GameManager.Instance.player = this;

        Initialize();
    }

    public void LevelUp(int level, Status status)
    {
        statHandler.LevelUp(level, status);
    }

    public void RegisterSoul()
    {
        PlayerSouls.RegisterSoul("Ŭ�󸮽�", new SoulMagician(11000));
        PlayerSouls.RegisterSoul("�÷ڸ�", new SoulKnight(11001));
        PlayerSouls.RegisterSoul("�翣", new SoulArcher(11002));
        PlayerSouls.EquipSoul("Ŭ�󸮽�", 0);
        PlayerSouls.EquipSoul("�÷ڸ�", 1);
        PlayerSouls.EquipSoul("�翣", 2);
        OnUpdateSoulStats?.Invoke();    // ���� �� �нú� ������Ʈ

        PlayerSouls.SpawnSoul(0);

        //StatViewUpdate();
    }

    public void Initialize()
    {
        baseHpSystem.IsDead = false; 

        //Model(UserData) ����
        if (DataManager.Instance.LoadUserData() == null)
        {
            //�����ϱ� , �⺻ �ɷ�ġ�� ���� 
            userData = new UserData(DataManager.Instance.UserDB.GetByKey(TestID));
            DataManager.Instance.SaveUserData(userData);
        }
        else
        {
            //�̾��ϱ�
            userData = new UserData(DataManager.Instance.LoadUserData());
        }

        statHandler = new StatHandler(StatType.Player);
        statHandler.CurrentStat.iD = userData.UID;
        statHandler.CurrentStat.health = userData.stat.health;
        statHandler.CurrentStat.maxHealth = userData.stat.maxHealth;
        statHandler.CurrentStat.atk = userData.stat.atk;
        statHandler.CurrentStat.def = userData.stat.def;
        statHandler.CurrentStat.moveSpeed = userData.stat.moveSpeed;
        statHandler.CurrentStat.atkSpeed = userData.stat.atkSpeed;
        statHandler.CurrentStat.reduceDamage = userData.stat.reduceDamage;
        statHandler.CurrentStat.critChance = userData.stat.critChance;
        statHandler.CurrentStat.critDamage = userData.stat.critDamage;
        statHandler.CurrentStat.coolDown = userData.stat.coolDown;

        //Controller(FSM ����)
        playerStateMachine.ChangeState(playerStateMachine.IdleState);

        //Debug �ҿ� �ʱ�ȭ -> �����丵 �� ȣ�� ���� ������ �ʿ�
        //RegisterSoul();

        Debug.Log("Player ���� �Ϸ�!!");
    }

    public override void TakeDamage(float damage)
    {
        baseHpSystem.TakeDamage(damage, statHandler);
        UIManager.Instance.ShowUI("PlayerHPDisplay");

        // ������ ��Ʈ�� �����ϴ� �κ�
        // TODO : ũ��Ƽ�� ������ ��, ��ȭ�� �ش�
        var dmgFont = ObjectPoolManager.Instance.GetPool(Const.DAMAGE_FONT_KEY, Const.DAMAGE_FONT_POOL_KEY).GetObject();
        dmgFont.SetActive(true);
        dmgFont.transform.position = transform.position;
        dmgFont.transform.rotation = Quaternion.identity;
        dmgFont.GetComponent<DamageFont>().SetDamage(Owner.Player, new BigInteger((int)damage));

        if (statHandler.CurrentStat.health <= 0)
        {
            Die();
        }
    }

    [ContextMenu("PlayerDie")]
    public void Die()
    {
        if (!baseHpSystem.IsDead)
        {
            baseHpSystem.IsDead = true;
            Debug.Log("Player Die!!! ");
            string animName = PlayerAnimationController.DeathAnimationName;
            PlayerAnimationController.spineAnimationState.SetAnimation(0, animName, false);

            rb.velocity = Vector3.zero; //ĳ���� �̵������ʰ� �ӵ��� 0���� ����
            rb.isKinematic = true;
            GameManager.Instance.GameOver();
            enabled = false;
        }
    }

    public void Respwan()
    {
        //ToDoCode : �÷��̾ ������� �缼���ϴ� �Լ�
        statHandler.CurrentStat.health = statHandler.CurrentStat.maxHealth;
        transform.position = Vector3.up;
        rb.isKinematic = false;
        enabled = true;
        baseHpSystem.IsDead = false;
        UIManager.Instance.ShowUI("PlayerHPDisplay");
    }

    public override void Attack()
    {

    }

    public override void Move()
    {

    }

    private void Update()
    {
        if (isJoyStick == false)
            playerStateMachine.Update();

        if (Input.GetKeyDown(KeyCode.D)) // ������ ����
        {
            userData.Level++;
            userData.stat.atk = userData.Level * userData.stat.atk;
            userData.stat.def = userData.Level * userData.stat.def;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DataManager.Instance.SaveUserData(userData);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            DataManager.Instance.LoadUserData();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            targetSearch.TargetClear();
            Debug.Log("Player Chase Target Reset");
        }
    }

    private void FixedUpdate()
    {
        playerStateMachine.FixedUpdateState();
    }
}
