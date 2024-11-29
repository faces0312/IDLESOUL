using System.Collections.Generic;

public class ShopInventory : UIModel
{
    public ShopSlot[] InventorySlots;


    private void Awake()
    {
        InventorySlots = GetComponentsInChildren<ShopSlot>();
    }
}