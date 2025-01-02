using UnityEngine;
using System;
using ScottGarland;
using System.Collections.Generic;

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
    public int curStageID; //���� �������� Stage ID�� ����
    public int ClearStageCycle; //���� Ŭ������ Stage ���� Ƚ���� ����
    public float StageModifier; //���� Ŭ������ stage�� ������ ���� ���� ����

    public List<UserItemData> GainItem = new List<UserItemData>();
    public List<UserSoulData> GainSoul = new List<UserSoulData>();

    public Stat stat;
    public UserData(UserDB userDB)
    {
        GainItem = userDB.GainItem;
        GainSoul = userDB.GainSoul;

        UID = userDB.key;
        NickName = userDB.Nickname;
        Level = userDB.Level;
        Gold = userDB.Gold;
        Diamonds = userDB.Diamonds;
        PlayTimeInSeconds = userDB.PlayTimeInSeconds;
        curStageID = userDB.CurStageID;
        ClearStageCycle = userDB.ClearStageCycle;
        StageModifier = userDB.StageModifier;

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

[System.Serializable]
public class UserItemData
{
    public int ID;                  // �������� ID
    public int Level;               // ������ ��ȭ ����
    public int GainStack;           // �������� ���� ����

    public UserItemData(Item item)
    {
        ID = item.ItemStat.iD;
        Level = item.UpgradeLevel;
        GainStack = item.stack;
    }

}

[System.Serializable]
public class UserSoulData
{
    public int ID;                  // �ҿ��� ID
    public int Level;               // �ҿ��� ��ȭ ����
    public int PassiveSkillLevel;   // �ҿ� �нú� ��ų ����
    public int DefaultSkillLevel;   // �ҿ� ��Ƽ�� ��ų ����
    public int UltimateSkillLevel;   // �ҿ� �ñر� ��ų ����

    public UserSoulData(Soul soul)
    {
        ID = soul.ID;
        Level = soul.level;
        PassiveSkillLevel = soul.Skills[(int)SkillType.Passive].level;
        DefaultSkillLevel = soul.Skills[(int)SkillType.Default].level;
        UltimateSkillLevel = soul.Skills[(int)SkillType.Ultimate].level;
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

        switch (status)
        {
            case Status.Hp:
                userData.stat.MaxHealthLevel = statHandler.CurrentStat.MaxHealthLevel;
                userData.stat.maxHealth = statHandler.CurrentStat.maxHealth;
                break;
            case Status.Atk:
                userData.stat.AtkLevel = statHandler.CurrentStat.AtkLevel;
                userData.stat.atk = statHandler.CurrentStat.atk;
                break;
            case Status.Def:
                userData.stat.DefLevel = statHandler.CurrentStat.DefLevel;
                userData.stat.def = statHandler.CurrentStat.def;
                break;
            case Status.ReduceDmg:
                userData.stat.ReduceDamageLevel = statHandler.CurrentStat.ReduceDamageLevel;
                userData.stat.reduceDamage = statHandler.CurrentStat.reduceDamage;
                break;
            case Status.CritChance:
                userData.stat.CriticalRateLevel = statHandler.CurrentStat.CriticalRateLevel;
                userData.stat.critChance = statHandler.CurrentStat.critChance;
                break;
            case Status.CritDmg:
                userData.stat.CriticalDamageLevel = statHandler.CurrentStat.CriticalDamageLevel;
                userData.stat.critDamage = statHandler.CurrentStat.critDamage;
                break;
        }
    }

    public void RegisterSoul()
    {
        //PlayerSouls.RegisterSoul("Ŭ�󸮽�", new SoulMagician(11000));
        //PlayerSouls.RegisterSoul("�÷ڸ�", new SoulKnight(11001));
        //PlayerSouls.RegisterSoul("�翣", new SoulArcher(11002));
        //PlayerSouls.EquipSoul("Ŭ�󸮽�", 0);
        //PlayerSouls.EquipSoul("�÷ڸ�", 1);
        //PlayerSouls.EquipSoul("�翣", 2);
        //OnUpdateSoulStats?.Invoke();    // ���� �� �нú� ������Ʈ

        //PlayerSouls.SpawnSoul(0);

        PlayerSouls.RegisterSoul("Ŭ�󸮽�", new SoulMagician(11000));
        //PlayerSouls.RegisterSoul("�÷ڸ�", new SoulKnight(11001));
        //PlayerSouls.RegisterSoul("�翣", new SoulArcher(11002));
        PlayerSouls.EquipSoul("Ŭ�󸮽�", 0);
        //PlayerSouls.EquipSoul("�÷ڸ�", 1);
        //PlayerSouls.EquipSoul("�翣", 2);
        OnUpdateSoulStats?.Invoke();    // ���� �� �нú� ������Ʈ

        PlayerSouls.SpawnSoul(0);

        //Debug : �ҿ� ������ ���� üũ ���� 
        //Soul[] playerSouls = GameManager.Instance.player.PlayerSouls.SoulSlot;

        
        //foreach (SoulSlot slot in PlayerSouls.SoulInventory.SoulInventoryModel.Slots)
        //{
        //    if(slot.soul != null) //�κ��丮�� �ҿ��� ���� ������ ���������Ϳ� ���� 
        //    {
        //        GameManager.Instance.player.UserData.GainSoul.Add(new UserSoulData(slot.soul));
        //    }
        //}
    }

    public void Initialize()
    {
        baseHpSystem.IsDead = false;
        isAuto = false;

        //MVP ���Ŀ� ���ľ� �� �÷��̾� ���� ������ �ҷ����� ����
        //Model(UserData) ����
        if (DataManager.Instance.LoadUserData() == null)
        {
            //�����ϱ� , �⺻ �ɷ�ġ�� ���� 
            userData = new UserData(DataManager.Instance.UserDB.GetByKey(TestID));  
        }
        else
        {
            //�̾��ϱ�
            userData = new UserData(DataManager.Instance.LoadUserData());
        }

        statHandler = new StatHandler(StatType.Player,0,userData);

        //Controller(FSM ����)
        playerStateMachine.ChangeState(playerStateMachine.IdleState);

        //Debug �ҿ� �ʱ�ȭ -> �����丵 �� ȣ�� ���� ������ �ʿ�
        //RegisterSoul();

        Debug.Log("Player ���� �Ϸ�!!");

        DataManager.Instance.SaveUserData(userData);
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
