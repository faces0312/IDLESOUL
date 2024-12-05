using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Spine.Unity;
using System;

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
        Exp = userDB.exp;
        MaxExp = userDB.MaxExp;

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
    public Rigidbody rb;
    private PlayerAnimationController playerAnimationController;
    private PlayerSouls playerSouls;
    public PlayerAnimationController PlayerAnimationController { get => playerAnimationController; }
    public PlayerSouls PlayerSouls { get => playerSouls; }

    [Header("State Machine")]
    private PlayerStateMachine playerStateMachine;

    [Header("Projectile Object Pool")]
    [SerializeField] private GameObject playerProjectile;

    private readonly int INITIAL_POOL_SIZE = 100;

    public StatHandler StatHandler { get => base.statHandler; set => base.statHandler = value; }
    public UserData UserData { get => userData;  }

    public Action OnUpdateSoulStats;


    private void Awake()
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

        ObjectPool playerProjectilePool = new ObjectPool(Utils.POOL_KEY_PLAYERPROJECTILE, INITIAL_POOL_SIZE, "Prefabs/Player/Attack/EnergyBolt");
        ObjectPoolManager.Instance.AddPool("playerProjectile", playerProjectilePool);
    }

    public void Initialize()
    {
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
    }

    public override void Attack()
    {

    }

    public override void Move()
    {

    }

    private void Update()
    {
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

    private void OnTriggerEnter(Collider other)
    {

    }
}
