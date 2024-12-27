using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot
{
    private IShopItem item;

    public void SetItem(IShopItem item)
    {
        this.item = item;
    }

    public IShopItem GetItem()
    {
        return this.item;
    }

    public void Clear()
    {
        this.item = null;
    }
}