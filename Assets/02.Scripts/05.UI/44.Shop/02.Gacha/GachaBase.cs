using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IGachableDB
{
}

public class GachaBase : MonoBehaviour
{
    [SerializeField] private GameObject gachaPanel;
    [SerializeField] private GachaResult result;
    private List<IGachableDB> gachaList;
    private List<IGachableDB> tempList;
    private List<ItemDB> items;
    private List<SoulDB> souls;

    private void OnEnable()
    {
        EventManager.Instance.Subscribe<GachaEvent>(Channel.Shop, Gacha);
    }

    private void Start()
    {
        items = DataManager.Instance.ItemDB.ItemsList;
        souls = DataManager.Instance.SoulDB.ItemsList;
        tempList = new List<IGachableDB>();
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe<GachaEvent>(Channel.Shop, Gacha);
    }

    private void Gacha(GachaEvent arg)
    {
        if(gachaList == null) gachaList = new List<IGachableDB>();
        tempList.Clear();
        gachaList.Clear();
        if (arg.type == GachaType.Weapon)
        {
            foreach (ItemDB item in items)
            {
                gachaList.Add(item);
            }
        }
        else if (arg.type == GachaType.Soul)
        {
            foreach (SoulDB soul in souls)
            {
                gachaList.Add(soul);
            }
        }
        for (int i = 0; i < arg.count; i ++)
        {
            int rand = UnityEngine.Random.Range(0, gachaList.Count);
            tempList.Add(gachaList[rand]);
        }
        result.SetList(tempList);
    }
}

public class GachaResult
{
    [SerializeField] private Sprite ResultSprite;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;

    private List<IGachableDB> gachaResultList;
    private List<GachaSlot> slots;


    public IEnumerator CoResult()
    {
        foreach(IGachableDB gacha in gachaResultList)
        {
            //전환 효과
            
            yield return (Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase == TouchPhase.Began);
        }
        yield return null;
    }

    public void SetList(List<IGachableDB> dbs)
    {
        gachaResultList = dbs;
    }
}

internal class GachaSlot
{
    private Image icon;

}


public class GachaEvent : IEventObject
{
    public int key;
    public GachaType type;
    public int count;
    

    public GachaEvent SetEvent(int key, GachaType type, int count = 1)
    {
        this.key = key;
        this.type = type;
        this.count = count;
        return this;
    }
}