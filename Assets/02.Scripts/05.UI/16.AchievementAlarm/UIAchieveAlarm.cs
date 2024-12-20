using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UIAchieveAlarm : MonoBehaviour
{
    public string UIKey; //"AchieveAlarm"

    private AchieveAlarmModel model;
    [SerializeField] private AchieveAlarmView view;
    private AchieveAlarmController controller;

    private void Start()
    {
        //Model(Data) 초기화
        model = new AchieveAlarmModel();

        //컨트롤러  초기화 및 View 등록
        controller = new AchieveAlarmController();
        controller.Initialize(view, model);

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
        gameObject.SetActive(false);
    }

}