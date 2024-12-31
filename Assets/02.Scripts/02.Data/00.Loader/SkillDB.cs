using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SkillDB
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
    /// SkillType
    /// </summary>
    public SkillType SkillType;

    /// <summary>
    /// MaxLevel
    /// </summary>
    public int MaxLevel;

    /// <summary>
    /// Value
    /// </summary>
    public float Value;

    /// <summary>
    /// UpgradeValue
    /// </summary>
    public float UpgradeValue;

    /// <summary>
    /// ApplyCount
    /// </summary>
    public int ApplyCount;

    /// <summary>
    /// UpgradeCost
    /// </summary>
    public int UpgradeCost;

    /// <summary>
    /// SpritePath
    /// </summary>
    public string SpritePath;
}
public class SkillDBLoader
{
    public List<SkillDB> ItemsList { get; private set; }
    public Dictionary<int, SkillDB> ItemsDict { get; private set; }

    public SkillDBLoader(string path = "JSON/SkillDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, SkillDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<SkillDB> Items;
    }

    public SkillDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public SkillDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
