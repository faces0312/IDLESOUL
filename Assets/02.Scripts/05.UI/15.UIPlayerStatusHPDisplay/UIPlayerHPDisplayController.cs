using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class UIPlayerHPDisplayController : UIController
{
    private UIPlayerHPDisplayModel playerHpDisplayModel;
    private UIPlayerHPDisplayView playerHpDisplayView;

    public override void Initialize(IUIBase view, UIModel model)
    {
        playerHpDisplayModel = model as UIPlayerHPDisplayModel;
        playerHpDisplayView = view as UIPlayerHPDisplayView;

        base.Initialize(playerHpDisplayView, playerHpDisplayModel);
        OnShow();
    }

    public override void OnShow()
    {
        playerHpDisplayModel.Update();
        UpdateView();   // 초기 View 갱신
        view.ShowUI();
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        playerHpDisplayView.HpRatioChange(playerHpDisplayModel.CurHealth, playerHpDisplayModel.MaxHealth);
        view.UpdateUI();
    }
}
