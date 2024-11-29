using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public int ItemKey;
    public int Price;
    private ItemDB itemData;
    private Button click;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private ItemPanel itemPanel;
    

    private void Awake()
    {
        this.itemData = DataManager.Instance.ItemDB.GetByKey(ItemKey);
        click = GetComponent<Button>();
        click.onClick.AddListener(SendData);
    }

    public ShopSlot(int itemKey, int price)
    {
        this.ItemKey = itemKey;
    }

    private void SendData()
    {
        itemPanel.SetItemDB(itemData);
    }
}
