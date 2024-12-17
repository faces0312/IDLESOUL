
using UnityEngine;

public class UIAchievement : MonoBehaviour
{
    public string UIKey;

    private AchievementModel model;
    [SerializeField] private AchievementView view;
    private AchievementController controller;

    private void Start()
    {
        //Model(Data) 초기화
        model = new AchievementModel();
    
        //컨트롤러  초기화 및 View 등록
        controller = new AchievementController();
        controller.Initialize(view, model);

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
        gameObject.SetActive(false);
    }
}

