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
    public int MaxHealthBaseCost;

    /// <summary>
    /// MaxHealthGrowRate
    /// </summary>
    public float MaxHealthGrowRate;

    /// <summary>
    /// MaxHealthBaseStat
    /// </summary>
    public int MaxHealthBaseStat;

    /// <summary>
    /// AtkBaseCost
    /// </summary>
    public int AtkBaseCost;

    /// <summary>
    /// AtkAtkGrowRate
    /// </summary>
    public float AtkGrowRate;

    /// <summary>
    /// AtkBaseStat
    /// </summary>
    public int AtkBaseStat;

    /// <summary>
    /// DefBaseCost
    /// </summary>
    public int DefBaseCost;

    /// <summary>
    /// DefGrowRate
    /// </summary>
    public float DefGrowRate;

    /// <summary>
    /// DefBaseStat
    /// </summary>
    public int DefBaseStat;

    /// <summary>
    /// ReduceDamageBaseCost
    /// </summary>
    public int ReduceDamageBaseCost;

    /// <summary>
    /// ReduceDamageGrowRate
    /// </summary>
    public float ReduceDamageGrowRate;

    /// <summary>
    /// ReduceDamageBaseStat
    /// </summary>
    public int ReduceDamageBaseStat;

    /// <summary>
    /// CriticalRateBaseCost
    /// </summary>
    public int CriticalRateBaseCost;

    /// <summary>
    /// CriticalRateGrowRate
    /// </summary>
    public float CriticalRateGrowRate;

    /// <summary>
    /// CriticalRateBaseStat
    /// </summary>
    public int CriticalRateBaseStat;

    /// <summary>
    /// CriticalDamageBaseCost
    /// </summary>
    public int CriticalDamageBaseCost;

    /// <summary>
    /// CriticalDamageGrowRate
    /// </summary>
    public float CriticalDamageGrowRate;

    /// <summary>
    /// CriticalDamageBaseStat
    /// </summary>
    public int CriticalDamageBaseStat;

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
