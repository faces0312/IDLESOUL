using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBase : MonoBehaviour
{
    public string UIKey; // shopController

    private ShopModel model;
    [SerializeField] private ShopView views;
    private ShopController controller;

    private void Start()
    {
        model = new ShopModel();

        //컨트롤러  초기화 및 View 등록
        controller = new ShopController();
        controller.Initialize(views, model);

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
    }
}

