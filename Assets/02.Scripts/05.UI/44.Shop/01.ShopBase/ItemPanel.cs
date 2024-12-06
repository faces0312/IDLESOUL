using TMPro;
using Unity.VisualScripting;
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
        if(DataManager.Instance.UserData.Gold >= curItem.Price)
        {
            DataManager.Instance.UserData.Gold -= curItem.Price;
            inventory.AddItem(curItem.key.ToString());
        }
    }
}