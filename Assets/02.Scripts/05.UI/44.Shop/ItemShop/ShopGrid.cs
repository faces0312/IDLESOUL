using System.Collections.Generic;
using UnityEngine;

public class ShopGrid : MonoBehaviour
{
    public List<ShopSlotComponent> slots = new List<ShopSlotComponent>();

    private void Start()
    {
        ShopSlotComponent[] tempSlots = GetComponentsInChildren<ShopSlotComponent>();
        for(int i = 0; i < tempSlots.Length; i++)
        {
            slots.Add(tempSlots[i]);
        }
    }

    public void SetItem()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetItem(DataManager.Instance.SellItemDB.ItemsList[i]);
        }
    }
}