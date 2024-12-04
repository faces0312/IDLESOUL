using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;

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
    private readonly string jsonAchieveDBPath = "JSON/AchieveDB";
    private readonly string jsonUserDBPath = "JSON/UserDB";

    private readonly string jsonUserDataPath = "/userdata.json";

    private StringBuilder strBuilder = new StringBuilder();
    public JsonController JsonController = new JsonController();

    private EnemyDBLoader enemyDB;
    private ItemDBLoader itemDB;
    private SellItemDBLoader sellItemDB;
    private StageDBLoader stageDB;
    private SoulDBLoader soulDB;
    private SkillDBLoader skillDB;
    private UserDBLoader userDB;
    private AchieveDBLoader achieveDB;

    public EnemyDBLoader EnemyDB { get => enemyDB; }
    public ItemDBLoader ItemDB { get => itemDB; }
    public SellItemDBLoader SellItemDB { get => sellItemDB; }
    public StageDBLoader StageDB { get => stageDB; }
    public SoulDBLoader SoulDB { get => soulDB; }
    public SkillDBLoader SkillDB { get => skillDB; }
    public UserDBLoader UserDB { get => userDB;}
    public AchieveDBLoader AchieveDB { get => achieveDB; }

    private UserDB userData;
    public UserDB UserData { get => userData; set => userData = value; }

    private Inventory inventory = new Inventory();

    //public static event Action<UserDB> OnEventSaveUserData;
    //public static event Action OnEventLoadUserData;

    protected override void Awake()
    {
        base.Awake();

        enemyDB = new EnemyDBLoader(jsonEnemyDBPath);
        itemDB = new ItemDBLoader(jsonItemDBPath);
        sellItemDB = new SellItemDBLoader(jsonSellItemDBPath);
        stageDB = new StageDBLoader(jsonStageDBPath);
        soulDB = new SoulDBLoader(jsonSoulDBPath);
        skillDB = new SkillDBLoader(jsonSkillDBPath);
        userDB = new UserDBLoader(jsonUserDBPath);
        achieveDB = new AchieveDBLoader(jsonAchieveDBPath);

        inventory.Items = new List<ItemDB>();
        inventory.Items.Add(itemDB.GetByKey(1000));
        inventory.Items.Add(itemDB.GetByKey(2000));



        //OnEventSaveUserData += SaveUserData;
        //OnEventLoadUserData += LoadUserData;
    }

    public void SaveUserData(UserData userData) // 유저 데이터 세이브
    {
        UserDB saveData = userDB.GetByKey(userData.UID);
        saveData.JsonDataConvert(userData);

        JsonController.SaveUserData(saveData, jsonUserDataPath);
    }

    public UserDB LoadUserData() // 유저 데이터 로드
    {
        userData = JsonController.LoadUserData(jsonUserDataPath);
       
        return userData;
    }
}