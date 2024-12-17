using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;
    private SellItemDB item;

    public void SetItem(SellItemDB item)
    {
        this.item = item;
    }

    public SellItemDB GetItem()
    {
        return item;
    }
}