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
