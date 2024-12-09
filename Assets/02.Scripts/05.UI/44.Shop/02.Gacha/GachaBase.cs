using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGachable
{
    int GetID();

    string GetName();

    string GetDescription();
}

public class GachaBase : MonoBehaviour
{
    [SerializeField] private GameObject gachaPanel;
    private List<IGachable> gachaList;

    private void OnEnable()
    {
        EventManager.Instance.Subscribe(Enums.Channel.Shop, Gacha);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe(Enums.Channel.Shop, Gacha);
    }

    private void Gacha()
    {
        
    }
}

public class GachaResult
{
    private List<IGachable> gachaResultList;
    [SerializeField] private Sprite ResultSprite;

    public IEnumerator CoResult()
    {
        foreach(IGachable gacha in gachaResultList)
        {
            yield return (Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase == TouchPhase.Began);
        }
        yield return null;
    }
}

public class GachaEvent
{
    public int key;
    public GachaType type;

    public GachaEvent(int key)
    {
        this.key = key;
    }

    public GachaEvent(int key, GachaType type)
    {
        this.key = key;
        this.type = type;
    }

    public GachaEvent SetEvent(int key)
    {
        this.key = key;
        return this;
    }

    public GachaEvent SetEvent(int key, GachaType type)
    {
        this.key = key;
        this.type = type;
        return this;
    }

}