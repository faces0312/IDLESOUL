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
        Debug.LogAssertion("소울 획득!");
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(SoulSlot slot)
    {
        if (slots.Remove(slot))
        {
            Debug.LogAssertion("소울 파괴!");
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.LogAssertion($"해당 소울은 존재하지 않습니다.");
        }
    }
}
