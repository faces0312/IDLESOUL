using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.U2D;

public class InventoryModel : UIModel
{
    public List<Item> Items = new List<Item>(); // 소지하고있는 아이템 리스트 

    public void Initilaize()
    {
        foreach (ItemDB Data in DataManager.Instance.ItemDB.ItemsDict.Values)
        {
            Item itemObj = new Item();
            itemObj.Initialize(Data);
            Items.Add(itemObj);
        }
    }

    public void AddItem(string item)
    {

    }

    public void RemoveItem(string item)
    {

    }
}
