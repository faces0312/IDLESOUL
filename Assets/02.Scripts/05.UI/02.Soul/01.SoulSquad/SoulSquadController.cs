using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSquadController : UIController
{
    private SoulSquadModel soulSquadModel;
    public SoulSquadModel SoulSquadModel { get => soulSquadModel; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        base.Initialize(view, model);

        soulSquadModel = model as SoulSquadModel;
        soulSquadModel.OnSquadChanged += UpdateView;
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
