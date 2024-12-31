using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class GachaResult : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image resultSprite;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GachaBase gachaBase;
    [SerializeField] private Button skipButton;
    private GachaResultController controller;

    private List<IGachableDB> gachaResultList;

    public Coroutine coResult;

    public bool isSkip;

    private SoulDB tempSoul;
    private ItemDB tempItem;

    private static bool isConfirm;

    private WaitUntil click;



    private void Awake()
    {
        gachaResultList = new List<IGachableDB>();
        skipButton.onClick.AddListener(() =>
        {
            isSkip = true;
        });
        controller = new GachaResultController();
        controller.GachaPanel = this.gameObject;
        UIManager.Instance.RegisterController(controller.key, controller);
        this.gameObject.SetActive(false);
        click = Click();
    }

    public IEnumerator CoResult()
    {
        while (true)
        {
            isSkip = false;
            gachaBase.result.gameObject.SetActive(true);
            int curSoulCount = GameManager.Instance.player.PlayerSouls.SoulInventory.GetSoulCount();
            for (int i = 0; i < gachaResultList.Count; i++)
            {
                switch (gachaResultList[i].GetType().ToString())
                {
                    case "SoulDB":
                        tempSoul = DataManager.Instance.SoulDB.GetByKey(gachaResultList[i].GetKey());
                        resultSprite.sprite = Resources.Load<Sprite>(tempSoul.SpritePath);
                        _name.text = tempSoul.Name;
                        description.text = tempSoul.Descripton;
                        RegistSoul(tempSoul);
                        break;
                    case "ItemDB":
                        tempItem = DataManager.Instance.ItemDB.GetByKey(gachaResultList[i].GetKey());
                        resultSprite.sprite = Resources.Load<Sprite>(tempItem.IconPath);
                        _name.text = tempItem.Name;
                        description.text = tempItem.Descripton;
                        RegistItem(tempItem);
                        break;
                }
                if(isSkip == false)
                {
                    isConfirm = false;
                    yield return click;
                }
                else
                {
                    isConfirm = true;
                }
            }
            this.gameObject.SetActive(false);
            gachaBase.grid.gameObject.SetActive(true);
            StopCoroutine(coResult);
            yield return null;
        }
    }

    public void SetList(List<IGachableDB> dbs)
    {
        gachaResultList.Clear();
        foreach (IGachableDB gachaResult in dbs)
        {
            gachaResultList.Add(gachaResult);
        }
        gachaBase.grid.SetSlots(dbs);
    }

    public List<IGachableDB> GetList()
    {
        return gachaResultList;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isConfirm = true;
    }

    public void RegistSoul(SoulDB data)
    {
        switch((SoulType)data.SoulType)
        {
            case SoulType.Magician:
                SoulMagician soulM = new SoulMagician(data.GetKey());
                GameManager.Instance.player.PlayerSouls.RegisterSoul(data.Name, soulM);
                break;
            case SoulType.Knight:
                SoulKnight soulK = new SoulKnight(data.GetKey());
                GameManager.Instance.player.PlayerSouls.RegisterSoul(data.Name, soulK);
                break;
            case SoulType.Archer:
                SoulArcher soulA = new SoulArcher(data.GetKey());
                GameManager.Instance.player.PlayerSouls.RegisterSoul(data.Name, soulA);
                break;
            case SoulType.DummyRare:
                SoulDummyRare soulR = new SoulDummyRare(data.GetKey());
                GameManager.Instance.player.PlayerSouls.RegisterSoul(data.Name, soulR);
                break;
            case SoulType.DummyEpic:
                SoulDummyEpic soulE = new SoulDummyEpic(data.GetKey());
                GameManager.Instance.player.PlayerSouls.RegisterSoul(data.Name, soulE);
                break;
        }
    }

    public void RegistItem(ItemDB data)
    {
        GameManager.Instance.player.Inventory.AddItem(data.GetKey());
    }

    public WaitUntil Click()
    {
        click = new WaitUntil(() =>
        {
            if(isSkip == true)
            {
                return true;
            }
            else return isConfirm;
        });
        return click;
    }
}
