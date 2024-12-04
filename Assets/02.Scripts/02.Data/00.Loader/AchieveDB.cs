using ScottGarland;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class AchieveDB
{
    /// <summary>
    /// key
    /// </summary>
    public int key;
    /// <summary>
    /// ID
    /// </summary>
    public int ID;
    /// <summary>
    /// Name
    /// </summary>
    public string Name;
    /// <summary>
    /// Description
    /// </summary>
    public string Description;
    /// <summary>
    /// Progress
    /// </summary>
    public float progress;
    /// <summary>
    /// Goal
    /// </summary>
    public float Goal;
    /// <summary>
    /// iconPath
    /// </summary>
    public string iconPath;
    /// <summary>
    /// (int)Enums.Action / 0 = None, 1 = 
    /// </summary>
    public int Action;
    /// <summary>
    /// (int)AchievementType / 0 = None, 1 = KillMonster, 2 = ClearStage, 3 = CollectGold, 4 = CollectSoul, 5 = Time
    /// </summary>
    public int AchievementType;
    

    public void JsonDataConvert(AchieveData achieveData)
    {
        ID = achieveData.ID;
        Name = achieveData.Name;
        Description = achieveData.Description;
        progress = 0;
        Goal = achieveData.Goal;
        iconPath = achieveData.iconPath;
        AchievementType = (int)achieveData.AchievementType;
    }

}
public class AchieveDBLoader
{
    public List<AchieveDB> AchieveList { get; private set; }
    public Dictionary<int, AchieveDB> AchieveDict { get; private set; }

    public AchieveDBLoader(string path = "JSON/AchieveDB")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        AchieveList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        AchieveDict = new Dictionary<int, AchieveDB>();
        foreach (var item in AchieveList)
        {
            AchieveDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<AchieveDB> Items;
    }

    public AchieveDB GetByKey(int key)
    {
        if (AchieveDict.ContainsKey(key))
        {
            return AchieveDict[key];
        }
        return null;
    }
    public AchieveDB GetByIndex(int index)
    {
        if (index >= 0 && index < AchieveList.Count)
        {
            return AchieveList[index];
        }
        return null;
    }
}
