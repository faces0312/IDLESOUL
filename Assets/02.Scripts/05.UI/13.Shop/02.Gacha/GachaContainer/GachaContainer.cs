using Enums;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GachaContainer : MonoBehaviour
{
    [SerializeField] private Button SoulPickUP;
    [SerializeField] private Button Soul;
    [SerializeField] private Button WeaponPickUP;
    [SerializeField] private Button Weapon;

    [SerializeField] private Button GachaOnce;
    [SerializeField] private Button Gacha10;

    [SerializeField] private Image containerImage;

    private GachaContainerController controller;
    private GachaEvent gacha;
    private GachaType gachatype = GachaType.Soul;

    private void Start()
    {
        controller = new GachaContainerController();
        controller.GachaContainer = this.gameObject;
        UIManager.Instance.RegisterController(controller.key, controller);
        gacha = new GachaEvent();

        SoulPickUP.onClick.AddListener(() =>
        {
            gachatype = GachaType.Soul;
            containerImage.sprite = Resources.Load<Sprite>("Sprite/SoulSprite/Pickup/Carmilla");
        });

        Soul.onClick.AddListener(() =>
        {
            gachatype = GachaType.Soul;
            containerImage.sprite = Resources.Load<Sprite>("Sprite/SoulSprite/Pickup/General");
        });

        WeaponPickUP.onClick.AddListener(() =>
        {
            gachatype = GachaType.Weapon;
            containerImage.sprite = Resources.Load<Sprite>("Sprite/ItemSprite/Pickup/Pickup");
        });

        Weapon.onClick.AddListener(() =>
        {
            gachatype = GachaType.Weapon;
            containerImage.sprite = Resources.Load<Sprite>("Sprite/ItemSprite/Pickup/General");
        });

        GachaOnce.onClick.AddListener(() =>
        {
            if(GameManager.Instance.player.UserData.Diamonds >= 130)
            {
                GameManager.Instance.player.UserData.Diamonds -= 130;
                if (gachatype == GachaType.Soul) EventManager.Instance.Publish<GachaEvent>(Enums.Channel.Gacha, gacha.SetEvent(Enums.GachaType.Soul, 1));
                else EventManager.Instance.Publish<GachaEvent>(Enums.Channel.Gacha, gacha.SetEvent(Enums.GachaType.Weapon, 1));
            }
        });

        Gacha10.onClick.AddListener(() =>
        {
            if (GameManager.Instance.player.UserData.Diamonds >= 1300)
            {
                GameManager.Instance.player.UserData.Diamonds -= 1300;
                if (gachatype == GachaType.Soul) EventManager.Instance.Publish<GachaEvent>(Enums.Channel.Gacha, gacha.SetEvent(Enums.GachaType.Soul, 10));
                else EventManager.Instance.Publish<GachaEvent>(Enums.Channel.Gacha, gacha.SetEvent(Enums.GachaType.Weapon, 10));
            }
        });

    }
}