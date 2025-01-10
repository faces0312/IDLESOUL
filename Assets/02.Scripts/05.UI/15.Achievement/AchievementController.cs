public class AchievementController : UIController
{
    private AchievementModel achievementModel;
    private AchievementView achievementView;

    public override void Initialize(IUIBase view, UIModel model)
    {
        achievementModel = model as AchievementModel;
        achievementView = view as AchievementView;

        base.Initialize(achievementView, achievementModel);
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