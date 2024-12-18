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

    [Header("Debuh : 근접(true) , 원거리(false)")]
    public bool TestDefaultAttackType;

    [Header("Data")]
    private UserData userData;

    [Header("References")]
    public TargetSearch targetSearch;
    private PlayerAnimationController playerAnimationController;
    private PlayerSouls playerSouls;
    public GameObject CamarePivot;
    public InventoryModel Inventory; //플레이어 인벤토리 데이터

    [Header("State Machine")]
    public PlayerStateMachine playerStateMachine;

    [Header("EquipData")]
    private Item equipItem; //장착 아이템 여부 

    [Header("Auto")]
    public bool isAuto;//오토버튼이 활성화됐는지
    public bool isJoyStick;//조이스틱으로 조종 중인지

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
        //FSM 초기 상태 설정 (Idle)
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
        PlayerSouls.RegisterSoul("클라리스", new SoulMagician(11000));
        PlayerSouls.RegisterSoul("플뢰르", new SoulKnight(11001));
        PlayerSouls.RegisterSoul("루엔", new SoulArcher(11002));
        PlayerSouls.EquipSoul("클라리스", 0);
        PlayerSouls.EquipSoul("플뢰르", 1);
        PlayerSouls.EquipSoul("루엔", 2);
        OnUpdateSoulStats?.Invoke();    // 착용 시 패시브 업데이트

        PlayerSouls.SpawnSoul(0);

        //StatViewUpdate();
    }

    public void Initialize()
    {
        baseHpSystem.IsDead = false; 

        //Model(UserData) 세팅
        if (DataManager.Instance.LoadUserData() == null)
        {
            //새로하기 , 기본 능력치를 제공 
            userData = new UserData(DataManager.Instance.UserDB.GetByKey(TestID));
            DataManager.Instance.SaveUserData(userData);
        }
        else
        {
            //이어하기
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

        //Controller(FSM 세팅)
        playerStateMachine.ChangeState(playerStateMachine.IdleState);

        //Debug 소울 초기화 -> 리팩토링 및 호출 시점 재조정 필요
        //RegisterSoul();

        Debug.Log("Player 세팅 완료!!");
    }

    public override void TakeDamage(float damage)
    {
        baseHpSystem.TakeDamage(damage, statHandler);
        UIManager.Instance.ShowUI("PlayerHPDisplay");

        // 데미지 폰트를 적용하는 부분
        // TODO : 크리티컬 데미지 시, 변화를 준다
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

            rb.velocity = Vector3.zero; //캐릭터 이동되지않게 속도를 0으로 수정
            rb.isKinematic = true;
            GameManager.Instance.GameOver();
            enabled = false;
        }
    }

    public void Respwan()
    {
        //ToDoCode : 플레이어가 죽을경우 재세팅하는 함수
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

        if (Input.GetKeyDown(KeyCode.D)) // 데이터 갱신
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
