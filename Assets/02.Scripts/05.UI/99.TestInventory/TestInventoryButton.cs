using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInventoryButton : MonoBehaviour
{
    [SerializeField] private TestInventoryView view;
    private TestInventoryController inventoryController;
    private Button button;
    private string uiKey;

    private void Awake()
    {
        button = GetComponent<Button>();
        uiKey = "Inventory";
    }

    private void Start()
    {
        button.onClick.AddListener(()=> OnClickButton(uiKey));
    }

    private void OnClickButton(string key)
    {
        if (inventoryController == null)
        {
            var inventoryModel = new TestInventoryModel();
            // 플레이어가 모델 데이터가 필요하면 소지하게 함
            inventoryController = new TestInventoryController();
            inventoryController.Initialize(view, inventoryModel);
            UIManager.Instance.RegisterController(key, inventoryController);
        }

        UIManager.Instance.ShowUI(key);
    }
}
