public class AchievementController : UIController
{
    string key = "achieveController";
    private AchievementModel achievementModel;

    public override void Initialize(IUIBase view, UIModel model)
    {
        base.Initialize(view, model);

        achievementModel = model as AchievementModel;

    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void OnShow()
    {
        view.ShowUI();
        UpdateView();
    }

    public override void UpdateView()
    {
        view.UpdateUI();
    }
}