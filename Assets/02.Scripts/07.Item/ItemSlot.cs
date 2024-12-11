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
    [SerializeField] private Image itemIconImage; // UI Image ÂüÁ¶
    [SerializeField] private TextMeshProUGUI Name; //

    public void Initiliaze(Item item)
    {
        this.item = item;
        Name.text = item.ItemData.Name;
        itemIconImage.sprite = Resources.Load<Sprite>(item.ItemData.IconPath);

        itemStatusOpenPopUpBtn.onClick.AddListener(ItemStatusData);
        itemStatusOpenPopUpBtn.onClick.AddListener(() => UIManager.Instance.UpdateUI("ItemStatus"));
        itemStatusOpenPopUpBtn.onClick.AddListener(() => UIManager.Instance.ShowUI("ItemStatus"));
    }

    private void ItemStatusData()
    {
        UIManager.Instance.UpdateUI("ItemStatus");
    }
}
