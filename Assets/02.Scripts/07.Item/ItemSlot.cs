using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using System;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    [SerializeField] private Button itemStatusOpenPopUpBtn;
    [SerializeField] private Image itemIconImage; // UI Image ����
    [SerializeField] private TextMeshProUGUI Name; //
    [SerializeField] private GameObject EquipCheck; //Player�� �����ϰ� �ִ� ������ üũ ��ũ
    [SerializeField] private GameObject IsGainMask; //Player�� Inventory�� �����ϰ� �ִ� ������ üũ ��ũ

   public void Initiliaze(Item item)
    {
        this.item = item;
        Name.text = item.ItemData.Name;
        itemIconImage.sprite = Resources.Load<Sprite>(item.ItemData.IconPath);

        IsGainMask.gameObject.SetActive(false);
        EquipCheck.gameObject.SetActive(false);

        itemStatusOpenPopUpBtn.onClick.AddListener(ItemStatusData);
        itemStatusOpenPopUpBtn.onClick.AddListener(() => UIManager.Instance.ShowUI("ItemStatus"));
    }

    public void UIUpdate()
    {
        if(GameManager.Instance.player.IsEquipItem != null &&
            GameManager.Instance.player.IsEquipItem.ItemData.key == item.ItemData.key)
        {
            EquipCheck.gameObject.SetActive(true);   
        }
        else
        {
            EquipCheck.gameObject.SetActive(false);
        }
    }

    private void ItemStatusData()
    {
       ItemStatusController itemStatus = UIManager.Instance.GetController("ItemStatus") as ItemStatusController;
       itemStatus.SelectItem = this;
    }
}
