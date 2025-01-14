using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStageLabelController : UIController
{
    private UIStageLabelView stageLabelView;

    public override void OnShow()
    {
        view.ShowUI();
        UpdateView();   // �ʱ� View ����
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        // Model �����͸� ������� View ����
        view.UpdateUI();
    }

    public void SetStage(StageDB stage)
    {
        stageLabelView = view as UIStageLabelView;
        stageLabelView.SetUI(stage);
    }
}
