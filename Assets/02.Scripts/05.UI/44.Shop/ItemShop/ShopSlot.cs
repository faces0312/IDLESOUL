using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot
{
    private SellItemDB item;

    public void SetItem(SellItemDB item)
    {
        this.item = item;
    }

    public SellItemDB GetItem()
    {
        return this.item;
    }
}