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
        playerInfoModel.OnHpUpgrade += UpdateHp;
        playerInfoModel.OnAtkUpgrade += UpdateAtk;
        playerInfoModel.OnDefUpgrade += UpdateDef;
        playerInfoModel.OnReduceDmgUpgrade += UpdateReduceDmg;
        playerInfoModel.OnCritChanceUpgrade += UpdateCritChance;
        playerInfoModel.OnCritDmgUpgrade += UpdateCritDmg;
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

    public void UpdateHp()
    {
        if (view is PlayerInfoView infoView)
        {
            infoView.UpdateHp();
        }
    }

    public void UpdateAtk()
    {
        if (view is PlayerInfoView infoView)
        {
            infoView.UpdateAtk();
        }
    }

    public void UpdateDef()
    {
        if (view is PlayerInfoView infoView)
        {
            infoView.UpdateDef();
        }
    }

    public void UpdateReduceDmg()
    {
        if (view is PlayerInfoView infoView)
        {
            infoView.UpdateReduceDmg();
        }
    }

    public void UpdateCritChance()
    {
        if (view is PlayerInfoView infoView)
        {
            infoView.UpdateCritChance();
        }
    }

    public void UpdateCritDmg()
    {
        if (view is PlayerInfoView infoView)
        {
            infoView.UpdateCritDmg();
        }
    }
}
