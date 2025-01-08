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

}
public class DialogLoader
{
    public List<Dialog> ItemsList { get; private set; }
    public Dictionary<int, Dialog> ItemsDict { get; private set; }
    public List<Dialog> ListByCycle { get; private set; }

    public DialogLoader(string path = "JSON/Dialogue")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Dialog>();
        ListByCycle = new List<Dialog>();
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
        ListByCycle.Clear();
        foreach (Dialog log in ItemsList)
        {
            if(log.cycle == cycle)
            {
                ListByCycle.Add(log);
            }
        }

        return ListByCycle;
    }
}
