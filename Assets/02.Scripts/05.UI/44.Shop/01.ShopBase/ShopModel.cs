using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel : UIModel
{
    public UserData testUser;
    public Dictionary<string, List<testItem>> ItemGachaList;
    public Dictionary<string, List<testSoul>> SoulGachaList;
    public int GachaCount; //후에 데이터 매니저에 저장 필요
    public int GachaPrice;
    private string ItemPickUP;
    private string SoulPickUP;
    private string general;


    private void Awake()
    {
        testUser = new UserData();
        ItemGachaList = new Dictionary<string, List<testItem>>();
        SoulGachaList  = new Dictionary<string, List<testSoul>>();
        //Todo : 아이템 리스트, 소울 리스트 받아오기
        ItemPickUP = "GoldenSword"; //Todo : 이벤트 매니저? 를 통해 픽업 이벤트 관리 필요
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
        else Debug.LogAssertion("다이아가 부족합니다.");
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