using System;
using System.Collections.Generic;
using Enums;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class AchievementManager : SingletonDDOL<AchievementManager>
{
    public Dictionary<AchievementType, List<AchieveData>> achievements; //도전과제 리스트
    private List<AchieveDB> achievementDB; //도전과제 DB
    public List<AchieveData> aDatas = new List<AchieveData>();

    protected void Start()
    {
        achievements = new Dictionary<AchievementType, List<AchieveData>>();
        achievementDB = DataManager.Instance.AchieveDB.AchieveList;

        foreach (var item in achievementDB)
        {
            AchieveData data = new AchieveData(item);
            if (achievements?.ContainsKey((AchievementType)item.AchievementType) == false)
            {
                achievements[(AchievementType)item.AchievementType] = new List<AchieveData>();
            }
            achievements[(AchievementType)item.AchievementType].Add(data);
        }
        EventManager.Instance.Subscribe<AchieveEvent>(Channel.Achievement, OnTriggerAction);
    }

    public void OnTriggerAction(AchieveEvent data)
    {
        aDatas.Clear();
        if ((int)data.Action == 0 || (int)data.Type == 0) return;
        foreach (AchieveData aData in EventToData(data.Action, data.Type))
        {
            aData.AddProgress(data.Value);
        }
    }

    public List<AchieveData> EventToData(ActionType action, AchievementType type)
    {
        foreach (AchieveData aData in achievements[type])
        {
            if (aData.Action == action)
            {
                aDatas.Add(aData);
            }
        }
        return aDatas;
    }
}
