using Enums;
using System;
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

    [SerializeField] private ItemPanel buyPanel;
    [SerializeField] private Button exitButton;

    private GachaEvent gachaEvent;

    private void Start()
    {
        content_Gacha.onClick.AddListener(BuyItemToGacha);
        content_BuyItem.onClick.AddListener(GachaToBuyItem);
        content_ChangeProduct.onClick.AddListener(GachaToBuyItem);
        gachaType_Soul.onClick.AddListener(SoulGacha);
        gachaType_Weapon.onClick.AddListener(ItemGacha);
        gachaOnce.onClick.AddListener(PlayGachaOnce);
        gacha10.onClick.AddListener(PlayGacha10);
        exitButton.onClick.AddListener(Exit);
        gachaEvent = new GachaEvent();
    }

    private void Exit()
    {
        this.gameObject.SetActive(false);
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
        pickupImage.sprite = Resources.Load<Sprite>("Sprite/SoulSprite/Pickup/Carmilla");
    }

    private void ItemGacha()
    {
        GachaType = GachaType.Weapon;
        pickupImage.sprite = Resources.Load<Sprite>("Prefabs/Sample/CurItemPickup");
    }

    private void PlayGachaOnce()
    {
        int price = 150;
        if(DataManager.Instance.UserData.Diamonds >= price)
        {
            //DataManager.Instance.UserData.Diamonds -= price;
            switch (GachaType)
            {
                case GachaType.Soul:
                    EventManager.Instance.Publish<GachaEvent>(Channel.Gacha, gachaEvent.SetEvent(GachaType.Soul));
                    break;
                case GachaType.Weapon:
                    EventManager.Instance.Publish<GachaEvent>(Channel.Gacha, gachaEvent.SetEvent(GachaType.Weapon));
                    break;
            }
        }
        else Debug.LogAssertion("돈이 모자라요");
    }

    private void PlayGacha10()
    {
        int price = 1350;
        if (DataManager.Instance.UserData.Diamonds >= price)
        {
            //DataManager.Instance.UserData.Diamonds -= price;
            switch (GachaType)
            {
                case GachaType.Soul:
                    EventManager.Instance.Publish<GachaEvent>(Channel.Gacha, gachaEvent.SetEvent(GachaType.Soul, 10));
                    break;
                case GachaType.Weapon:
                    EventManager.Instance.Publish<GachaEvent>(Channel.Gacha, gachaEvent.SetEvent(GachaType.Weapon, 10));
                    break;
            }
        }
        else Debug.LogAssertion("돈이 모자라요");
    }
}