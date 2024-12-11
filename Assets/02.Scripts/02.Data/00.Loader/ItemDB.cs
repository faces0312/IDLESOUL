using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ItemDB : IGachableDB
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// Name
    /// </summary>
    public string Name;

    /// <summary>
    /// Type
    /// </summary>
    public string Type;

    /// <summary>
    /// Rairty
    /// </summary>
    public string Rairty;

    /// <summary>
    /// Descripton
    /// </summary>
    public string Descripton;

    /// <summary>
    /// Attack
    /// </summary>
    public float Attack;

    /// <summary>
    /// AttackPercent
    /// </summary>
    public bool AttackPercent;

    /// <summary>
    /// Defence
    /// </summary>
    public float Defence;

    /// <summary>
    /// DefencePercent
    /// </summary>
    public bool DefencePercent;

    /// <summary>
    /// Health
    /// </summary>
    public float Health;

    /// <summary>
    /// HealthPercent
    /// </summary>
    public bool HealthPercent;

    /// <summary>
    /// CritChance
    /// </summary>
    public float CritChance;

    /// <summary>
    /// CritChancePercent
    /// </summary>
    public bool CritChancePercent;

    /// <summary>
    /// CritDamage
    /// </summary>
    public float CritDamage;

    /// <summary>
    /// CritDamagePercent
    /// </summary>
    public bool CritDamagePercent;

    /// <summary>
    /// Effect
    /// </summary>
    public string Effect;

    /// <summary>
    /// Cost
    /// </summary>
    public int Cost;

    /// <summary>
    /// StackMaxCount
    /// </summary>
    public int StackMaxCount;

    /// <summary>
    /// IconPath
    /// </summary>
    public string IconPath;

    /// <summary>
    /// SpritePath
    /// </summary>
    public string SpritePath;

    public int GetKey()
    {
        return key;
    }

    public int GetRairity()
    {
        throw new NotImplementedException();
    }
}
public class ItemDBLoader
{
    public List<ItemDB> ItemsList { get; private set; }
    public Dictionary<int, ItemDB> ItemsDict { get; private set; }

    public ItemDBLoader(string path = "JSON/ItemDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, ItemDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<ItemDB> Items;
    }

    public ItemDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public ItemDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
