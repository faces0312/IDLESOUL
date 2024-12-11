using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemStatus : MonoBehaviour
{
    public string UIKey;

    private ItemStatusModel model;
    [SerializeField] private ItemStatusView views;
    private ItemStatusController controller;

    private void Start()
    {
        //Model(Data) 초기화
        model = new ItemStatusModel();
        //model.Initilaize();

        //컨트롤러  초기화 및 View 등록
        controller = new ItemStatusController();
        controller.Initialize(views, model);

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
        gameObject.SetActive(false);
    }

}
