using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.U2D;
using System.Drawing;

public class InventoryModel : UIModel
{
    public List<Item> Items = new List<Item>(); // 소지하고있는 아이템 리스트 
   
    public InventoryModel()
    {
        Initilaize();
    }

    public void Initilaize()
    {
        foreach (ItemDB Data in DataManager.Instance.ItemDB.ItemsDict.Values)
        {
            Item itemObj = new Item();
            itemObj.Initialize(Data);
            Items.Add(itemObj);
        }

        GameManager.Instance.player.Inventory = this;
    }

    public void AddItem(int key)
    {
        Item item = new Item();
        item.Initialize(DataManager.Instance.ItemDB.GetByKey(key));
        //첫 획득시 아이템 소지여부를 true로 변경


        foreach (Item inven in Items)
        {
            if (inven.ItemStat.iD == item.ItemStat.iD)
            {
                if (!inven.IsGain)
                {
                    inven.IsGain = true;
                }
                else
                {
                    inven.stack += 1;
                }
            }
        }
    }

    public void RemoveItem(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
        }

    }
}
