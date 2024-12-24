using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatusController : UIController
{
    public ItemSlot SelectItem;
    private ItemStatusModel itemStatusModel;
    private ItemStatusView itemStatusView;

    public override void Initialize(IUIBase view, UIModel model)
    {
        itemStatusModel = model as ItemStatusModel;
        itemStatusView = view as ItemStatusView;

        //������ ���� ��ư �̺�Ʈ �Լ� ��� (���� , UI ���)
        itemStatusView.EquipButton.onClick.AddListener(() => GameManager.Instance.player.EquipItem(SelectItem.item));
        itemStatusView.EquipButton.onClick.AddListener(() => OnShow());

        //������ �������� ��ư �̺�Ʈ �Լ� ��� (�������� , UI ���)
        itemStatusView.DisEquipButton.onClick.AddListener(() => GameManager.Instance.player.DisEquipItem());
        itemStatusView.DisEquipButton.onClick.AddListener(() => OnShow());

        base.Initialize(itemStatusView, itemStatusModel);
    }

    public override void OnShow()
    {
        UpdateView();   // �ʱ� View ����
        view.ShowUI();
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        itemStatusView.PrintData(SelectItem.item.ItemData);

        //�÷��̾ ������ �������� �ִ��� nullüũ
        if (GameManager.Instance.player.IsEquipItem == null)
        {
            EquipButtonViewUpdate();
        }
        else
        {
            //�÷��̾ ���� �ִ� �����۰� ������ ����â�� �ִ� ������ ���ϱ�(Key��)
            if (GameManager.Instance.player.IsEquipItem.ItemData.key == SelectItem.item.ItemData.key)
            {
                DisEquipButtonViewUpdate();
            }
            else
            {
                EquipButtonViewUpdate();
            }
        }

        UIManager.Instance.ShowUI<InventoryController>();

        view.UpdateUI();
    }

    public void EquipButtonViewUpdate()
    {
        itemStatusView.EquipButton.gameObject.SetActive(true);
        itemStatusView.DisEquipButton.gameObject.SetActive(false);
    }

    public void DisEquipButtonViewUpdate()
    {
        itemStatusView.EquipButton.gameObject.SetActive(false);
        itemStatusView.DisEquipButton.gameObject.SetActive(true);
    }
}
