using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ShopGrid : MonoBehaviour
{
    public List<ShopSlotComponent> slots = new List<ShopSlotComponent>();
    private List<SellItemDB> datas;
    private ShopType type;

    private void Start()
    {
        ShopSlotComponent[] tempSlots = GetComponentsInChildren<ShopSlotComponent>();
        for(int i = 0; i < tempSlots.Length; i++)
        {
            slots.Add(tempSlots[i]);
            slots[i].Init();
            slots[i].slot.SetItem(DataManager.Instance.SellItemDB.ItemsList[i]);
        }
        this.gameObject.SetActive(false);
    }

    public void SetItem(ShopType type)
    {
        this.type = type;
        if (type == ShopType.Item)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].SetItem(DataManager.Instance.SellItemDB.ItemsList[i]);
            }
        }
        else if (type == ShopType.Product)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                
            }
        }
    }
}