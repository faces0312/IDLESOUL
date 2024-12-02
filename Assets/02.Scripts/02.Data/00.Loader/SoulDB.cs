using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SoulDB
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
    /// JobType
    /// </summary>
    public JobType JobType;

    /// <summary>
    /// Health
    /// </summary>
    public float Health;

    /// <summary>
    /// Attack
    /// </summary>
    public float Attack;

    /// <summary>
    /// Defence
    /// </summary>
    public float Defence;

    /// <summary>
    /// ReduceDamage
    /// </summary>
    public float ReduceDamage;

    /// <summary>
    /// AttackSpeed
    /// </summary>
    public float AttackSpeed;

    /// <summary>
    /// CriticalRate
    /// </summary>
    public float CriticalRate;

    /// <summary>
    /// CriticalDamage
    /// </summary>
    public float CriticalDamage;

    /// <summary>
    /// AtkSpeed
    /// </summary>
    public float AtkSpeed;

    /// <summary>
    /// MoveSpeed
    /// </summary>
    public float MoveSpeed;

    /// <summary>
    /// CoolDown
    /// </summary>
    public float CoolDown;

    /// <summary>
    /// MaxExp
    /// </summary>
    public int MaxExp;

    /// <summary>
    /// SkillList
    /// </summary>
    public List<int> SkillList;

    /// <summary>
    /// AttackType
    /// </summary>
    public AttackType AttackType;
}
public class SoulDBLoader
{
    public List<SoulDB> ItemsList { get; private set; }
    public Dictionary<int, SoulDB> ItemsDict { get; private set; }

    public SoulDBLoader(string path = "JSON/SoulDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, SoulDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<SoulDB> Items;
    }

    public SoulDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public SoulDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
