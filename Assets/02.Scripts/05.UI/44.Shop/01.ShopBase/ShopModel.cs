using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopModel : UIModel
{
    public UserData testUser;
    public Dictionary<string, List<testItem>> ItemGachaList;
    public Dictionary<string, List<testSoul>> SoulGachaList;
    public int SoulGachaCount; //후에 데이터 매니저에 저장 필요
    public int ItemGachaCount;
    public int GachaPrice;
    private string general;


    private void Awake()
    {
        ItemGachaList = new Dictionary<string, List<testItem>>();
        SoulGachaList = new Dictionary<string, List<testSoul>>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void DiamondGacha<T>(string Pickup) where T : IGachable
    {
        string t = typeof(T).ToString();

        switch (t)
        {
            case "testSoul":
                if(SoulGachaList.ContainsKey(Pickup))
                {
                    if (SoulGachaCount % 10 != 0)
                    {
                        GeneralGacha<testSoul>(SoulGachaList[Pickup]);
                        SoulGachaCount++;
                    }
                    else
                    {
                        Gacha10th<testSoul>(SoulGachaList[Pickup]);
                        SoulGachaCount++;
                    }
                }
                break;
            case "testItem":
                if(ItemGachaList.ContainsKey(Pickup))
                {
                    if (ItemGachaCount % 10 != 0)
                    {
                        GeneralGacha<testItem>(ItemGachaList[Pickup]);
                        ItemGachaCount++;
                    }
                    else
                    {
                        Gacha10th<testItem>(ItemGachaList[Pickup]);
                        ItemGachaCount++;
                    }
                }
                break;
        }
    }

    private T GeneralGacha<T>(List<T> GachaList) where T : IGachable
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

    private T Gacha10th<T>(List<T> GachaList) where T : IGachable
    {
        List<T> Rare = new List<T>();
        List<T> Legendary = new List<T>();

        for (int i = 0; i < GachaList.Count; i++)
        {
            switch (GachaList[i].GetGrade())
            {
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
            if (target >= 5) targetList = Rare;
            else targetList = Legendary;
        }

        target = Random.Range(0, targetList.Count);


        return targetList[target];
    }
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