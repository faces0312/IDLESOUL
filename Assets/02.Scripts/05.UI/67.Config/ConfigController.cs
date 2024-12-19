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
        configView.fps30.onValueChanged.AddListener(SetFPSTo30);
        configView.fps60.onValueChanged.AddListener(SetFPSTo60);
    }

    public override void UpdateView()
    {
        view.UpdateUI();
    }

    public void SetFPSTo30(bool isOn)
    {
        if(isOn == true)
        {
            configView.fps60.isOn = false;
            Application.targetFrameRate = 30;
        }
    }

    public void SetFPSTo60(bool isOn)
    {
        if (isOn == true)
        {
            configView.fps30.isOn = false;
            Application.targetFrameRate = 60;
        }
    }
}