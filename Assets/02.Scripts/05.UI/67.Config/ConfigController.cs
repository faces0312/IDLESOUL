using UnityEngine;

public class ConfigController : UIController
{
    public readonly string key = "ConfigController";

    private ConfigView configView;

    public ConfigView View { get => configView; set => configView = value; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        configView = view as ConfigView;
        base.Initialize(view, model);
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void OnShow()
    {
        view.ShowUI();
    }

    public override void UpdateView()
    {
        view.UpdateUI();
    }

    public void SetFPS()
    {
        
    }
}