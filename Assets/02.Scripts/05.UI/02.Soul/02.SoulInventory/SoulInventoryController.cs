using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInventoryController : UIController
{
    private SoulInventoryModel soulInventoryModel;

    public override void Initialize(IUIBase view, UIModel model)
    {
        base.Initialize(view, model);

        soulInventoryModel = model as SoulInventoryModel;
        soulInventoryModel.OnInventoryChanged += UpdateView;
    }

    public override void OnShow()
    {
        view.ShowUI();
        UpdateView();
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
