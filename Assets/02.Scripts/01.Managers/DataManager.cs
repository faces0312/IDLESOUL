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

        enemyDB = new EnemyDBLoader(Const.JsonEnemyDBPath);
        itemDB = new ItemDBLoader(Const.JsonItemDBPath);
        sellItemDB = new SellItemDBLoader(Const.JsonSellItemDBPath);
        stageDB = new StageDBLoader(Const.JsonStageDBPath);
        soulDB = new SoulDBLoader(Const.JsonSoulDBPath);
        skillDB = new SkillDBLoader(Const.JsonSkillDBPath);
        userDB = new UserDBLoader(Const.JsonUserDBPath);
        achieveDB = new AchieveDBLoader(Const.JsonAchieveDBPath);

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

        JsonController.SaveUserData(saveData, Const.JsonUserDataPath);
    }

    public UserDB LoadUserData() // 유저 데이터 로드
    {
        userData = JsonController.LoadUserData(Const.JsonUserDataPath);
       
        return userData;
    }
}