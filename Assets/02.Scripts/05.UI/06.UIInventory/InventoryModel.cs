using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.U2D;
using System.Drawing;

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

    public void AddItem(int key)
    {
        //ToDoCode : 아이템을 인벤토리에 추가될시 동작하는 메서드
    }

    public void RemoveItem(int key)
    {
        //ToDoCode : 아이템을 인벤토리에 삭제될때 동작하는 메서드
    }
}
