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

        //��Ʈ�ѷ�  �ʱ�ȭ �� View ���
        controller = new UIStageLabelController();
        controller.Initialize(views, model);

        //UI�Ŵ����� UI ���
        UIManager.Instance.RegisterController(UIKey, controller);
    }
}
