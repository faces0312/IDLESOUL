using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerManager : Singleton<TestPlayerManager>
{
    private TestInventoryModel inventory;
    private StatHandler playerStatHandler;
    public event Action OnUpdateSoulStats;

    public TestInventoryModel Inventory { get { return inventory; } set { inventory = value; } }
    public StatHandler PlayerStatHandler { get { return playerStatHandler; } set { playerStatHandler = value; } }

    public void OnClickAddItem(string key)
    {
        inventory.AddItem(key);
    }

    public void OnClickRemoveItem(string key)
    {
        inventory.RemoveItem(key);
    }

    public void OnClickLevelUp(int level)
    {
        playerStatHandler.LevelUp(level);
        OnUpdateSoulStats?.Invoke();
    }
}
