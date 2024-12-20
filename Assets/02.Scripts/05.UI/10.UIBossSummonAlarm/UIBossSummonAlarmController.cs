using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class UIBossSummonAlarmController : UIController
{
    private UIBossSummonAlarmModel bossSummonAlarmModel;
    private UIBossSummonAlarmView bossSummonAlarmView;

    public UIBossSummonAlarmView BossSummonAlarmView { get => bossSummonAlarmView;  }

    public override void Initialize(IUIBase view, UIModel model)
    {
        bossSummonAlarmModel = model as UIBossSummonAlarmModel;
        bossSummonAlarmView = view as UIBossSummonAlarmView;

        base.Initialize(bossSummonAlarmView, bossSummonAlarmModel);
    }

    public override void OnShow()
    {
        UpdateView();   // 초기 View 갱신
        view.ShowUI();
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        view.UpdateUI();
    }
}
