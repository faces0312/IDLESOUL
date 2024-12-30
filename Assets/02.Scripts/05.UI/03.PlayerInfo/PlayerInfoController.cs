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
        playerInfoModel.OnHpUpgrade += UpdateView;
        playerInfoModel.OnAtkUpgrade += UpdateAtk;
        playerInfoModel.OnAtkUpgrade += UpdateView;
        playerInfoModel.OnDefUpgrade += UpdateDef;
        playerInfoModel.OnDefUpgrade += UpdateView;
        playerInfoModel.OnReduceDmgUpgrade += UpdateReduceDmg;
        playerInfoModel.OnReduceDmgUpgrade += UpdateView;
        playerInfoModel.OnCritChanceUpgrade += UpdateCritChance;
        playerInfoModel.OnCritChanceUpgrade += UpdateView;
        playerInfoModel.OnCritDmgUpgrade += UpdateCritDmg;
        playerInfoModel.OnCritDmgUpgrade += UpdateView;
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
