using System.Collections.Generic;


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

        //유저데이터의 아이템 데이터를 불러오기
        for(int i = 0; i < GameManager.Instance.player.UserData.GainItemID.Count; i++)
        {
            AddItem(GameManager.Instance.player.UserData.GainItemID[i]);
        } 
    }

    public void AddItem(int key)
    {
        Item item = new Item();
        item.Initialize(DataManager.Instance.ItemDB.GetByKey(key));

        foreach (Item inven in Items)
        {
            if (inven.ItemStat.iD == item.ItemStat.iD)
            {
                if (!inven.IsGain)
                {
                    //첫 획득시 아이템 소지여부를 true로 변경
                    inven.IsGain = true;

                    //획득시 보유효과(패시브) 스텟 적용
                    GameManager.Instance.player.StatHandler.EquipItem(inven.PassiveStat);
                    EventManager.Instance.Publish<AchieveEvent>(Enums.Channel.Achievement, new AchieveEvent(Enums.AchievementType.Collect, Enums.ActionType.Item, 1));
                }
                else
                {
                    inven.stack += 1;
                }
            }
        }
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < Items.Count; i++) 
        {
            if (Items[i].ItemStat.iD == item.ItemStat.iD)
            {
                Items[i] = item;
                break;
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
