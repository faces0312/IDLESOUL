using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class StatUpgradeDB
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// MaxHealthBaseCost
    /// </summary>
    public float MaxHealthBaseCost;

    /// <summary>
    /// MaxHealthGrowRateCost
    /// </summary>
    public float MaxHealthGrowCost;

    /// <summary>
    /// MaxHealthGrowRate
    /// </summary>
    public float MaxHealthGrowRate;

    /// <summary>
    /// MaxHealthBaseStat
    /// </summary>
    public float MaxHealthBaseStat;

    /// <summary>
    /// AtkBaseCost
    /// </summary>
    public float AtkBaseCost;

    /// <summary>
    /// AtkBaseGrowCost
    /// </summary>
    public float AtkGrowCost;

    /// <summary>
    /// AtkAtkGrowRate
    /// </summary>
    public float AtkGrowRate;

    /// <summary>
    /// AtkBaseStat
    /// </summary>
    public float AtkBaseStat;

    /// <summary>
    /// DefBaseCost
    /// </summary>
    public float DefBaseCost;

    /// <summary>
    /// DefGrowCost
    /// </summary>
    public float DefGrowCost;

    /// <summary>
    /// DefGrowRate
    /// </summary>
    public float DefGrowRate;

    /// <summary>
    /// DefBaseStat
    /// </summary>
    public float DefBaseStat;

    /// <summary>
    /// ReduceDamageBaseCost
    /// </summary>
    public float ReduceDamageBaseCost;

    /// <summary>
    /// ReduceDamageGrowCost
    /// </summary>
    public float ReduceDamageGrowCost;

    /// <summary>
    /// ReduceDamageGrowRate
    /// </summary>
    public float ReduceDamageGrowRate;

    /// <summary>
    /// ReduceDamageBaseStat
    /// </summary>
    public float ReduceDamageBaseStat;

    /// <summary>
    /// CriticalRateBaseCost
    /// </summary>
    public float CriticalRateBaseCost;

    /// <summary>
    /// CriticalRateGrowCost
    /// </summary>
    public float CriticalRateGrowCost;

    /// <summary>
    /// CriticalRateGrowRate
    /// </summary>
    public float CriticalRateGrowRate;

    /// <summary>
    /// CriticalRateBaseStat
    /// </summary>
    public float CriticalRateBaseStat;

    /// <summary>
    /// CriticalDamageBaseCost
    /// </summary>
    public float CriticalDamageBaseCost;

    /// <summary>
    /// CriticalDamageGrowCost
    /// </summary>
    public float CriticalDamageGrowCost;

    /// <summary>
    /// CriticalDamageGrowRate
    /// </summary>
    public float CriticalDamageGrowRate;

    /// <summary>
    /// CriticalDamageBaseStat
    /// </summary>
    public float CriticalDamageBaseStat;

}
public class StatUpgradeDBLoader
{
    public List<StatUpgradeDB> ItemsList { get; private set; }
    public Dictionary<int, StatUpgradeDB> ItemsDict { get; private set; }

    public StatUpgradeDBLoader(string path = "JSON/StatUpgradeDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, StatUpgradeDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<StatUpgradeDB> Items;
    }

    public StatUpgradeDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public StatUpgradeDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
