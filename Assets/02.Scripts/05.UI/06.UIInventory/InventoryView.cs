﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class InventoryView : MonoBehaviour, IUIBase
{
    [SerializeField] private Transform itemSlotParent;
    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private RectTransform itemSlotBoundary;

    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    public void Initialize()
    {
        if (itemSlotPrefab == null)
        {
            itemSlotPrefab = Resources.Load<ItemSlot>("Prefabs/Item/ItemSlot");
        }
       
        for (int i = 0; i < GameManager.Instance.player.Inventory.Items.Count; i++)
        {
            itemSlots.Add(Instantiate(itemSlotPrefab, itemSlotParent));

             itemSlots[i].Initiliaze(GameManager.Instance.player.Inventory.Items[i]);
        }
    }

    public void ShowUI()
    {
        Vector2 size = itemSlotBoundary.sizeDelta;
        size.y = 135 * ((itemSlots.Count / 4) + 1);
        itemSlotBoundary.sizeDelta = size;

        for (int i = 0; i < GameManager.Instance.player.Inventory.Items.Count; i++)
        {
            itemSlots[i].UIUpdate();    
        }

        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
    }
}
