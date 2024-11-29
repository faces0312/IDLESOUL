using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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
    //public TestStat Tstat;
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

    [Header("Data")]
    private UserData userData;

    [Header("State Machine")]
    private PlayerStateMachine playerStateMachine;

    [Header("Controller")]
    public TargetSearch targetSearch;
    public Rigidbody rb;

    public StatHandler StatHandler { get => base.statHandler; }
    public UserData UserData { get => userData;  }

    private void Awake()
    {
        if (targetSearch == null)
        {
            targetSearch = GetComponent<TargetSearch>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        //FSM 초기 상태 설정 (Idle)
        playerStateMachine = new PlayerStateMachine(this);
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

        //statHandler = new StatHandler(StatType.Player);
        //statHandler.CurrentStat.iD = userData.UID;
        //statHandler.CurrentStat.health = userData.stat.health;
        //statHandler.CurrentStat.maxHealth = userData.stat.maxHealth;
        //statHandler.CurrentStat.atk = userData.stat.atk;
        //statHandler.CurrentStat.def = userData.stat.def;
        //statHandler.CurrentStat.moveSpeed = userData.stat.moveSpeed;
        //statHandler.CurrentStat.atkSpeed = userData.stat.atkSpeed;
        //statHandler.CurrentStat.reduceDamage = userData.stat.reduceDamage;
        //statHandler.CurrentStat.critChance = userData.stat.critChance;
        //statHandler.CurrentStat.critDamage = userData.stat.critDamage;
        //statHandler.CurrentStat.coolDown = userData.stat.coolDown;

        //Controller(FSM 세팅)
        playerStateMachine.ChangeState(playerStateMachine.IdleState);
    }

    public override void TakeDamage(float damage)
    {

    }

    public override void TakeKnockBack(Vector3 direction, float force)
    {

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
}
