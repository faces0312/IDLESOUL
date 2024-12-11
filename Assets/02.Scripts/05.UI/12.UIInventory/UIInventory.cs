using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public string UIKey;

    private InventoryModel model;
    [SerializeField] private InventoryView[] views;
    private InventoryController controller;

    private void Start()
    {
        //Model(Data) 초기화
        model = new InventoryModel();
        model.Initilaize();
        GameManager.Instance._player.Inventory = model;

        //컨트롤러  초기화 및 View 등록
        controller = new InventoryController();
        for (int i = 0; i < views.Length; i++) 
        {
            views[i].Controller = controller;
            controller.Initialize(views[i], model);
        }

        //UI매니저에 UI 등록
        UIManager.Instance.RegisterController(UIKey, controller);
        gameObject.SetActive(false);
    }

    public void UIShow()
    {
        controller.OnShow();
    }
    
}
