using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerManager : Singleton<TestPlayerManager>
{
    private TestInventoryModel inventory;

    public TestInventoryModel Inventory { get { return inventory; } set { inventory = value; } }

    public void OnClickAddItem(string key)
    {
        inventory.AddItem(key);
    }

    public void OnClickRemoveItem(string key)
    {
        inventory.RemoveItem(key);
    }
}
