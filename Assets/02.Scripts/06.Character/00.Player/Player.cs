using UnityEngine;
using System;
using ScottGarland;

public class UserData
{
    public int UID;
    public string NickName;
    public int Gold;
    public int Diamonds;
    public int PlayTimeInSeconds;
    public int Level; // ���� ����
    public int Exp; // ���� ���� ����ġ
    public int MaxExp; // ���� �ְ� ����ġ 

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

        stat.MaxHealthLevel = userDB.MaxHealthLevel;
        stat.AtkLevel = userDB.AtkLevel;
        stat.DefLevel = userDB.DefLevel;
        stat.ReduceDamageLevel = userDB.ReduceDamageLevel;
        stat.CriticalRateLevel = userDB.CriticalRateLevel;
        stat.CriticalDamageLevel = userDB.CriticalDamageLevel;
    }

}

public class Player : BaseCharacter
{
    private readonly int TestID = 12345678;

    //Debug - �׽�Ʈ�� ����,���Ÿ� ���� Ÿ�� ���� ���� ���߿� ����Ÿ�� �����Ұ� 
    public bool DefaultAttackType = false; //true : �������� , false : ���Ÿ� ����

    [Header("Data")]
    private UserData userData;

    [Header("References")]
    public TargetSearch targetSearch;
    private PlayerAnimationController playerAnimationController;
    public GameObject CamarePivot;
    private PlayerSouls playerSouls;
    public InventoryModel Inventory; //�÷��̾� �κ��丮 ������

    [Header("State Machine")]
    public PlayerStateMachine playerStateMachine;

    [Header("EquipData")]
    private Item equipItem; //���� ������ ���� 

    [Header("Auto")]
    public bool isAuto;//�����ư�� Ȱ��ȭ�ƴ���
    public bool isJoyStick;//���̽�ƽ���� ���� ������
    public bool isController;//��Ʈ�ѷ��� ���� ������

    public PlayerAnimationController PlayerAnimationController { get => playerAnimationController; }
    public PlayerSouls PlayerSouls { get => playerSouls; }
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
        UIManager.Instance.ShowUI<UIPlayerHPDisplayController>();
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

        //MVP ���Ŀ� ���ľ� �� �÷��̾� ���� ������ �ҷ����� ����
        ////Model(UserData) ����
        //if (DataManager.Instance.LoadUserData() == null)
        //{
        //    //�����ϱ� , �⺻ �ɷ�ġ�� ���� 
        //    userData = new UserData(DataManager.Instance.UserDB.GetByKey(TestID));
        //    DataManager.Instance.SaveUserData(userData);
        //}
        //else
        //{
        //    //�̾��ϱ�
        //    userData = new UserData(DataManager.Instance.LoadUserData());
        //}

        //�����ϱ� , �⺻ �ɷ�ġ�� ���� 
        userData = new UserData(DataManager.Instance.UserDB.GetByKey(TestID));
        DataManager.Instance.SaveUserData(userData);

        statHandler = new StatHandler(StatType.Player,0,userData);

        //Controller(FSM ����)
        playerStateMachine.ChangeState(playerStateMachine.IdleState);

        //Debug �ҿ� �ʱ�ȭ -> �����丵 �� ȣ�� ���� ������ �ʿ�
        //RegisterSoul();

        Debug.Log("Player ���� �Ϸ�!!");
    }

    public override void TakeDamage(BigInteger damage)
    {
        baseHpSystem.TakeDamage(damage, statHandler);
        UIManager.Instance.ShowUI<UIPlayerHPDisplayController>();

        // ������ ��Ʈ�� �����ϴ� �κ�
        // TODO : ũ��Ƽ�� ������ ��, ��ȭ�� �ش�
        var dmgFont = ObjectPoolManager.Instance.GetPool(Const.DAMAGE_FONT_KEY, Const.DAMAGE_FONT_POOL_KEY).GetObject();
        dmgFont.SetActive(true);
        dmgFont.transform.position = transform.position;
        dmgFont.transform.rotation = Quaternion.identity;
        dmgFont.GetComponent<DamageFont>().SetDamage(Owner.Player, damage);

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
        targetSearch.TargetClear();
        UIManager.Instance.ShowUI<UIPlayerHPDisplayController>();
    }

    private void Update()
    {
        if (isJoyStick == false && isController == false)
            playerStateMachine.Update();
    }

    private void FixedUpdate()
    {
        playerStateMachine.FixedUpdateState();
    }


}
