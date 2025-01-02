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
    public int Level; // 계정 레벨
    public int Exp; // 계정 현재 경험치
    public int MaxExp; // 계정 최고 경험치 
    public int curStageID; //현재 진행중인 Stage ID를 저장
    public int ClearStageCycle; //현재 클리어한 Stage 루프 횟수를 지정
    public float StageModifier; //여태 클리어한 stage의 배율의 곱셈 값을 저장

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
    public int ID;                  // 아이템의 ID
    public int Level;               // 아이템 강화 레벨
    public int GainStack;           // 아이템의 소유 갯수

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
    public int ID;                  // 소울의 ID
    public int Level;               // 소울의 강화 레벨
    public int PassiveSkillLevel;   // 소울 패시브 스킬 레벨
    public int DefaultSkillLevel;   // 소울 액티브 스킬 레벨
    public int UltimateSkillLevel;   // 소울 궁극기 스킬 레벨

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

    //Debug - 테스트용 근접,원거리 공격 타입 선택 변수 나중에 변수타입 변경할것 
    public bool DefaultAttackType = false; //true : 근접공격 , false : 원거리 공격

    [Header("Data")]
    private UserData userData;

    [Header("References")]
    public TargetSearch targetSearch;
    private PlayerAnimationController playerAnimationController;
    public GameObject CamarePivot;
    private PlayerSouls playerSouls;
    public InventoryModel Inventory; //플레이어 인벤토리 데이터

    [Header("State Machine")]
    public PlayerStateMachine playerStateMachine;

    [Header("EquipData")]
    private Item equipItem; //장착 아이템 여부 

    [Header("Auto")]
    public bool isAuto;//오토버튼이 활성화됐는지
    public bool isJoyStick;//조이스틱으로 조종 중인지
    public bool isController;//컨트롤러로 조종 중인지

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
        //FSM 초기 상태 설정 (Idle)
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
        //PlayerSouls.RegisterSoul("클라리스", new SoulMagician(11000));
        //PlayerSouls.RegisterSoul("플뢰르", new SoulKnight(11001));
        //PlayerSouls.RegisterSoul("루엔", new SoulArcher(11002));
        //PlayerSouls.EquipSoul("클라리스", 0);
        //PlayerSouls.EquipSoul("플뢰르", 1);
        //PlayerSouls.EquipSoul("루엔", 2);
        //OnUpdateSoulStats?.Invoke();    // 착용 시 패시브 업데이트

        //PlayerSouls.SpawnSoul(0);

        PlayerSouls.RegisterSoul("클라리스", new SoulMagician(11000));
        //PlayerSouls.RegisterSoul("플뢰르", new SoulKnight(11001));
        //PlayerSouls.RegisterSoul("루엔", new SoulArcher(11002));
        PlayerSouls.EquipSoul("클라리스", 0);
        //PlayerSouls.EquipSoul("플뢰르", 1);
        //PlayerSouls.EquipSoul("루엔", 2);
        OnUpdateSoulStats?.Invoke();    // 착용 시 패시브 업데이트

        PlayerSouls.SpawnSoul(0);

        //Debug : 소울 데이터 저장 체크 목적 
        //Soul[] playerSouls = GameManager.Instance.player.PlayerSouls.SoulSlot;

        
        //foreach (SoulSlot slot in PlayerSouls.SoulInventory.SoulInventoryModel.Slots)
        //{
        //    if(slot.soul != null) //인벤토리에 소울이 비지 않으면 유저데이터에 저장 
        //    {
        //        GameManager.Instance.player.UserData.GainSoul.Add(new UserSoulData(slot.soul));
        //    }
        //}
    }

    public void Initialize()
    {
        baseHpSystem.IsDead = false;
        isAuto = false;

        //MVP 이후에 고쳐야 될 플레이어 유저 데이터 불러오기 로직
        //Model(UserData) 세팅
        if (DataManager.Instance.LoadUserData() == null)
        {
            //새로하기 , 기본 능력치를 제공 
            userData = new UserData(DataManager.Instance.UserDB.GetByKey(TestID));  
        }
        else
        {
            //이어하기
            userData = new UserData(DataManager.Instance.LoadUserData());
        }

        statHandler = new StatHandler(StatType.Player,0,userData);

        //Controller(FSM 세팅)
        playerStateMachine.ChangeState(playerStateMachine.IdleState);

        //Debug 소울 초기화 -> 리팩토링 및 호출 시점 재조정 필요
        //RegisterSoul();

        Debug.Log("Player 세팅 완료!!");

        DataManager.Instance.SaveUserData(userData);
    }

    public override void TakeDamage(BigInteger damage)
    {
        baseHpSystem.TakeDamage(damage, statHandler);
        UIManager.Instance.ShowUI<UIPlayerHPDisplayController>();

        // 데미지 폰트를 적용하는 부분
        // TODO : 크리티컬 데미지 시, 변화를 준다
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
