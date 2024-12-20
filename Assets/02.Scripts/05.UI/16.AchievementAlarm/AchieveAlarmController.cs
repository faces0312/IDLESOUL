using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchieveAlarmController : UIController
{
    private AchieveAlarmModel achieveAlarmModel;
    private AchieveAlarmView achieveAlarmView;

    public AchieveAlarmView AchieveAlarmView { get => achieveAlarmView;  }

    public override void Initialize(IUIBase view, UIModel model)
    {
        achieveAlarmModel = model as AchieveAlarmModel;
        achieveAlarmView = view as AchieveAlarmView;

        base.Initialize(achieveAlarmView, achieveAlarmModel);
    }

    public override void OnShow()
    {
        view.ShowUI();
        UpdateView();   // 초기 View 갱신
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        // Model 데이터를 기반으로 View 갱신
        view.UpdateUI();
    }



}