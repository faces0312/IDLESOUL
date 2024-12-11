using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatusController : UIController
{
    public Item SelectItem;
    private ItemStatusModel itemStatusModel;
    private ItemStatusView itemStatusView;

    public override void Initialize(IUIBase view, UIModel model)
    {
        itemStatusModel = model as ItemStatusModel;
        itemStatusView = view as ItemStatusView;

        base.Initialize(itemStatusView, itemStatusModel);
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



        // Model 데이터를 기반으로 View 갱신
        view.UpdateUI();
    }
}
