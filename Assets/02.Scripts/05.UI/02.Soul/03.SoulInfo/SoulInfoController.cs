using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInfoController : UIController
{
    private SoulInfoModel soulInfoModel;
    public SoulInfoModel SoulInfoModel { get => soulInfoModel; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        base.Initialize(view, model);

        soulInfoModel = model as SoulInfoModel;
        soulInfoModel.OnInfoChanged += UpdateView;
        soulInfoModel.OnDefaultSkillChanged += UpdateDefault;
        soulInfoModel.OnUltimateSkillChanged += UpdateUltimate;
        soulInfoModel.OnPassiveSkillChanged += UpdatePassive;
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

    public void UpdateDefault()
    {
        if (view is SoulInfoView infoView)
        {
            infoView.UpdateDefault();
        }
    }

    public void UpdateUltimate()
    {
        if (view is SoulInfoView infoView)
        {
            infoView.UpdateUltimate();
        }
    }

    public void UpdatePassive()
    {
        if (view is SoulInfoView infoView)
        {
            infoView.UpdatePassive();
        }
    }
}
