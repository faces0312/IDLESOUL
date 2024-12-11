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
        UpdateView();   // �ʱ� View ����
        view.ShowUI();
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
}
