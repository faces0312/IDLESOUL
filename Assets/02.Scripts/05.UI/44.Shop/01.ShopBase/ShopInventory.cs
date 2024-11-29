using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : UIModel
{
    public ShopSlot[] InventorySlots;
    private GameObject slotPrefab;

    private void Awake()
    {
        InventorySlots = GetComponentsInChildren<ShopSlot>();
        slotPrefab = Resources.Load<GameObject>("Prafabs/Sample/Slot");
        //TODO : 판매 갯수에 맞춰 슬롯을 추가 또는 삭제하는 기능 추가
    }
}