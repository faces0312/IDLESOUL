using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public Button Buy;
    public Button Cancel;
    public SellItemDB curItem;
    private TestInventoryModel inventory;

    private void OnEnable()
    {
        Name.text = curItem.ProductName;
        Description.text = curItem.ProductDescription;
        Buy.onClick.AddListener(BuyItem);
        Cancel.onClick.AddListener(Quit);
    }
    private void Start()
    {
    }

    public void SetItemDB(SellItemDB item)
    {
        this.curItem = item;
    }

    private void Quit()
    {
        this.gameObject.SetActive(false);
    }

    private void BuyItem()
    {
        //inventory.AddItem(curItem.key.ToString());
    }
}