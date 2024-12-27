using UnityEngine;
using UnityEngine.UI;
using Enums;
using TMPro;

public class ShopSlotComponent : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;

    public ShopSlot slot;
    private ItemEvent itemEvent;

    public void Init()
    {
        slot = new ShopSlot();
        itemEvent = new ItemEvent();
        button.onClick.AddListener(Select);
    }

    public void Select()
    {
        if(slot.GetItem().GetKey() >= 20000)
        {
            EventManager.Instance.Publish<ItemEvent>(Channel.Shop, itemEvent.SetEvent(slot.GetItem().GetKey(), ShopAction.Buy, ShopType.Product));
        }
        else
        {
            EventManager.Instance.Publish<ItemEvent>(Channel.Shop, itemEvent.SetEvent(slot.GetItem().GetKey(), ShopAction.Buy, ShopType.Item));
        }
    }

    public void SetItem(IShopItem data)
    {
        slot.SetItem(data);
        itemName.text = data.GetName();
        itemPrice.text = data.GetPrice().ToString() + data.GetPriceType().ToString();
    }

    public void Clear()
    {
        slot.Clear();
        itemName.text = "";
        itemPrice.text = "";
    }
}