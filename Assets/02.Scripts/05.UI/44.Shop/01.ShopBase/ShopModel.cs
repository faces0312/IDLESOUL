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
        ItemGachaList = new Dictionary<string, List<testItem>>();
        SoulGachaList  = new Dictionary<string, List<testSoul>>();
        //Todo : 아이템 리스트, 소울 리스트 받아오기
        ItemPickUP = "GoldenSword"; //Todo : 이벤트 매니저? 를 통해 픽업 이벤트 관리 필요
        SoulPickUP = "SSS";
    }

    public void DiamondGacha<T>(string Pickup) where T : Gachable
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

    private T Gacha<T>(List<T> GachaList) where T : Gachable
    {
        List<T> normal = new List<T>();
        List<T> Rare = new List<T>();
        List<T> Legendary = new List<T>();

        for (int i = 0; i < GachaList.Count; i++)
        {
            switch (GachaList[i].Grade)
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

public class Gachable
{
    public int ID;
    public Enums.Grade Grade;
    public string Name;
}

public class testSoul : Gachable //DataManager.Instance.SoulDB
{
    
}

public class testItem : Gachable
{

}