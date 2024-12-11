using Enums;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GachaItemDB : IGachableDB
{
    /// <summary>
    /// ProductID
    /// </summary>
    public int key;

    public int GetKey()
    {
        return key;
    }

    public int GetRairity()
    {
        throw new NotImplementedException();
    }
}
public class GachaItemDBLoader
{
    public List<GachaItemDB> ItemsList { get; private set; }
    public Dictionary<int, GachaItemDB> ItemsDict { get; private set; }

    public GachaItemDBLoader(string path = "JSON/SellItemDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, GachaItemDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<GachaItemDB> Items;
    }

    public GachaItemDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public GachaItemDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
