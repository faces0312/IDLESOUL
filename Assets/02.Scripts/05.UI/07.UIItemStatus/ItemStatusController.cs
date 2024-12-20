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

        base.Initialize(itemStatusView, itemStatusModel);
        //아이템 장착 버튼 이벤트 함수 등록 (장착 , UI 출력)
        itemStatusView.EquipButton.onClick.AddListener(() => GameManager.Instance.player.EquipItem(SelectItem.item));
        itemStatusView.EquipButton.onClick.AddListener(() => OnShow());

        //아이템 장착해제 버튼 이벤트 함수 등록 (장착해제 , UI 출력)
        itemStatusView.DisEquipButton.onClick.AddListener(() => GameManager.Instance.player.DisEquipItem());
        itemStatusView.DisEquipButton.onClick.AddListener(() => OnShow());
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
        itemStatusView.PrintData(SelectItem.item.ItemData);

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
