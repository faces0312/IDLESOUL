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
        itemStatusView.EquipButton.onClick.AddListener(EquipItem);

        //������ �������� ��ư �̺�Ʈ �Լ� ��� (�������� , UI ���)
        itemStatusView.DisEquipButton.onClick.AddListener(DisEquipItem);

        //������ ��ȭ�ϱ� ��ư �̺�Ʈ �Լ� ��� ( ������ ��ȭ�ϱ�, UI���)
        itemStatusView.UpgradeButton.onClick.AddListener(UpgradeItem);

        base.Initialize(itemStatusView, itemStatusModel);
    }

    private void EquipItem()
    {
        //�ش� �������� �����ϰ��������� ������ �� 
        if (SelectItem.item.IsGain)
        {
            GameManager.Instance.player.EquipItem(SelectItem.item);
            OnShow();
        }
    }

    private void DisEquipItem()
    {
        GameManager.Instance.player.DisEquipItem();
        OnShow();
    }

    private void UpgradeItem()
    {
        if (SelectItem.item.stack >= 2)
        {
            SelectItem.item.stack -= 2;

            SelectItem.item.ItemStat.maxHealth *= 2;
            SelectItem.item.ItemStat.atk *= 2;
            SelectItem.item.ItemStat.def *= 2;
            SelectItem.item.ItemStat.maxHealth *= 2;
            SelectItem.item.ItemStat.maxHealth *= 2;
            SelectItem.item.ItemStat.maxHealth *= 2;
        }

        UpdateView();
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
        itemStatusView.PrintData(SelectItem.item);

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
