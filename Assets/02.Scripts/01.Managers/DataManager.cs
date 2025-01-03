using System;
using System.Text;
using UnityEngine;

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
    private StatUpgradeDBLoader statUpgradeDB;
    private ExchangeDBLoader exchangeDB;
    private TutorialDBLoader tutorialDB;

    public EnemyDBLoader EnemyDB { get => enemyDB; }
    public ItemDBLoader ItemDB { get => itemDB; }
    public SellItemDBLoader SellItemDB { get => sellItemDB; }
    public StageDBLoader StageDB { get => stageDB; }
    public SoulDBLoader SoulDB { get => soulDB; }
    public SkillDBLoader SkillDB { get => skillDB; }
    public UserDBLoader UserDB { get => userDB;}
    public AchieveDBLoader AchieveDB { get => achieveDB; }
    public StatUpgradeDBLoader StatUpgradeDB { get => statUpgradeDB; }

    private UserDB userData;
    public UserDB UserData { get => userData; set => userData = value; }
    public ExchangeDBLoader ExchangeDB { get => exchangeDB; }
    public TutorialDBLoader TutorialDB { get => tutorialDB; }

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
        statUpgradeDB = new StatUpgradeDBLoader(Const.JsonStatUpgradeDBPath);
        exchangeDB = new ExchangeDBLoader(Const.JsonExchangeDBPath);
        tutorialDB = new TutorialDBLoader(Const.JsonTutorialDBPath);
    }

    public void Init()
    {
        Debug.Log("DataManager Init 완료!!");
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