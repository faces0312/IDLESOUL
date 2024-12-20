using UnityEngine;

public class ItemShopController : UIController
{
    public string key = "itemShopController";
    public GameObject ItemPanel;

    public override void OnHide()
    {
        ItemPanel.SetActive(false);
    }

    public override void OnShow()
    {
        ItemPanel.SetActive(true);
    }

    public override void UpdateView()
    {
        
    }
}