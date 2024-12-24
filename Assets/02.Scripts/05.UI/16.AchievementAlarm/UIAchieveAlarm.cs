using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UIAchieveAlarm : UIBase<AchieveAlarmModel, AchieveAlarmView, AchieveAlarmController>
{

    public override void Start()
    {
        //Model(Data) 초기화
        model = new AchieveAlarmModel();
        base.Start();
        gameObject.SetActive(false);
    }

}