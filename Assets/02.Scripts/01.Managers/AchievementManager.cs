using System;
using System.Collections.Generic;
using Enums;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class AchievementManager : SingletonDDOL<AchievementManager>
{
    private Dictionary<AchievementType, List<AchieveData>> achievements;
    private List<AchieveDB> achievementDB;
    public Animation anime;

    protected override void Awake()
    {
        base.Awake();
        achievements = new Dictionary<AchievementType, List<AchieveData>>();
        achievementDB = DataManager.Instance.AchieveDB.AchieveList;

        foreach (var item in achievementDB)
        {
            AchieveData data = new AchieveData(item);
            if(achievements?.ContainsKey((AchievementType)item.AchievementType) == false)
            {
                achievements[(AchievementType)item.AchievementType] = new List<AchieveData>();
            }
            achievements[(AchievementType)item.AchievementType].Add(data);
        }
        foreach (KeyValuePair<AchievementType,List<AchieveData>> pair in achievements)
        {
            EventManager.Instance.Subscribe<AchieveEvent>(Channel.Achievement, OnTriggerAction);
        }
    }

    public void OnTriggerAction(AchieveEvent data)
    {
        
    }

    public List<AchieveData> MakeAchieveList(ActionType action, AchievementType type, int key)
    {
        return null;
    }
}
