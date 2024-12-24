//UI에 적용되는 도전과제 MVC 초기화 클래스

public class UIAchievement : UIBase<AchievementModel, AchievementView, AchievementController>
{
    public override void Start()
    {
        //Model(Data) 초기화
        model = new AchievementModel();
        base.Start();
        gameObject.SetActive(false);
    }
}

