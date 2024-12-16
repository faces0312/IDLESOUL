using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStageLabel : MonoBehaviour
{
    public string UIKey;

    private UIStageLabelModel model;
    [SerializeField] private UIStageLabelView views;
    private UIStageLabelController controller;

    private void Start()
    {
        model = new UIStageLabelModel();

        //컨트롤러  초기화 및 View 등록
        controller = new UIStageLabelController();
        controller.Initialize(views, model);

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
    }
}
