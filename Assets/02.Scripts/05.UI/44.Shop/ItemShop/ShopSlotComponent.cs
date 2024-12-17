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

    private void Start()
    {
        slot = new ShopSlot();
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);
    }

    public void Select()
    {
        EventManager.Instance.Publish<ItemEvent>(Channel.Shop, itemEvent.SetEvent(slot.GetItem().key));
    }

    public void SetItem(SellItemDB data)
    {
        slot.SetItem(data);
        itemName.text = data.ProductName;
        itemPrice.text = data.Price.ToString() + data.PriceType;
    }
}