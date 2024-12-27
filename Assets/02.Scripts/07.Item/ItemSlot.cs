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
    [SerializeField] private Image itemIconImage; // UI Image ����
    [SerializeField] private TextMeshProUGUI Name; //
    [SerializeField] private GameObject EquipCheck; //Player�� �����ϰ� �ִ� ������ üũ ��ũ
    [SerializeField] private GameObject isGainMask; //Player�� Inventory�� �����ϰ� �ִ� ������ üũ ��ũ

    public GameObject IsGainMask { get => isGainMask; }

    public void Initiliaze(Item item)
    {
        this.item = item;
        Name.text = item.ItemData.Name;
        itemIconImage.sprite = Resources.Load<Sprite>(item.ItemData.IconPath);

        //������ ���� ���ο� ���� �����۽��� ����ũ ó��
        if (!item.IsGain)
        {
            isGainMask.SetActive(true);
        }
        else
        {
            isGainMask.SetActive(false);
        }

        //������ ���� ���� Ȯ�� 
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
