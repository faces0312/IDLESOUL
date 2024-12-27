using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class ItemPanel : MonoBehaviour
{
    public IShopItem CurItem;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Button confirm;
    [SerializeField] private Button cancel;
    private ItemShopController controller;
    private ShopType shopType;
    private int tempKey;

    private void Start()
    {
        controller = new ItemShopController();
        controller.ItemPanel = this.gameObject;
        UIManager.Instance.RegisterController(controller.key, controller);
        confirm.onClick.AddListener(() =>
        {
            switch (CurItem.GetPriceType())
            {
                case PriceType.Diamond:
                    if (GameManager.Instance.player.UserData.Diamonds >= CurItem.GetPrice())
                    {
                        GameManager.Instance.player.UserData.Diamonds -= CurItem.GetPrice();
                        if (this.shopType == ShopType.Item) GameManager.Instance.player.Inventory.AddItem(tempKey);
                        else if (this.shopType == ShopType.Product) GameManager.Instance.player.UserData.Gold += DataManager.Instance.ExchangeDB.GetByKey(tempKey).Product;

                    }
                    break;
                case PriceType.Gold:
                    if (GameManager.Instance.player.UserData.Gold >= CurItem.GetPrice())
                    {
                        GameManager.Instance.player.UserData.Gold -= CurItem.GetPrice();
                        if (this.shopType == ShopType.Item) GameManager.Instance.player.Inventory.AddItem(tempKey);
                        else if (this.shopType == ShopType.Product) GameManager.Instance.player.UserData.Diamonds += DataManager.Instance.ExchangeDB.GetByKey(tempKey).Product;
                    }
                    break;
            }
        });

        cancel.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        this.gameObject.SetActive(false);

        EventManager.Instance.Subscribe<ItemEvent>(Channel.Shop, SetItem);
    }

    public void SetItem(SellItemDB item)
    {
        this.CurItem = item;
        SetContent();
    }

    public void SetItem(ItemEvent item)
    {
        this.gameObject.SetActive(true);
        switch (item.ShopType)
        {
            case ShopType.Item:
                this.CurItem = DataManager.Instance.SellItemDB.GetByKey(item.Key);
                break;
            case ShopType.Product:
                this.CurItem = DataManager.Instance.ExchangeDB.GetByKey(item.Key);
                break;
        }
        this.shopType = item.ShopType;
        this.tempKey = item.Key;
        SetContent();
    }

    public void SetContent()
    {
        this.icon.sprite = Resources.Load<Sprite>(CurItem.GetIconPath());
        this._name.text = CurItem.GetName();
        this.description.text = CurItem.GetDescription();
    }
}

