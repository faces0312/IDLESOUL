using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBossSummonAlarm : MonoBehaviour
{
    public string UIKey; // "BossSummonAlarm"

    private UIBossSummonAlarmModel model;
    [SerializeField] private UIBossSummonAlarmView views;
    private UIBossSummonAlarmController controller;

    private void Start()
    {
        model = new UIBossSummonAlarmModel();
        //model.Init();

        //컨트롤러  초기화 및 View 등록
        controller = new UIBossSummonAlarmController();
        controller.Initialize(views, model);

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
        controller.OnHide();
    }
}

