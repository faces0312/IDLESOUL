using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoulInventoryModel : UIModel
{
    public event Action OnInventoryChanged;
    private List<SoulSlot> slots = new List<SoulSlot>();

    public void AddSoul(SoulSlot slot)
    {
        slots.Add(slot);
        Debug.LogAssertion("�ҿ� ȹ��!");
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(SoulSlot slot)
    {
        if (slots.Remove(slot))
        {
            Debug.LogAssertion("�ҿ� �ı�!");
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.LogAssertion($"�ش� �ҿ��� �������� �ʽ��ϴ�.");
        }
    }
}
