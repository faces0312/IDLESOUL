using Enums;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    private GachaType GachaType = GachaType.Soul;

    [SerializeField] private Button content_Gacha;
    [SerializeField] private GameObject gachaPanel;

    [SerializeField] private Button content_BuyItem;
    [SerializeField] private Button content_ChangeProduct;
    [SerializeField] private GameObject buyItemPanel;

    [SerializeField] private Button gachaType_Soul;
    [SerializeField] private Button gachaType_Weapon;
    public Image pickupImage;

    [SerializeField] private Button gachaOnce;
    [SerializeField] private Button gacha10;
    private ShopModel shop;

    private UserData userData; //= DataManager.Instance.UserData
    [SerializeField] private ItemPanel buyPanel;

    private void Start()
    {
        shop = GetComponent<ShopModel>();

        content_Gacha.onClick.AddListener(BuyItemToGacha);
        content_BuyItem.onClick.AddListener(GachaToBuyItem);
        content_ChangeProduct.onClick.AddListener(GachaToBuyItem);
        gachaType_Soul.onClick.AddListener(SoulGacha);
        gachaType_Weapon.onClick.AddListener(ItemGacha);
        gachaOnce.onClick.AddListener(PlayGachaOnce);
        gacha10.onClick.AddListener(PlayGacha10);


    }

    private void GachaToBuyItem()
    {
        if (gachaPanel.activeSelf == true)
        {
            gachaPanel.SetActive(false);
        }
        buyItemPanel.SetActive(true);
    }

    private void BuyItemToGacha()
    {
        if (buyItemPanel.activeSelf == true)
        {
            buyItemPanel.SetActive(false);
        }
        gachaPanel.SetActive(true);
    }

    private void SoulGacha()
    {
        GachaType = GachaType.Soul;
        pickupImage.sprite = Resources.Load<Sprite>("Prefabs/Sample/CurSoulPickup");
    }

    private void ItemGacha()
    {
        GachaType = GachaType.Weapon;
        pickupImage.sprite = Resources.Load<Sprite>("Prefabs/Sample/CurItemPickup");
    }

    private void PlayGachaOnce()
    {
        if(userData.Diamonds >= shop.GachaPrice)
        {
            userData.Diamonds -= shop.GachaPrice;
            switch (GachaType)
            {
                case GachaType.Soul:
                    shop.DiamondGacha<testSoul>("SSS");
                    break;
                case GachaType.Weapon:
                    shop.DiamondGacha<testItem>("GoldenSword");
                    break;
            }
        }
    }

    private void PlayGacha10()
    {
        if(userData.Diamonds >= shop.GachaPrice * 10)
        {
            userData.Diamonds -= shop.GachaPrice * 10;
            for(int i = 0; i < 10; i++)
            switch (GachaType)
            {
                case GachaType.Soul:
                    shop.DiamondGacha<testSoul>("SSS");
                    break;
                case GachaType.Weapon:
                    shop.DiamondGacha<testItem>("GoldenSword");
                    break;
            }
        }
    }
}