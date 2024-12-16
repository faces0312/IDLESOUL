using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : UIController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;
    public InventoryModel Model { get => inventoryModel; set => inventoryModel = value; }
    public InventoryView View { get => inventoryView; set => inventoryView = value; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        inventoryModel = model as InventoryModel;
        inventoryView = view as InventoryView;

        base.Initialize(inventoryView, inventoryModel);
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
