using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerHPDisplay : MonoBehaviour
{
    public string UIKey;

    private UIPlayerHPDisplayModel model;
    [SerializeField] private UIPlayerHPDisplayView views;
    private UIPlayerHPDisplayController controller;

    private void Start()
    {
        model = new UIPlayerHPDisplayModel();

        //컨트롤러  초기화 및 View 등록
        controller = new UIPlayerHPDisplayController();
        controller.Initialize(views, model);

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
    }
}

