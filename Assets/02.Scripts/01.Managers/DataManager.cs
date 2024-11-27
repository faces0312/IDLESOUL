using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

[System.Serializable]
public class ItemData
{
    public int      ID;
    public string   Name;
    public string   Type;
    public string   Rairty;
    public string   Descripton;
    public float    Attack;
    public bool     AttackPercent;
    public float    Defence;
    public bool     DefencePercent;
    public float    Health;
    public bool     HealthPercent;
    public float    CritChance;
    public bool     CritChancePercent;
    public float    CritDamage;
    public bool     CritDamagePercent;
    public string   Effect;
    public int      Cost;
    public int      StackMaxCount;
}

[System.Serializable]
public class EnemyData
{
    public int      ID;
    public string   Name;
    public string   Descripton;
    public List<int> DropItemID;
    public int      DropGold;
    public float    Attack;
    public float    AttackSpeed;
    public float    Defence;
    public float    MoveSpeed;
    public float    Health;
    public float    CritChance;
    public float    CritDamage;
}

[Serializable]
public class UserData
{
    public int       UserID;
    public string    Nickname;
    public int       Level;
    public int       Experience;
    public int       Gold;
    public int       Diamonds;
    public int       PlayTimeInSeconds;
    //public string    LastLogin;         // Unity�� JsonUtolity�� DataTime �ڷ��� ���� ����
    public Inventory Inventory;
}

[Serializable]
public class Inventory
{
    public List<ItemData> Items;
    //public Equipment Equipment;
}

public class DataManager : SingletonDDOL<DataManager>
{
    private readonly string csvItemDBPath = "CSV/ItemDB";
    private readonly string csvEnemyDBPath = "CSV/EnemyDB";

    private readonly string jsonUserDataPath = Application.dataPath + "/userdata.json";

    private StringBuilder strBuilder = new StringBuilder();
    public CSVController CsvController = new CSVController();
    public JsonController JsonController = new JsonController();

    private Dictionary<int, ItemData> itemDB = new Dictionary<int, ItemData>();
    private Dictionary<int, EnemyData> enemyDB = new Dictionary<int, EnemyData>();

    public Dictionary<int, ItemData> ItemDB { get => itemDB; }
    public Dictionary<int, EnemyData> EnemyDB { get => enemyDB; }

    Inventory inventory = new Inventory();
    UserData userData = new UserData();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        itemDB = CsvController.ItemCSVRead(csvItemDBPath);
        enemyDB = CsvController.EnemyCSVRead(csvEnemyDBPath);

        inventory.Items = new List<ItemData>();
        inventory.Items.Add(ItemDB[1000]);
        inventory.Items.Add(ItemDB[2000]);

        userData.UserID = 12345;
        userData.Nickname = "���� ������";
        userData.Level = 10;
        userData.Experience = 100052;
        userData.Gold = 999999;
        userData.Diamonds = 9999;
        userData.PlayTimeInSeconds = 72000;
        userData.Inventory = inventory;

    

        ////ToDoCode : �����ҵ����͸� ���� �ڵ� �׽�Ʈ �κ�

        strBuilder.Clear();
        //strBuilder.Append(csvSaveFilePath);
        //CsvController.Write(strBuilder.ToString(), saveData);
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.S)) //������ ���̺�
        {
            JsonController.SaveUserData(userData, jsonUserDataPath);
        }
        else if (Input.GetKeyDown(KeyCode.L)) //������ �ε�
        {
            userData = JsonController.LoadUserData(jsonUserDataPath);

            Debug.Log($"�ε��� �г���  : {userData.Nickname}");
            Debug.Log($"�ε��� ����  : {userData.Level}");
        }
        else if(Input.GetKeyDown(KeyCode.D)) // ������ ����
        {
            userData.Nickname += "1";
            userData.Level += 10;

            Debug.Log($"�г��� ���� : {userData.Nickname}");
            Debug.Log($"���� ���� : {userData.Level}");
        }
    }

}