using System.Collections.Generic;
using Enums;
using UnityEngine;
using System.Collections;

public class AchievementManager : SingletonDDOL<AchievementManager>
{
    public Dictionary<AchievementType, List<AchieveData>> achievements; //도전과제 리스트
    private List<AchieveDB> achievementDB; //도전과제 DB
    public List<AchieveData> aDatas = new List<AchieveData>();

    //[SerializeField] private AchieveAlarm alarm;

    protected override void Awake()
    {
        base.Awake();
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
            if(aData.isClear == true && aData.isPublished == false)
            {
                aData.isPublished = true;
                StartCoroutine(CoAlarm(aData));
            }
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

    public IEnumerator CoAlarm(AchieveData data)
    {
        AchieveAlarmController controller = UIManager.Instance.GetController("AchieveAlarm") as AchieveAlarmController;
        controller.AchieveAlarmView.SetContent(data);
        UIManager.Instance.ShowUI("AchieveAlarm");
        yield return Wait.Wait3s;
        UIManager.Instance.HideUI("AchieveAlarm");
        //alarm.gameObject.SetActive(true);
        //alarm.SetContent(data);
        //yield return Wait.Wait3s;
        //alarm.gameObject.SetActive(false);
    }
}
