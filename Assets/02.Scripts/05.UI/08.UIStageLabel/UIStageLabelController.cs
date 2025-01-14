using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStageLabelController : UIController
{
    private UIStageLabelView stageLabelView;

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

    public void SetStage(StageDB stage)
    {
        stageLabelView = view as UIStageLabelView;
        stageLabelView.SetUI(stage);
    }
}
