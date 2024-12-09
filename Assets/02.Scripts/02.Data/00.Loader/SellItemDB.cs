using Enums;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SellItemDB
{
    /// <summary>
    /// ProductID
    /// </summary>
    public int key;

    /// <summary>
    /// OriginID
    /// </summary>
    public List<int> OriginID;

    /// <summary>
    /// ProductName
    /// </summary>
    public string ProductName;

    /// <summary>
    /// ProductDescription
    /// </summary>
    public string ProductDescription;

    /// <summary>
    /// PriceType
    /// </summary>
    public int PriceType;

    /// <summary>
    /// Price
    /// </summary>
    public int Price;

    /// <summary>
    /// IsStack
    /// </summary>
    public bool IsStack;

    /// <summary>
    /// StackCount
    /// </summary>
    public int StackCount;

    /// <summary>
    /// IconPath
    /// </summary>
    public string IconPath;

}
public class SellItemDBLoader
{
    public List<SellItemDB> ItemsList { get; private set; }
    public Dictionary<int, SellItemDB> ItemsDict { get; private set; }

    public SellItemDBLoader(string path = "JSON/SellItemDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, SellItemDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<SellItemDB> Items;
    }

    public SellItemDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public SellItemDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
