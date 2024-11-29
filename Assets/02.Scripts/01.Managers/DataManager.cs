using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class UserData
{
    public int UserID;
    public string Nickname;
    public Stat Status;
    public int Gold;
    public int Diamonds;
    public int PlayTimeInSeconds;
    //public string    LastLogin;         // Unity의 JsonUtolity는 DataTime 자료형 지원 안함
    //public Inventory Inventory;
    public ClearStageData ClearStage;
}

[Serializable]
public class Inventory
{
    public List<ItemDB> Items;
    //public Equipment Equipment;
}

[Serializable]
public class ClearStageData
{


}

public class DataManager : SingletonDDOL<DataManager>
{
    private readonly string jsonItemDBPath = "JSON/ItemDB";
    private readonly string jsonSellItemDBPath = "JSON/SellItemDB";
    private readonly string jsonEnemyDBPath = "JSON/EnemyDB";
    private readonly string jsonStageDBPath = "JSON/StageDB";
    private readonly string jsonSoulDBPath = "JSON/SoulDB";
    private readonly string jsonSkillDBPath = "JSON/SkillDB";

    private readonly string jsonUserDataPath = Application.dataPath + "/userdata.json";

    private StringBuilder strBuilder = new StringBuilder();
    public JsonController JsonController = new JsonController();

    private EnemyDBLoader enemyDB;
    private ItemDBLoader itemDB;
    private SellItemDBLoader sellItemDB;
    private StageDBLoader stageDB;
    private SoulDBLoader soulDB;
    private SkillDBLoader skillDB;

    public EnemyDBLoader EnemyDB { get => enemyDB; }
    public ItemDBLoader ItemDB { get => itemDB; }
    public SellItemDBLoader SellItemDB { get => sellItemDB; }
    public StageDBLoader StageDB { get => stageDB; }
    public SoulDBLoader SoulDB { get => soulDB; }
    public SkillDBLoader SkillDB { get => skillDB; }

    private Inventory inventory = new Inventory();
    private UserData userData = new UserData();

    public UserData UserData { get => userData;}

    public static event Action<UserData> OnEventSaveUserData;
    public static event Action OnEventLoadUserData;

    protected override void Awake()
    {
        base.Awake();

        OnEventSaveUserData += SaveUserData;
        OnEventLoadUserData += LoadUserData;
    }

    private void Start()
    {
        enemyDB = new EnemyDBLoader(jsonEnemyDBPath);
        itemDB = new ItemDBLoader(jsonItemDBPath);
        sellItemDB = new SellItemDBLoader(jsonSellItemDBPath);
        stageDB = new StageDBLoader(jsonStageDBPath);
        soulDB = new SoulDBLoader(jsonSoulDBPath);
        skillDB = new SkillDBLoader(jsonSkillDBPath);

        inventory.Items = new List<ItemDB>();
        inventory.Items.Add(itemDB.GetByKey(1000));
        inventory.Items.Add(itemDB.GetByKey(2000));

        userData.UserID = 12345;
        userData.Nickname = "지존 감자탕";
        userData.Status = new Stat();
        userData.Status.level = 1;
        userData.Gold = 999999;
        userData.Diamonds = 9999;
        userData.PlayTimeInSeconds = 72000;
        //userData.Inventory = inventory;


    }

    private void SaveUserData(UserData userData) // 유저 데이터 세이브
    {
        JsonController.SaveUserData(userData, jsonUserDataPath);
    }

    private void LoadUserData() // 유저 데이터 로드
    {
        userData = JsonController.LoadUserData(jsonUserDataPath);
    }

    //Debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) // 데이터 갱신
        {
            userData.Nickname += "_WA";
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            OnEventSaveUserData?.Invoke(userData);
        }
        else if( Input.GetKeyDown(KeyCode.L))
        {
            OnEventLoadUserData?.Invoke();
        }
    }

}