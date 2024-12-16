using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulInventoryView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject invenPanel;
    [SerializeField] private Image thumbnail;

    public Sprite Sprite { get; set; }

    public void Initialize()
    {
        
    }

    public void ShowUI()
    {
        invenPanel.SetActive(true);
    }

    public void HideUI()
    {
        invenPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        if (Sprite != null)
            thumbnail.sprite = Sprite;

        //Debug.LogAssertion("소울 인벤토리 UI 업데이트");
    }
}
