using System.Collections.Generic;
using UnityEngine;

public class ShopModel : UIModel
{
    public UserData testUser;
    public List<testSoul> GachaList;
    //public Item[] SellList;
    public int GachaCount;
    public int GachaPrice;

    private void Awake()
    {
        //GachaList = DataManger.Instance.SoulDB
    }

    public void DiamondGacha()
    {
        if(testUser.Diamonds >= GachaPrice)
        {
            testUser.Diamonds -= GachaPrice;
            Gacha<testSoul>();
        }
    }

    private void Gacha<T>() where T : class
    {
        //Todo : 등급(1성,2성,3성)에 따른 확률 분류
    }


}

public class testSoul //DataManager.Instance.SoulDB
{
    public int ID;
}