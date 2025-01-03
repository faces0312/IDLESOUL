using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class TutorialDB
{
    /// <summary>
    /// Key
    /// </summary>
    public int Key;

    /// <summary>
    /// Xpos
    /// </summary>
    public float Xpos;

    /// <summary>
    /// Ypos
    /// </summary>
    public float Ypos;

    /// <summary>
    /// Width
    /// </summary>
    public float Width;

    /// <summary>
    /// Height
    /// </summary>
    public float Height;

    /// <summary>
    /// TextXpos
    /// </summary>
    public float TextXpos;

    /// <summary>
    /// TextYpos
    /// </summary>
    public float TextYpos;

    /// <summary>
    /// TextWidth
    /// </summary>
    public float TextWidth;

    /// <summary>
    /// TextHeight
    /// </summary>
    public float TextHeight;

    /// <summary>
    /// Text
    /// </summary>
    public string Text;

}
public class TutorialDBLoader
{
    public List<TutorialDB> ItemsList { get; private set; }
    public Dictionary<int, TutorialDB> ItemsDict { get; private set; }

    public TutorialDBLoader(string path = "JSON/TutorialDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, TutorialDB>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.Key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<TutorialDB> Items;
    }

    public TutorialDB GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public TutorialDB GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
