using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICurGainKeyCountController : UIController
{
    private UICurGainKeyCountModel curGainKeyModel;
    private UICurGainKeyCountView curGainKeyView;
    public UICurGainKeyCountModel Model { get => curGainKeyModel; set => curGainKeyModel = value; }
    public UICurGainKeyCountView View { get => curGainKeyView; set => curGainKeyView = value; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        curGainKeyModel = model as UICurGainKeyCountModel;
        curGainKeyView = view as UICurGainKeyCountView;

        base.Initialize(curGainKeyView, curGainKeyModel);
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
