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
        //Model(Data) �ʱ�ȭ
        model = new ItemStatusModel();

        //��Ʈ�ѷ�  �ʱ�ȭ �� View ���
        controller = new ItemStatusController();
        controller.Initialize(views, model);

        //UI�Ŵ����� UI ���
        UIManager.Instance.RegisterController(UIKey, controller);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"{controller.SelectItem}");
        }
    }
}
