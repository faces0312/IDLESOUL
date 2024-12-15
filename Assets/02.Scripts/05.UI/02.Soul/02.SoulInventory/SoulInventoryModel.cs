using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO : 완성 후 Serializable 제거
[Serializable]
public class SoulInventoryModel : UIModel
{
    public event Action OnInventoryChanged;
    // TODO : 완성 후 SerializeField 제거
    [SerializeField]private List<SoulSlot> slots = new List<SoulSlot>();

    public void AddSlot(SoulSlot slot)
    {
        slots.Add(slot);
    }

    public void RemoveSlot(SoulSlot slot)
    {
        if (slots.Remove(slot))
        {
            Debug.LogAssertion("슬롯 제거!");
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.LogAssertion($"해당 슬롯은 존재하지 않습니다.");
        }
    }
    
    public void AddSoul(Soul soul)
    {
        SoulSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.soul = soul;
            emptySlot.soulName = soul.soulName;
            OnInventoryChanged?.Invoke();
            //Debug.LogAssertion("소울 획득!");
            return;
        }
        
        // TODO : 빈 슬롯이 없다면
        // 슬롯을 추가하고 거기에 넣어 준다.
    }

    private SoulSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].soul == null)
            {
                return slots[i];
            }
        }
        
        return null;
    }

    public void UpdateThumbnail(string name)
    {
        foreach (var slot in slots)
        {
            if(slot.soulName == name && slot.soulName != string.Empty)
            {
                slot.OnUpdateThumbnail();
            }
        }
    }
}
