using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

public interface IShopItem
{
    public int GetKey();
    public PriceType GetPriceType();
    public string GetName();
    public string GetDescription();
    public string GetIconPath();
    public int GetPrice();
}

[Serializable]
public class ExchangeDB : IShopItem
{
    /// <summary>
    /// ProductID
    /// </summary>
    public int key;

    /// <summary>
    /// ProductName
    /// </summary>
    public string ProductName;

    /// <summary>
    /// ProductDescription
    /// </summary>
    public string ProductDescription;

    /// <summary>
    /// Product
    /// </summary>
    public int Product;

    /// <summary>
    /// PriceType
    /// </summary>
    public int PriceType;

    /// <summary>
    /// Price
    /// </summary>
    public int Price;

    /// <summary>
    /// IconPath
    /// </summary>
    public string IconPath;

    public string GetDescription()
    {
        return ProductDescription;
    }

    public string GetIconPath()
    {
        return IconPath;
    }

    public int GetKey()
    {
        return key;
    }

    public string GetName()
    {
        return ProductName;
    }

    public int GetPrice()
    {
        return Price;
    }

    public PriceType GetPriceType()
    {
        return (PriceType)PriceType;
    }
}
public class ExchangeDBLoader
{
    public List<ExchangeDB> ItemsList { get; private set; }
    public Dictionary<int, ExchangeDB> ItemsDict { get; private set; }

    public ExchangeDBLoader(string path = "JSON/ExchangeDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, ExchangeDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<ExchangeDB> Items;
    }

    public ExchangeDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public ExchangeDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
