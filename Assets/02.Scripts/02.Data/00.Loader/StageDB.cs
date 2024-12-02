using Enums;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class StageDB
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// ChapterNum
    /// </summary>
    public int ChapterNum;

    /// <summary>
    /// StageNum
    /// </summary>
    public int StageNum;

    /// <summary>
    /// CurStageModifier
    /// </summary>
    public float CurStageModifier;

    /// <summary>
    /// stageName
    /// </summary>
    public string stageName;

    /// <summary>
    /// SlayEnemyCount
    /// </summary>
    public int SlayEnemyCount;

    /// <summary>
    /// SummonEnemyIDList
    /// </summary>
    public List<int> SummonEnemyIDList;


    /// <summary>
    /// StageType Int / 0 = none, 1 = daily, 2 = EXP, 3 = Upgrade
    /// </summary>
    public int StageType;

}
public class StageDBLoader
{
    public List<StageDB> ItemsList { get; private set; }
    public Dictionary<int, StageDB> ItemsDict { get; private set; }

    public StageDBLoader(string path = "JSON/StageDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, StageDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<StageDB> Items;
    }

    public StageDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public StageDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
