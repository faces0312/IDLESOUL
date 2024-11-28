using UnityEngine;

public class ShopView : MonoBehaviour, UIBase
{
    public GameObject ShopPanel;

    public void HideUI()
    {
        ShopPanel.SetActive(false);
    }

    public void Initialize()
    {
        
    }

    public void ShowUI()
    {
        ShopPanel.SetActive(true);
    }

    public void UpdateUI()
    {
        
    }
}
