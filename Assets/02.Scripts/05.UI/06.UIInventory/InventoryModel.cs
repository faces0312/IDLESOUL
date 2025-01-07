using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;


public class InventoryModel : UIModel
{
    public List<Item> Items = new List<Item>(); // 소지하고있는 아이템 리스트 
    public Dictionary<int, Item> DictItems = new Dictionary<int, Item>(); // (탐색용)소지하고 있는 아이템 딕셔너리 

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

        foreach (Item item in Items)
        {
            DictItems.Add(item.ItemStat.iD, item);
        }

        for (int i = 0; i< GameManager.Instance.player.UserData.GainItem.Count; i++)
        {
            int key = GameManager.Instance.player.UserData.GainItem[i].ID;

            if (DictItems.ContainsKey(key))
            {
                InitItem(GameManager.Instance.player.UserData.GainItem[i]);
            }
        }

        GameManager.Instance.player.Inventory = this;
    }

    public void AddItem(int key)
    {
        Item item = new Item();
        item.Initialize(DataManager.Instance.ItemDB.GetByKey(key));

        if (DictItems.ContainsKey(key))
        {
            if (!DictItems[key].IsGain)
            {
                //첫 획득시 아이템 소지여부를 true로 변경
                DictItems[key].IsGain = true;

                //획득시 보유효과(패시브) 스텟 적용
                GameManager.Instance.player.StatHandler.EquipItem(DictItems[key].PassiveStat);
                EventManager.Instance.Publish<AchieveEvent>(Enums.Channel.Achievement, new AchieveEvent(Enums.AchievementType.Collect, Enums.ActionType.Item, 1));
                //아이템 획득시 데이터 저장
                GameManager.Instance.player.UserData.GainItem.Add(new UserItemData(DictItems[key]));
            }
            else
            {
                DictItems[key].stack += 1;
            }

            //아이템 획득시 게임 데이터 저장
            DataManager.Instance.SaveUserData(GameManager.Instance.player.UserData);
        }


    }

    public void InitItem(UserItemData userItem)
    {
        //DictItems[userItem.ID].Initialize(DataManager.Instance.ItemDB.GetByKey(userItem.ID));
        DictItems[userItem.ID].LoadInitData(userItem);

        if (DictItems.ContainsKey(userItem.ID))
        {
            if (!DictItems[userItem.ID].IsGain)
            {
                //첫 획득시 아이템 소지여부를 true로 변경
                DictItems[userItem.ID].IsGain = true;

                //획득시 보유효과(패시브) 스텟 적용
                GameManager.Instance.player.StatHandler.EquipItem(DictItems[userItem.ID].PassiveStat);
            }

            if(DictItems[userItem.ID].equip)
            {
                
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
