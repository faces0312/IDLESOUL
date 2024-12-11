using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoulSquadModel : UIModel
{
    public event Action OnSquadChanged;

    private List<SoulSquadSlot> slots = new List<SoulSquadSlot>();

    public void AddSlot(SoulSquadSlot slot)
    {
        slots.Add(slot);
        Debug.LogAssertion("슬롯 추가!");
        //OnInventoryChanged?.Invoke();
    }

    public void RemoveSlot(SoulSquadSlot slot)
    {
        if (slots.Remove(slot))
        {
            Debug.LogAssertion("슬롯 제거!");
            OnSquadChanged?.Invoke();
        }
        else
        {
            Debug.LogAssertion($"해당 슬롯은 존재하지 않습니다.");
        }
    }

    public void EquipSoul(Soul soul)
    {
        // TODO : 소울 슬롯에 장착 구현

        //SoulSquadSlot emptySlot = GetEmptySlot();

        //if (emptySlot != null)
        //{
        //    emptySlot.soul = soul;
        //    emptySlot.soulName = soul.soulName;
        //    OnSquadChanged?.Invoke();
        //    Debug.LogAssertion("소울 장착!");
        //    return;
        //}
    }

    //private SoulSquadSlot GetEmptySlot()
    //{
    //    for (int i = 0; i < slots.Count; i++)
    //    {
    //        if (slots[i].soul == null)
    //        {
    //            return slots[i];
    //        }
    //    }

    //    return null;
    //}
}
