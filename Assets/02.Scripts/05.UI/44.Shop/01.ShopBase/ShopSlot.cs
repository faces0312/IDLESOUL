using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public int ItemKey;
    public int Price;
    private SellItemDB itemData;
    private Button click;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private ItemPanel itemPanel;
    

    private void Awake()
    {
        //this.itemData = DataManager.Instance.SellItemDB.GetByKey(ItemKey);
        click = GetComponent<Button>();
        click.onClick.AddListener(SendData);
        //itemIcon.sprite = Resources.Load<Sprite>(itemData.IconPath);
    }

    public ShopSlot(int itemKey, int price)
    {
        this.ItemKey = itemKey;
    }

    private void SendData()
    {
        itemPanel.gameObject.SetActive(true);
        itemPanel.SetItemDB(itemData);
    }
}
