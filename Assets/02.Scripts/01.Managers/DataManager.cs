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
    public int Level;
    public int Experience;
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

    private readonly string jsonUserDataPath = Application.dataPath + "/userdata.json";

    private StringBuilder strBuilder = new StringBuilder();
    public JsonController JsonController = new JsonController();

    //Inventory inventory = new Inventory();

    private EnemyDBLoader enemyDB;
    private ItemDBLoader itemDB;
    private SellItemDBLoader sellItemDB;
    private StageDBLoader stageDB;

    public EnemyDBLoader EnemyDB { get => enemyDB; }
    public ItemDBLoader ItemDB { get => itemDB; }
    public SellItemDBLoader SellItemDB { get => sellItemDB; }
    public StageDBLoader StageDB { get => stageDB; }

    Inventory inventory;
    UserData userData = new UserData();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        enemyDB = new EnemyDBLoader(jsonEnemyDBPath);
        itemDB = new ItemDBLoader(jsonItemDBPath);
        sellItemDB = new SellItemDBLoader(jsonSellItemDBPath);
        stageDB = new StageDBLoader(jsonStageDBPath);
        
        inventory.Items = new List<ItemDB>();
        inventory.Items.Add(itemDB.GetByKey(1000));
        inventory.Items.Add(itemDB.GetByKey(2000));

        userData.UserID = 12345;
        userData.Nickname = "지존 감자탕";
        userData.Level = 10;
        userData.Experience = 100052;
        userData.Gold = 999999;
        userData.Diamonds = 9999;
        userData.PlayTimeInSeconds = 72000;
        //userData.Inventory = inventory;



        ////ToDoCode : 저장할데이터를 쓰는 코드 테스트 부분

        strBuilder.Clear();
        //strBuilder.Append(csvSaveFilePath);
        //CsvController.Write(strBuilder.ToString(), saveData);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.S)) //데이터 세이브
        {
            JsonController.SaveUserData(userData, jsonUserDataPath);
        }
        else if (Input.GetKeyDown(KeyCode.L)) //데이터 로드
        {
            userData = JsonController.LoadUserData(jsonUserDataPath);

            Debug.Log($"로드한 닉네임  : {userData.Nickname}");
            Debug.Log($"로드한 레벨  : {userData.Level}");
        }
        else if (Input.GetKeyDown(KeyCode.D)) // 데이터 갱신
        {
            userData.Nickname += "1";
            userData.Level += 10;

            Debug.Log($"닉네임 변경 : {userData.Nickname}");
            Debug.Log($"레벨 변경 : {userData.Level}");
        }
    }

}