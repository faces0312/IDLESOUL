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
        //Todo : ���(1��,2��,3��)�� ���� Ȯ�� �з�
    }


}

public class testSoul //DataManager.Instance.SoulDB
{
    public int ID;
}