using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventoryController : UIController
{
    private TestInventoryModel inventoryModel;

    public override void Initialize(IUIBase view, UIModel model)
    {
        base.Initialize(view, model);

        inventoryModel = model as TestInventoryModel;

        inventoryModel.OnInventoryChanged += UpdateView;
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
