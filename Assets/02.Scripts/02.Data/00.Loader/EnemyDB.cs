using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class EnemyDB
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
    /// Descripton
    /// </summary>
    public string Descripton;

    /// <summary>
    /// DropItemID
    /// </summary>
    public List<int> DropItemID;

    /// <summary>
    /// DropGold
    /// </summary>
    public int DropGold;

    /// <summary>
    /// Attack
    /// </summary>
    public float Attack;

    /// <summary>
    /// AttackSpeed
    /// </summary>
    public float AttackSpeed;

    /// <summary>
    /// Defence
    /// </summary>
    public float Defence;

    /// <summary>
    /// MoveSpeed
    /// </summary>
    public float MoveSpeed;

    /// <summary>
    /// Health
    /// </summary>
    public float Health;

    /// <summary>
    /// CritChance
    /// </summary>
    public float CritChance;

    /// <summary>
    /// CritDamage
    /// </summary>
    public float CritDamage;

}
public class EnemyDBLoader
{
    public List<EnemyDB> ItemsList { get; private set; }
    public Dictionary<int, EnemyDB> ItemsDict { get; private set; }

    public EnemyDBLoader(string path = "JSON/EnemyDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, EnemyDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<EnemyDB> Items;
    }

    public EnemyDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public EnemyDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
