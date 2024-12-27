using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class ItemPanel : MonoBehaviour
{
    public SellItemDB CurItem;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Button confirm;
    [SerializeField] private Button cancel;
    private ItemShopController controller;

    private void Start()
    {
        controller = new ItemShopController();
        controller.ItemPanel = this.gameObject;
        UIManager.Instance.RegisterController(controller.key, controller);
        confirm.onClick.AddListener(() =>
        {
            switch (CurItem.PriceType)
            {
                case (int)PriceType.Diamond:
                    if (GameManager.Instance.player.UserData.Diamonds >= CurItem.Price)
                    {
                        GameManager.Instance.player.UserData.Diamonds -= CurItem.Price;
                        GameManager.Instance.player.Inventory.AddItem(CurItem.key);
                    }
                    break;
                case (int)PriceType.Gold:
                    if (GameManager.Instance.player.UserData.Gold >= CurItem.Price)
                    {
                        GameManager.Instance.player.UserData.Gold -= CurItem.Price;
                        GameManager.Instance.player.Inventory.AddItem(CurItem.key);
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
        if(this.gameObject.activeSelf == false) this.gameObject.SetActive(true);
        this.CurItem = DataManager.Instance.SellItemDB.GetByKey(item.Key);
        SetContent();
    }

    public void SetContent()
    {
        this.icon.sprite = Resources.Load<Sprite>(CurItem.IconPath);
        this._name.text = CurItem.ProductName;
        this.description.text = CurItem.ProductDescription;
    }
}

