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

    private WaitUntil click = new WaitUntil(() =>
        {
            return isConfirm;
        });

    

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
    }

    public IEnumerator CoResult()
    {
        while (true)
        {
            gachaBase.result.gameObject.SetActive(true);
            for (int i = 0; i < gachaResultList.Count; i++)
            {
                switch (gachaResultList[i].GetType().ToString())
                {
                    case "SoulDB":
                        tempSoul = DataManager.Instance.SoulDB.GetByKey(gachaResultList[i].GetKey());
                        resultSprite.sprite = Resources.Load<Sprite>(tempSoul.SpritePath);
                        _name.text = tempSoul.Name;
                        description.text = tempSoul.Descripton;
                        break;
                    case "ItemDB":
                        tempItem = DataManager.Instance.ItemDB.GetByKey(gachaResultList[i].GetKey());
                        resultSprite.sprite = Resources.Load<Sprite>(tempItem.SpritePath);
                        _name.text = tempItem.Name;
                        description.text = tempItem.Descripton;
                        break;
                }
                isConfirm = false;
                yield return click;
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
}
