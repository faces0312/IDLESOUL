using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    [SerializeField] private Button itemStatusOpenPopUpBtn;
    [SerializeField] private Image itemIconImage; // UI Image 참조
    [SerializeField] private TextMeshProUGUI Name; //
    [SerializeField] private GameObject EquipCheck; //Player가 장착하고 있는 아이템 체크 마크
    [SerializeField] private GameObject isGainMask; //Player가 Inventory에 소지하고 있는 아이템 체크 마크

    public GameObject IsGainMask { get => isGainMask; }

    public void Initiliaze(Item item)
    {
        this.item = item;
        Name.text = item.ItemData.Name;
        itemIconImage.sprite = Resources.Load<Sprite>(item.ItemData.IconPath);

        //아이템 소유 여부에 따라 아이템슬릇 마스크 처리
        if (!item.IsGain)
        {
            isGainMask.SetActive(true);
        }
        else
        {
            isGainMask.SetActive(false);
        }

        //아이템 장착 여부 확인 
        if (item.equip)
        {
            EquipCheck.SetActive(true);
        }
        else
        {
            EquipCheck.SetActive(false);
        }

        itemStatusOpenPopUpBtn.onClick.AddListener(ItemStatusDataShowUI);
    }

    public void UIUpdate()
    {
        if(item.IsGain)
        {
            IsGainItemMaskOff();
        }

        if (GameManager.Instance.player.IsEquipItem != null &&
            GameManager.Instance.player.IsEquipItem.ItemData.key == item.ItemData.key)
        {
            EquipCheck.gameObject.SetActive(true);
        }
        else
        {
            EquipCheck.gameObject.SetActive(false);
        }
    }

    private void ItemStatusDataShowUI()
    {
        UIManager.Instance.GetController<ItemStatusController>().SelectItem = this;
        UIManager.Instance.ShowUI<ItemStatusController>();
    }

    public void IsGainItemMaskOff()
    {
        isGainMask.SetActive(false);
    }

}
