using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Dialog
{
    /// <summary>
    /// key
    /// </summary>
    public int key;

    /// <summary>
    /// cycle
    /// </summary>
    public int cycle;

    /// <summary>
    /// index
    /// </summary>
    public int index;

    /// <summary>
    /// 1 = talk 2 = narration 3 = selection
    /// </summary>
    public int ConversationType;

    /// <summary>
    /// name
    /// </summary>
    public string name;

    /// <summary>
    /// text
    /// </summary>
    public string text;

    /// <summary>
    /// ImagePath
    /// </summary>
    public string CharacterImage;

    /// <summary>
    /// BgPath
    /// </summary>
    public string BackgroundMusic;

    /// <summary>
    /// SfxPath
    /// </summary>
    public string EffectMusic;

    /// <summary>
    /// Hexacode
    /// </summary>
    public string ImageColor;

    /// <summary>
    /// 0 = none
    /// </summary>
    public int NextIndex;

    /// <summary>
    /// 0 = none, 1 = jump, 2 = move, 3 = move with flip
    /// </summary>
    public int AnimControll;

}
public class DialogLoader
{
    public List<Dialog> ItemsList { get; private set; }
    public Dictionary<int, Dialog> ItemsDict { get; private set; }
    private List<Dialog> tempList;

    public DialogLoader(string path = "JSON/Dialog")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Dialog>();
        tempList = new List<Dialog>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Dialog> Items;
    }

    public Dialog GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Dialog GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
    public List<Dialog> GetByCycle(int cycle)
    {
        tempList.Clear();
        foreach (var item in ItemsList)
        {
            if (item.cycle == cycle)
                tempList.Add(item);
        }
        return tempList;
    }
}
