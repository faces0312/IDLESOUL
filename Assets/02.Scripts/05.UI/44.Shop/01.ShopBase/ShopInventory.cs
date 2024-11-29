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
        //TODO : �Ǹ� ������ ���� ������ �߰� �Ǵ� �����ϴ� ��� �߰�
    }
}