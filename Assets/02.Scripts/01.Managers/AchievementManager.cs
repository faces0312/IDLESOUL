using System.Collections.Generic;
using Enums;
using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.UIElements;

public class AchievementManager : SingletonDDOL<AchievementManager>
{
    public Dictionary<AchievementType, List<AchieveData>> achievements; //도전과제 리스트
    private List<AchieveDB> achievementDB; //도전과제 DB
    private float time;

    //[SerializeField] private AchieveAlarm alarm;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (Mathf.Round(time) == 60)
        {
            EventManager.Instance.Publish<AchieveEvent>(Channel.Achievement, new AchieveEvent(AchievementType.Time, ActionType.Time, 60));
            time = 0f;
        }
    }

    public void Init()
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

            UserAchieveData userAchieveData = GameManager.Instance.player.UserData.UsersAchieveData.Find(x => data.ID == x.ID);

            if (userAchieveData != null)
            {
                data.progress = userAchieveData.Progress;
                data.isClear = userAchieveData.IsClear;
                data.isPublished = userAchieveData.IsPublish;
                achievements[(AchievementType)item.AchievementType].Add(data);
            }
            else
            {
                achievements[(AchievementType)item.AchievementType].Add(data);
            }
        }
        EventManager.Instance.Subscribe<AchieveEvent>(Channel.Achievement, OnTriggerAction);

        
    }

    public void OnTriggerAction(AchieveEvent data)
    {
        if ((int)data.Action == 0 || (int)data.Type == 0) return;
        foreach (AchieveData aData in TransferList(data.Type, data.Action))
        {
            if (data.Type == AchievementType.Clear)
            {
                aData.AddProgress(1);
            }
            else aData.AddProgress(data.Value);
            if (aData.isClear == true && aData.isPublished == false)
            {
                aData.isPublished = true;
                StartCoroutine(CoAlarm(aData));
            }
        }
    }

    private List<AchieveData> TransferList(AchievementType type, ActionType action)
    {
        List<AchieveData> tempList = new List<AchieveData>();
        foreach (AchieveData aData in AchievementManager.Instance.achievements[type])
        {
            if(aData.Action == action)
            {
                tempList.Add(aData);
            }
        }
        return tempList;
    }

    public IEnumerator CoAlarm(AchieveData data)
    {
        UIManager.Instance.GetController<AchieveAlarmController>().AchieveAlarmView.SetContent(data);
        UIManager.Instance.ShowUI<AchieveAlarmController>();
        yield return Wait.Wait3s;
        UIManager.Instance.HideUI<AchieveAlarmController>();
    }

    public AchieveData GetByKey(AchievementType type, int key)
    {
        return achievements[type].First((data) => data.ID == key);
    }
}
