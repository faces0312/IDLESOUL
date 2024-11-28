using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel : UIModel
{
    public UserData testUser;
    public Dictionary<string, List<testItem>> ItemGachaList;
    public Dictionary<string, List<testSoul>> SoulGachaList;
    public int GachaCount; //�Ŀ� ������ �Ŵ����� ���� �ʿ�
    public int GachaPrice;
    private string ItemPickUP;
    private string SoulPickUP;
    private string general;


    private void Awake()
    {
        testUser = new UserData();
        ItemGachaList = new Dictionary<string, List<testItem>>();
        SoulGachaList  = new Dictionary<string, List<testSoul>>();
        //Todo : ������ ����Ʈ, �ҿ� ����Ʈ �޾ƿ���
        ItemPickUP = "GoldenSword"; //Todo : �̺�Ʈ �Ŵ���? �� ���� �Ⱦ� �̺�Ʈ ���� �ʿ�
        SoulPickUP = "SSS";
    }

    public void DiamondGacha<T>(string Pickup) where T : IGachable
    {
        string t = typeof(T).ToString();
        if (testUser.Diamonds >= GachaPrice)
        {

            switch (t)
            {
                case "testSoul":
                    Gacha<testSoul>(SoulGachaList[Pickup]);
                    break;
                case "testItem":
                    Gacha<testItem>(ItemGachaList[Pickup]);
                    break;
            }
        }
        else Debug.LogAssertion("���̾ư� �����մϴ�.");
    }

    private T Gacha<T>(List<T> GachaList) where T : IGachable
    {
        List<T> normal = new List<T>();
        List<T> Rare = new List<T>();
        List<T> Legendary = new List<T>();

        for (int i = 0; i < GachaList.Count; i++)
        {
            switch (GachaList[i].GetGrade())
            {
                case Enums.Grade.Normal:
                    normal.Add(GachaList[i]);
                    break;
                case Enums.Grade.Rare:
                    Rare.Add(GachaList[i]);
                    break;
                case Enums.Grade.Legendary:
                    Legendary.Add(GachaList[i]);
                    break;
            }
        }

        int target = Random.Range(0, 100);
        List<T> targetList = new List<T>();
        {
            if (target >= 30) targetList = normal;
            else if (target >= 5) targetList = Rare;
            else targetList = Legendary;
        }

        target = Random.Range(0, targetList.Count);
        
        return targetList[target];
    }
}

public interface IGachable
{
    Enums.Grade GetGrade();

    int GetID();

    string GetName();
}

public class testSoul : IGachable //DataManager.Instance.SoulDB
{
    public int ID;
    public Enums.Grade Grade;
    public string Name;

    public Grade GetGrade()
    {
        return Grade;
    }

    public int GetID()
    {
        return ID;
    }

    public string GetName()
    {
        return Name;
    }
}

public class testItem : IGachable
{
    public int ID;
    public Enums.Grade Grade;
    public string Name;

    public Grade GetGrade()
    {
        return Grade;
    }

    public int GetID()
    {
        return ID;
    }

    public string GetName()
    {
        return Name;
    }
}