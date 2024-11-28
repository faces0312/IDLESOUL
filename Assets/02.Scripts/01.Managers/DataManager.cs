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
    //public string    LastLogin;         // Unity�� JsonUtolity�� DataTime �ڷ��� ���� ����
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
        userData.Nickname = "���� ������";
        userData.Level = 10;
        userData.Experience = 100052;
        userData.Gold = 999999;
        userData.Diamonds = 9999;
        userData.PlayTimeInSeconds = 72000;
        //userData.Inventory = inventory;



        ////ToDoCode : �����ҵ����͸� ���� �ڵ� �׽�Ʈ �κ�

        strBuilder.Clear();
        //strBuilder.Append(csvSaveFilePath);
        //CsvController.Write(strBuilder.ToString(), saveData);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.S)) //������ ���̺�
        {
            JsonController.SaveUserData(userData, jsonUserDataPath);
        }
        else if (Input.GetKeyDown(KeyCode.L)) //������ �ε�
        {
            userData = JsonController.LoadUserData(jsonUserDataPath);

            Debug.Log($"�ε��� �г���  : {userData.Nickname}");
            Debug.Log($"�ε��� ����  : {userData.Level}");
        }
        else if (Input.GetKeyDown(KeyCode.D)) // ������ ����
        {
            userData.Nickname += "1";
            userData.Level += 10;

            Debug.Log($"�г��� ���� : {userData.Nickname}");
            Debug.Log($"���� ���� : {userData.Level}");
        }
    }

}