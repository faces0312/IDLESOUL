using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Dialogue
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
public class DialogueLoader
{
    public List<Dialogue> ItemsList { get; private set; }
    public Dictionary<int, Dialogue> ItemsDict { get; private set; }
    public List<Dialogue> ListByCycle { get; private set; }

    public DialogueLoader(string path = "JSON/Dialogue")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, Dialogue>();
        ListByCycle = new List<Dialogue>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<Dialogue> Items;
    }

    public Dialogue GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public Dialogue GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }

    public List<Dialogue> GetByCycle(int cycle)
    {
        ListByCycle.Clear();
        foreach (Dialogue log in ItemsList)
        {
            if(log.cycle == cycle)
            {
                ListByCycle.Add(log);
            }
        }

        return ListByCycle;
    }
}
