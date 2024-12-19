using UnityEngine;

public class ConfigController : UIController
{
    private ConfigModel configModel;
    private ConfigView configView;

    public ConfigView View { get => configView; set => configView = value; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        configModel = model as ConfigModel;
        configView = view as ConfigView;
        base.Initialize(configView, configModel);
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