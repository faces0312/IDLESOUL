using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoController : UIController
{
    private PlayerInfoModel playerInfoModel;

    public override void Initialize(IUIBase view, UIModel model)
    {
        base.Initialize(view, model);

        playerInfoModel = model as PlayerInfoModel;
        playerInfoModel.OnInfoChanged += UpdateView;
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
