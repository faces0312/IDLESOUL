using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button exitButton;

    [SerializeField] private Button gachaButton;
    [SerializeField] private GameObject gachaPanel;

    [SerializeField] private Button itemButton;
    [SerializeField] private GameObject itemPanel;

    [SerializeField] private Button moneyButton;

    //private ShopController shopController;

    private void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            HideUI();
        });

        gachaButton.onClick.AddListener(() =>
        {
            gachaPanel.SetActive(true);
            itemPanel.SetActive(false);
        });

        itemButton.onClick.AddListener(() =>
        {
            gachaPanel.SetActive(false);
            itemPanel.SetActive(true);
            itemPanel.GetComponent<ShopGrid>().SetItem(Enums.ShopType.Item);
        });

        moneyButton.onClick.AddListener(() =>
        {
            gachaPanel.SetActive(false);
            itemPanel.SetActive(true);
        });
    }

    public void HideUI()
    {
        shopPanel.SetActive(false);
    }

    public void Initialize()
    {
    }

    public void ShowUI()
    {
        shopPanel.SetActive(true);
        UpdateUI();
    }

    public void UpdateUI()
    {
        gachaPanel.SetActive(true);
        itemPanel.SetActive(false);
    }
}