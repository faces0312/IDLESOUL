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

    private void OnEnable()
    {
        EventManager.Instance.Subscribe<ItemEvent>(Channel.Shop, SetItem);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe<ItemEvent>(Channel.Shop, SetItem);
    }

    private void Start()
    {
        confirm.onClick.AddListener(() =>
        {
            switch (CurItem.PriceType)
            {
                case (int)PriceType.Diamond:
                    if (DataManager.Instance.UserData.Diamonds >= CurItem.Price)
                    {
                        DataManager.Instance.UserData.Diamonds -= CurItem.Price;
                        TestManager.Instance.inventory.AddItem(CurItem.key.ToString());
                    }
                    break;
                case (int)PriceType.Gold:
                    if (DataManager.Instance.UserData.Gold >= CurItem.Price)
                    {
                        DataManager.Instance.UserData.Gold -= CurItem.Price;
                        TestManager.Instance.inventory.AddItem(CurItem.key.ToString());
                    }
                    break;
            }
            TestManager.Instance.inventory.AddItem(CurItem.key.ToString());
        });

        cancel.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
    }

    public void SetItem(SellItemDB item)
    {
        this.CurItem = item;
        SetContent();
    }

    public void SetItem(ItemEvent item)
    {
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

