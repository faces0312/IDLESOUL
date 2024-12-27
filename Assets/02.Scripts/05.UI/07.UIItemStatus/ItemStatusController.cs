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

        //아이템 장착 버튼 이벤트 함수 등록 (장착 , UI 출력)
        itemStatusView.EquipButton.onClick.AddListener(EquipItem);

        //아이템 장착해제 버튼 이벤트 함수 등록 (장착해제 , UI 출력)
        itemStatusView.DisEquipButton.onClick.AddListener(DisEquipItem);

        //아이템 강화하기 버튼 이벤트 함수 등록 ( 아이템 강화하기, UI출력)
        itemStatusView.UpgradeButton.onClick.AddListener(UpgradeItem);

        base.Initialize(itemStatusView, itemStatusModel);
    }

    private void EquipItem()
    {
        //해당 아이템을 소지하고있을때만 장착이 됨 
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
        UpdateView();   // 초기 View 갱신
        view.ShowUI();
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        itemStatusView.PrintData(SelectItem.item);

        //플레이어에 장착된 아이템이 있는지 null체크
        if (GameManager.Instance.player.IsEquipItem == null)
        {
            EquipButtonViewUpdate();
        }
        else
        {
            //플레이어가 끼고 있는 아이템과 아이템 정보창에 있는 아이템 비교하기(Key비교)
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
