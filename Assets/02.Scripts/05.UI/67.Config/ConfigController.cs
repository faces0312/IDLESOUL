public class ConfigController : UIController
{
    public readonly string key = "configController";

    private ConfigModel configModel;
    private ConfigView configView;

    public ConfigModel Model { get => configModel; set => configModel = value; }
    public ConfigView View { get => configView; set => configView = value; }

    public override void Initialize(IUIBase view, UIModel model)
    {
        configView = view as ConfigView;
        configModel = model as ConfigModel;
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
}